using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.CCSystem.Transaction
{
    public class MRController : BaseApiController
    {
        public IApplicationCartonDbContext _context { get; }
        public IMRRepository _mrRepository { get; set; }

        public MRController(IApplicationCartonDbContext context, IMRRepository mrRepository)
        {
            _mrRepository = mrRepository;
            _context = context;
        }

        [HttpPost("SaveMR")]
        public async Task<IActionResult> SaveMaterialRequest(MaterialRequestDto mrDto)
        {
            var result = await _mrRepository.SaveMaterialRequestAsync(mrDto);
            return Ok(result);
        }

        [HttpPost("SendMR")]
        public async Task<IActionResult> SendMaterialRequest(TransMRHeader mrHeader)
        {
            var result = await _mrRepository.SendMaterialRequestAsync(mrHeader);
            return Ok(result);
        }

        [HttpGet("MRDtList/{mrHeaderId}")]
        public async Task<IActionResult> GetMRDetails(long mrHeaderId)
        {
            var result = await _mrRepository.GetMRDetailsAsync(mrHeaderId);
            return Ok(result);
        }

        [HttpPost("MRList")]
        public async Task<IActionResult> GetMRNoList(MRSearchDto searchDto)
        {
            var result = await _mrRepository.GetMRNoListAsync(searchDto);
            return Ok(result);
        }

        [HttpPost("ApproveMR")]
        public async Task<IActionResult> ApproveMaterialRequest(ApproveMRDto mrApprove)
        {
            var result = await _mrRepository.ApproveMaterialRequestAsync(mrApprove);
            return Ok(result);
        }

        [HttpPost("CancelMR")]
        public async Task<IActionResult> CancelMaterialRequest(TransMRHeader mrHeader)
        {
            var result = await _mrRepository.CancelMRAsync(mrHeader);
            return Ok(result);
        }

        [HttpGet("InvStock/{mrHeaderId}")]
        public async Task<IActionResult> GetInventoryStock(long mrHeaderId)
        {
            var result = await _mrRepository.GetInventoryStockAsync(mrHeaderId);
            return Ok(result);
        }

       
    }
}
