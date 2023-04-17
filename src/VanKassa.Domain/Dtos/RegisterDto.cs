﻿using VanKassa.Domain.Enums;

namespace VanKassa.Domain.Dtos
{
    public class RegisterDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; }
    }
}
