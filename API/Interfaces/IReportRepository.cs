using System.Threading.Tasks;
using System.Collections.Generic;
using API.Entities;
using API.DTOs;


namespace API.Interfaces
{
    public interface IReportRepository
    {
         Task<int> SaveReportAsync(MstrReport report);
         Task<IEnumerable<ReportsReturnDto>> GetReportListAsync(ReportSearchDto dashDto); 

    }
}