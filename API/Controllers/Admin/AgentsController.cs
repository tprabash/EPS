using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Entities.Admin;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Admin
{
    [Authorize]
    public class AgentsController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationAdminDbContext _context;
        private readonly IAdminRepository _adminRepository;

        public AgentsController(IUserRepository userRepository, IMapper mapper , IApplicationAdminDbContext context
            , IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
            _context = context;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        //[AllowAnonymous]
        [HttpGet("regUser/{logUserId}")]
        public async Task<ActionResult> GetUsers(int logUserId)
        {
            var user = await _context.MstrAgents.Where(x => x.idAgents == logUserId).FirstOrDefaultAsync();
            var level = await _context.MstrAgentLevel.Where(x => x.AutoId == user.iCategoryLevel).FirstOrDefaultAsync();

            var usersToReturn = await _context.MstrAgents                    
                    .Join(_context.MstrAgentLevel, a => a.iCategoryLevel , l => l.AutoId 
                        , (a , l) => new {
                        idAgents = a.idAgents ,
                        agentName = a.cAgentName,
                        bActive = a.bActive,
                        email = a.cEmail,
                        description = a.cDescription,
                        levelDescription = l.LevelDescription,
                        categoryLevel = a.iCategoryLevel,
                        levelPrority = l.LevelPrority
                    })
                    .Where(s => s.levelPrority >= level.LevelPrority)
                    .ToListAsync();

            return Ok(usersToReturn);
        }

        [HttpPost("Location")]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetLocation(MstrLocation loc)
        {
            var location = await _adminRepository.GetLocationAsync(loc);
            var locationToReturn = _mapper.Map<IEnumerable<LocationDto>>(location);
            return Ok(locationToReturn);
        }

        [HttpPost("User/Location")]
        public async Task<ActionResult<IEnumerable<UserLocationDto>>> GetUserLoction(MstrAgentModule userMod)
        {
            var location = await _adminRepository.GetUserLocAsync(userMod);
            var locationToReturn = _mapper.Map<IEnumerable<UserLocationDto>>(location);
            return Ok(locationToReturn);
        }

        [HttpGet("name/{username}")]
        public async Task<ActionResult<RegisterDto>> GetUserByName(string username)
        {
            if (!await UserExists(username)) return BadRequest("User not exists");
            var user = await _userRepository.GetUserByUsernameAsync(username);
            return _mapper.Map<RegisterDto>(user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegisterDto>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<RegisterDto>(user);
        }

        // [HttpPut("{username}")]
        // public async Task<ActionResult<bool>> ChangePassword(string username, UserUpdateDto updateDto)
        // {
        //     if (username != User.FindFirst(ClaimTypes.NameIdentifier).Value)
        //         return Unauthorized();

        //     var userFormRepo = await _userRepository.GetUserByUsernameAsync(username);

        //     using var hmac = new HMACSHA512();
        //     updateDto.passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(updateDto.cPassword));
        //     updateDto.passwordSalt = hmac.Key;

        //     _mapper.Map(updateDto, userFormRepo);

        //     if (await _userRepository.SaveAllAsync())
        //         return NoContent();

        //     throw new Exception($"Updating user {username} failed on server");
        // }

        [HttpPost("ChPwdUser")]
        public async Task<ActionResult> ChangeUserPwd(UserUpdateDto agents)
        {
            using var hmac = new HMACSHA512();
            agents.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(agents.cPassword));
            agents.PasswordSalt = hmac.Key;

            var result = await _userRepository.ChangeUserPwdAsync(agents);
            return Ok(result);
        }

        [HttpPost("DeActUser")]
        public async Task<ActionResult> DisableUser(StateUserDto agents)
        {
            var result = await _userRepository.DisableUserAsync(agents);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("Module")]
        public async Task<IEnumerable<SystemModule>> GetModules()
        {
            return await _userRepository.GetModuleAsync();
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.MstrAgents.AnyAsync(x => x.cAgentName == username.ToLower());
        }

        [HttpGet("AgentLevel/{id}")]
        public async Task<ActionResult<IEnumerable<UserLevelDto>>> GetAgentLevel(int id)
        {
            var levels = await _userRepository.AgentLevelsAsync(id);
            var levelToReturn = _mapper.Map<IEnumerable<UserLevelDto>>(levels);
            return Ok(levelToReturn);
        }

        [HttpGet("Users/{id}")]
        public async Task<ActionResult<IEnumerable<PermitedUserDto>>> GetPermittedUser(int id)
        {
            var userList = await _userRepository.GetPermitedAgentsAsync(id);
            var usersToReturn = _mapper.Map<IEnumerable<PermitedUserDto>>(userList);
            return Ok(usersToReturn);
        }

        [HttpGet("Users/Module/{id}")]
        public async Task<ActionResult<IEnumerable<UserModuleDto>>> GetUserModuleAsync(int id)
        {
            var moduleList = await _adminRepository.GetUserModuleAsync(id);
            return Ok(moduleList);
        }

        [HttpPost("UserModSave")]
        public async Task<IActionResult> SaveUserModule(List<UserModuleDto> userModList)
        {
            var result = await _adminRepository.SaveUserModule(userModList);
            return Ok(result);             
        }

        [HttpPost("UserModDelete")]
        public async Task<IActionResult> DeleteUserModule(DeleteuserModDto DeleteuserModDto)
        {
            var result = await _adminRepository.DeleteUserModule(DeleteuserModDto);
            return Ok(result);             
        }


       
    }
}