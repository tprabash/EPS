using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Type = API.Entities.Type;

namespace API.Controllers.MTrack
{
    [Authorize]
    public class MMasterController : BaseApiController
    {
        private readonly IApplicationMTrackDbContext _context;
        private readonly IMTrackMasterRepository _mTrackMasterRepository;

        public MMasterController(IApplicationMTrackDbContext context,  IMTrackMasterRepository mTrackMasterRepository)
        {
            _mTrackMasterRepository = mTrackMasterRepository;
            _context = context;
        }
        // #region Get MachineBreaks
        // [HttpGet("GetMachinBK/{location}")]
        // public async Task<IActionResult> GetFactoryWiseMachineBreak(string location)
        // {
        //     var result = await _mTrackMasterRepository.GetFactoryWiseMachineBreakAsync(location);
        //     return Ok(result);
        // }
        // #endregion Get MachineBreaks
    }
}