using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.CCSystem.Master
{
    //
    [Authorize]
    public class ReportController : BaseApiController
    {
        private readonly IReportRepository _reportRepository;
        private readonly IApplicationCartonDbContext _context;
        private readonly IMapper _mapper;
        public ReportController(IReportRepository reportRepository, IApplicationCartonDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _reportRepository = reportRepository;
        }

        #region Report

        [HttpPost("ReportL")]
        public async Task<IActionResult> GetReportList(ReportSearchDto reportSearch) 
        {
            var result = await _reportRepository.GetReportListAsync(reportSearch);
            return Ok(result);
        }
        
        [HttpPost("SaveR")]
        public async Task<IActionResult> SaveReport(MstrReport report)
        {
            var result = await _reportRepository.SaveReportAsync(report);
            return Ok(result);
        }

        #endregion Report
    }
}