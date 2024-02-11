using System;
namespace API.DTOs
{
    public class DashboardExpChartSearchDto
    {
        public DateTime transDate {get;set;}
        public DateTime toDate {get;set;}
        public int Action { get; set; }
    }
}