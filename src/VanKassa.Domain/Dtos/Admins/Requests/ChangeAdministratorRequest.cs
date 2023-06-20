﻿namespace VanKassa.Domain.Dtos.Admins.Requests
{
    public class ChangeAdministratorRequest : AdminDtoBase
    {
        public int AdminId { get; set; }
        public string NewPassword { get; set; } = string.Empty;
    }
}
