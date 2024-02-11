using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class DashBoardSalesDto
    {   
        public virtual IEnumerable<DashBoardStatsDto> DashBoardStats { get; set; }
        public virtual IEnumerable<DashBoardAnualSalesDto> AnualSales { get; set; }
        public virtual IEnumerable<DashBoardBrandCodeSalesDto> BrandCodeSales { get; set; }
        public virtual IEnumerable<DashBoardMonthlySalesDto> MonthlySales { get; set; }
        public virtual IEnumerable<DashBoardMonthlyProdTypeSalesDto> MonthlyProductTypeSales { get; set; }
        public virtual IEnumerable<DashBoardMonthlyDispatchSalesDto> MonthlyDispatchSales { get; set; }
        
    }
}