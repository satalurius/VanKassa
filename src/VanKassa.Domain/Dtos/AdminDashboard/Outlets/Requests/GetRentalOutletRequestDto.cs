using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanKassa.Domain.Dtos.AdminDashboard.Outlets.Requests;

public class GetRentalOutletRequestDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
