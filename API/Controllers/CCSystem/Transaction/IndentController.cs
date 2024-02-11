using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.CCSystem.Transaction
{
    public class IndentController : BaseApiController
    {
        private readonly IApplicationCartonDbContext _context;
        private readonly IIndentRepository _indentRepository;

        public IndentController(IApplicationCartonDbContext context, IIndentRepository indentRepository)
        {
            _context = context;
            _indentRepository = indentRepository;
        }

        #region IntentDetails

        [HttpPost("IntHd")]
        public async Task<IActionResult> GetIntentHeader(IndentSearchDto searchDto)
        {
            var result = await _indentRepository.GetIntentHeaderAsync(searchDto);
            return Ok(result);
        }

        [HttpGet("IntDt/{indentHederId}")]
        public async Task<IActionResult> GetIntentDetails(long indentHederId)
        {
            var result = await _indentRepository.GetIntentDetailsAsync(indentHederId);
            return Ok(result);
        }

        [HttpPost("IntPO")]
        public async Task<IActionResult> GetIntentDetailsByIds(IndentIdListDto indent)
        {
            var result = await _indentRepository.GetIntentDetailsByIdsAsync(indent);
            return Ok(result);
        }

        [HttpPost("SaveInd")]
        public async Task<IActionResult> SaveAdhocIndent(AdhocIndetSaveDto indentDetailsDto)
        {
            var result = await _indentRepository.SaveAdhocIndentAsync(indentDetailsDto);
            return Ok(result);
        }

        [HttpPost("ChAssignTo")]
        public async Task<IActionResult> ChangeIntentAssignedTo(IndentAssignToDto indentHeader)
        {
            var result = await _indentRepository.ChangeIntentAssignedToAsync(indentHeader);
            return Ok(result);
        }


        #endregion IntentDetails
    }
}
