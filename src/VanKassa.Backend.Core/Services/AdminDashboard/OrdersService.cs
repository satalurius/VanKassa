using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Helpers;
using VanKassa.Backend.Core.Services.Interface.AdminDashboard;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Entities;
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

            var orderId = Guid.NewGuid();
            
            var order = new Order
            {
                OrderId = orderId,
                Price = request.Price,
                Date = DateTime.SpecifyKind(request.Date, DateTimeKind.Unspecified),
                Canceled = OrdersHelper.GetCanceledStatusFromOrderStatusEnum(request.OrderStatus),
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
}