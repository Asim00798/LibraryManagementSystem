using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Request
{
    public class ReservationRequestDto
    {
        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;
    }
}
