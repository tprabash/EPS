using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.CCSystem.Transaction
{
    public class DispatchController : BaseApiController
    {
        private readonly IApplicationCartonDbContext _context;
        private readonly ISalesRepository _salesRepository;
        public DispatchController(IApplicationCartonDbContext context, ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
            _context = context;
        }

        [HttpGet("ListCus/{customerRef}")]
        public async Task<IActionResult> GetDispatchListByCustomer(string customerRef)
        {
            var result = await _salesRepository.GetDispatchListAsync(customerRef);
            return Ok(result);
        }

        [HttpGet("ListDisp/{dispatchNo}")]
        public async Task<IActionResult> GetDispatchListsByNo(string dispatchNo)
        {
            var result = await _salesRepository.GetDispatchListsAsync(dispatchNo);
            return Ok(result);
        }

        [HttpPost("PendList")]
        public async Task<IActionResult> GetPendDispatchDetails(PendDispatchDto prod)
        {
            var result = await _salesRepository.GetPendDispatchDtAsync(prod);
            return Ok(result);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveDispatchDetails(TransDispatchDto dispatch)
        {
            var result = await _salesRepository.SaveDispatchedDtAsync(dispatch);
            return Ok(result);
        }

        [HttpPost("Details")]
        public async Task<IActionResult> GetDispatchDetails(DispatchSearchDto dispatch)
        {
            var result = await _salesRepository.GetDispatchDetails(dispatch);
            return Ok(result);
        }

        [HttpPost("DispStock")]
        public async Task<IActionResult> GetDispatchStock(DispatchRequestDto requestDto)
        {
            var result = await _salesRepository.GetDispatchStockAsync(requestDto);
            return Ok(result);
        }

        [HttpPost("CancelDD")]
        public async Task<IActionResult> CancelDispatchNote(TransDispatchHeader dispHd)
        {
            var result = await _salesRepository.CancelDispatchDtAsync(dispHd);
            return Ok(result);
        }
    }
}
