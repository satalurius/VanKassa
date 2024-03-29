﻿using VanKassa.Domain.Entities.Base;

namespace VanKassa.Domain.Entities;

/// <summary>
/// Модель базы данных, представляющая таблицу точек-предприятий.
/// </summary>
public class Outlet
{
    public int OutletId { get; set; }
    public required string City { get; set; }
    public required string Street { get; set; }
    public string? StreetNumber { get; set; }

    public IEnumerable<EmployeeOutlet> UserOutlets { get; set; } = new HashSet<EmployeeOutlet>();
}