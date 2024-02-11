using System;
using API.Entities;

namespace API.DTOs
{
    public class DashboardSearchDto
    {
        public int ActionId { get; set; }
        public DateTime Transdate { get; set; }
        public DateTime Todate { get; set; }
        
    }
}
