﻿namespace VanKassa.Domain.Dtos.AdminDashboard.Orders.Products;

public class UpdateProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}