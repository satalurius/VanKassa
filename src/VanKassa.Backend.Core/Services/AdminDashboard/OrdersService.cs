﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Helpers;
using VanKassa.Backend.Core.Services.Interface.AdminDashboard;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Requests;
using VanKassa.Domain.Entities;
using VanKassa.Domain.Enums.AdminDashboard.Orders;
using VanKassa.Domain.Exceptions;

namespace VanKassa.Backend.Core.Services.AdminDashboard;

public class OrdersService : IOrdersService
{
    private readonly IDbContextFactory<VanKassaDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public OrdersService(IDbContextFactory<VanKassaDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }


    public async Task CreateOrderAsync(CreateOrderRequestDto request)
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var products = await dbContext.Products
                .Where(product => request.ProductsIds.Any(id => id == product.ProductId))
                .ToListAsync();

            var outlet = await dbContext.Outlets
                .FirstOrDefaultAsync(outlet => outlet.OutletId == request.OutletId);

            if (outlet is null)
            {
                throw new NotFoundException();
            }

            var orderId = Guid.NewGuid();
            
            var order = new Order
            {
                OrderId = orderId,
                Price = request.Price,
                Date = DateTime.SpecifyKind(request.Date, DateTimeKind.Unspecified),
                Canceled = OrdersHelper.GetCanceledStatusFromOrderStatusEnum(request.OrderStatus),
                Outlet = outlet
            };

            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();

            var orderEntity = await dbContext.Orders
                .FirstOrDefaultAsync(ord => ord.OrderId == orderId);

            if (orderEntity is null)
            {
                throw new ArgumentNullException();
            }

            var orderProducts = products
                .Select(product => new OrderProduct
                {
                    Order = orderEntity,
                    Product = product
                })
                .ToList();
            
            dbContext.OrderProducts.AddRange(orderProducts);
            await dbContext.SaveChangesAsync();

        }
        catch (OperationCanceledException)
        {
            throw new BadRequestException("Произошла ошибка создания заказа");
        }
        catch (ArgumentNullException)
        {
            throw new BadRequestException("Произошла ошибка создания заказа");
        }

    }

    public async Task<PageOrderDto> GetOrderByFilterAsync(OrdersPageParameters request)
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var count = await dbContext.Orders
                .CountAsync();

            var orderQuery = dbContext.Orders
                .Include(order => order.OrderProducts)
                .ThenInclude(orderProduct => orderProduct.Product)
                .Include(order => order.Outlet)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.FilterText))
            {
                orderQuery = orderQuery
                    .Where(order => order.OrderProducts
                        .Any(orderProduct => orderProduct.Product.Name.ToLower() == request.FilterText.ToLower()));
            }

            var orders = await orderQuery
                .Skip(request.Page * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();


            var ordersDtos = mapper.Map<IList<OrderDto>>(orders);

            return new PageOrderDto
            {
                TotalCount = count,
                Orders = ordersDtos
            };
        }
        catch (OperationCanceledException)
        {
            throw new BadRequestException("Произошла ошибка при получении заказов");
        }
        catch (ArgumentException)
        {
            throw new BadRequestException("Произошла ошибка при получении заказов");
        }
    }

}