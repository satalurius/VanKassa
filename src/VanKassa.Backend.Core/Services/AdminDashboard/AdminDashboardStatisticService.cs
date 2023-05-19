using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanKassa.Backend.Core.Services.Interface.AdminDashboard;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Requests;
using VanKassa.Domain.Dtos.AdminDashboard.Outlets.Requests;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics;
using VanKassa.Domain.Entities;
using VanKassa.Domain.Enums.AdminDashboard.Orders;
using VanKassa.Domain.Exceptions;

namespace VanKassa.Backend.Core.Services.AdminDashboard
{
    public class AdminDashboardStatisticService : IAdminDashboardStatisticService
    {

        private readonly IDbContextFactory<VanKassaDbContext> dbContextFactory;
        private readonly IMapper mapper;

        public AdminDashboardStatisticService(IMapper mapper, IDbContextFactory<VanKassaDbContext> dbContextFactory)
        {
            this.mapper = mapper;
            this.dbContextFactory = dbContextFactory;
        }

        #region OrdersStatistic

        public async Task<OrdersStatisticByPeriodDto> GetOrdersStatisticByPeriodAsync(GetOrdersByPeriodRequest request)
        {
            try
            {
                await using var dbContext = await dbContextFactory.CreateDbContextAsync();

                var orders = await dbContext.Orders
                    .Where(order => order.Date.Date <= request.Date.Date
                        && !order.Canceled)
                    .ToListAsync();

                return new OrdersStatisticByPeriodDto
                {
                    Count = orders.Count,
                    TotalMoney = CalcMoneyFromOrders(orders),
                    EndDate = request.Date.Date
                };
            }
            catch (OperationCanceledException)
            {
                throw new BadRequestException("Произошла ошибка при расчете заказов");
            }
            catch (ArgumentException)
            {
                throw new BadRequestException("Произошла ошибка при расчете заказов");
            }
        }

        public async Task<IList<SoldOrderByMonthDto>> GetOrdersStatisticByEveryMonth(GetOrdersByEveryMonthRequest request)
        {
            try
            {
                await using var dbContext = await dbContextFactory.CreateDbContextAsync();

                var countMonthOfYear = 12;

                var soldOrders = new List<SoldOrderByMonthDto>();

                for (var monthNumber = 1; monthNumber <= countMonthOfYear; monthNumber++)
                {
                    var number = monthNumber;

                    var ordersByMonth = await dbContext.Orders
                        .Where(order => order.Date.Year == request.YearDate
                                        && !order.Canceled
                                        && order.Date.Month == number)
                        .ToListAsync();


                    var soldOrder = new SoldOrderByMonthDto
                    {
                        Month = (Month)monthNumber,
                        Year = request.YearDate,
                        TotalMoney = CalcMoneyFromOrders(ordersByMonth),
                        Count = ordersByMonth.Count,
                    };

                    soldOrders.Add(soldOrder);
                }

                return soldOrders;
            }
            catch (OperationCanceledException)
            {
                throw new BadRequestException("Произошла ошибка при расчете статистики");
            }
            catch (ArgumentException)
            {
                throw new BadRequestException("Произошла ошибка при расчете статистики");
            }
        }

        #endregion

        public async Task<IList<RentalOutletDto>> StatisticForRentalOutletByPeriodAsync(GetRentalOutletRequestDto getRentalOutletRequest)
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var rentalOutlets = (await dbContext.Orders
                .Include(order => order.Outlet)
                .Where(order => order.Date >= getRentalOutletRequest.StartDate 
                    && order.Date <= getRentalOutletRequest.EndDate)
                .GroupBy(gr => gr.Outlet.OutletId)
                .ToListAsync())
                .Select(outletGroup => BuildRentalOutlet(outletGroup))
                .OrderByDescending(ord => ord.TotalMoney)
                .ToList();

            if (rentalOutlets.Count == 0)
            {
                throw new NotFoundException("Заказы не найдены");
            }

            return rentalOutlets;
        }


        private RentalOutletDto BuildRentalOutlet(IGrouping<int, Order> orderGroup)
        {
            var totalMoney = orderGroup
                .Where(ord => !ord.Canceled)
                .Sum(outlet => outlet.Price);

            var canceledMoney = orderGroup
                .Where(ord => ord.Canceled)
                .Sum(outlet => outlet.Price);

            var couldEarn = totalMoney + canceledMoney;

            var canceledCount = orderGroup.Count(order => order.Canceled);

            var outlet = orderGroup.First().Outlet;

            var outletName = string.Join(" ", outlet.City, outlet.Street, outlet.StreetNumber ?? string.Empty);

            return new RentalOutletDto
            {
                OutletId = orderGroup.Key,
                TotalMoney = totalMoney,
                CanceledMoney = canceledMoney,
                CanceledCount = canceledCount,
                CouldEarn = couldEarn,
                OutletName = outletName,
            };
        }


        private decimal CalcMoneyFromOrders(IList<Order> orders)
            => orders
                .Sum(order => order.Price);

    }
}
