﻿namespace VanKassa.Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public required string Patronymic { get; set; }
    public required string Photo { get; set; }
    public required int RoleId { get; set; }
    public required Role Role { get; set; }

    public IEnumerable<UserOutlet> UserOutlets { get; set; } = new List<UserOutlet>();
}