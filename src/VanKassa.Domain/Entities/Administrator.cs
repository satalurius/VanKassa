﻿using VanKassa.Domain.Entities.Base;

namespace VanKassa.Domain.Entities
{
    public class Administrator : EmployeeBase
    {
        public string Phone { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
