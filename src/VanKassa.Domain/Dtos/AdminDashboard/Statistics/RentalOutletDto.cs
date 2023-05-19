using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanKassa.Domain.Dtos.AdminDashboard.Statistics
{
    public class RentalOutletDto
    {
        public int OutletId { get; set; }
        public decimal TotalMoney { get; set; }
        public int CanceledCount { get; set; }
        public decimal CanceledMoney { get; set; }
        public decimal CouldEarn { get; set; }

        public string OutletName { get; set; }

    }
}
