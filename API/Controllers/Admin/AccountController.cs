using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Entities.Admin;
using API.Entities.Ptrack;
using API.Entities.MTrack;
using API.Entities.MWS;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace API.Controllers.Admin
{
    /// Admin COntroller
    public class AccountController : BaseApiController
    {
        private readonly IApplicationAdminDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IMasterRepository _masterRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IPTrackMasterRepository _pTrackMasterRepository;
        private readonly IMTrackMasterRepository _mTrackMasterRepository;
        private readonly IMWSMasterRepository _mwsMasterRepository;

        public AccountController(IApplicationAdminDbContext context, ITokenService tokenService, IMapper mapper
            , IMasterRepository masterRepository, IAdminRepository adminRepository,IPTrackMasterRepository pTrackMasterRepository 
            ,IMTrackMasterRepository mTrackMasterRepository , IMWSMasterRepository mwsMasterRepository)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _context = context;
            _masterRepository = masterRepository;
            _pTrackMasterRepository = pTrackMasterRepository;
            _mTrackMasterRepository = mTrackMasterRepository;
            _mwsMasterRepository=mwsMasterRepository;
        }

        [Authorize]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.cAgentName)) return BadRequest("Username is taken");

            var user = _mapper.Map<MstrAgents>(registerDto);

            using var hmac = new HMACSHA512();

            //var moduleName = await GetFactoryName(registerDto.SysModuleId);           
            user.cAgentName = registerDto.cAgentName;
            user.iCategoryLevel = registerDto.iCategoryLevel;
            user.bActive = true;
            user.cPassword = registerDto.cPassword;
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.cPassword));
            user.PasswordSalt = hmac.Key;

            _context.MstrAgents.Add(user);
            await _context.SaveChangesAsync(default);

            return Ok();
        }

        [Authorize]
        [HttpPost("RegModule")]
        public async Task<ActionResult> RegisterModule(List<MstrAgentModule> userModule)
        {
            var saveExists = false;

            foreach (var item in userModule)
            {
                if (await UserModuleExists(item))
                {
                    continue;
                }
                else
                {
                    _context.MstrAgentModule.Add(item);
                    saveExists = true;
                }
            }

            if (saveExists)
                await _context.SaveChangesAsync(default);
            else
                return BadRequest("User Module Exists");

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            IEnumerable<PermitMenuDto> menuList = Enumerable.Empty<PermitMenuDto>();
            IEnumerable<UserLocationDto> locationList ;

            var user = await _context.MstrAgents
                .SingleOrDefaultAsync(x => x.cAgentName == loginDto.cAgentName);

            if (user == null) return Unauthorized("Invalid Username");

            var isActiveUser = await _context.MstrAgents
                .SingleOrDefaultAsync(x => x.cAgentName == loginDto.cAgentName && x.bActive == true);

            if (isActiveUser == null) return Unauthorized("User is inactive");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.cPassword));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            int userId = int.Parse(user.idAgents.ToString());
            int moduleId = int.Parse(loginDto.ModuleId.ToString()); 

            var usermod = await _context.MstrAgentModule
                    .SingleOrDefaultAsync(x => x.UserId == userId && x.SysModuleId == moduleId);

            if (usermod == null) return Unauthorized("Invalid Module");
            else
            {
                ///// GET USER LOCATION LIST 
                MstrAgentModule agentModule = new MstrAgentModule();
                agentModule.UserId = userId;
                agentModule.SysModuleId = moduleId;

                locationList = await _adminRepository.GetUserLocAsync(agentModule);

                /// GET PERMITED MENU LIST FOR LOGGED USER
                UserDto userDto = new UserDto();

                userDto.UserId = user.idAgents;
                userDto.ModuleId = usermod.SysModuleId;

                menuList = await _adminRepository.GetAuthMenuListAsync(userDto);

                // if (moduleId == 1) //Log to CCS System
                // {  
                //     menuList = await _masterRepository.GetAuthMenuListAsync(userDto); 
                // }
                // else if (moduleId == 2) // Log to PTrack System
                // {   
                //     menuList = await _pTrackMasterRepository.GetAuthMenuListAsyncPtrack(userDto);
                // }
                // else if (moduleId == 4) // Log to PTrack System
                //{   
                //    menuList = await _mTrackMasterRepository.GetAuthMenuListAsyncMtrack (userDto);
                //}
                // else if (moduleId == 3) 
                //  {
                //     menuList = await _mwsMasterRepository.GetAuthMenuListAsyncMWS(userDto);
                // }

            }

            return new UserDto
            {
                ModuleId = usermod.SysModuleId,
                UserId = user.idAgents,
                UserName = user.cAgentName,
                Locations = locationList,
                Token = _tokenService.CreateToken(user),
                permitMenus = menuList
            };
        }

        // [HttpPost("refresh")]
        // public async Task<ActionResult<UserDto>> Refresh(LoginDto loginDto)
        // {
        //     var user = await _context.MstrAgents
        //         .SingleOrDefaultAsync(x => x.cAgentName == loginDto.cAgentName);
        // }

        // private async Task<string> GetFactoryName(int id)
        // {
        //     var factory = await _context.MstrFactory
        //             .Where(x => x.AutoId == id)
        //             .Select(p => new {p.Factory})
        //             .SingleOrDefaultAsync();
        //     return factory.Factory;
        // }

        private async Task<bool> UserModuleExists(MstrAgentModule userModule)
        {
            return await _context.MstrAgentModule.AnyAsync(x => x.UserId == userModule.UserId && x.SysModuleId == userModule.SysModuleId);
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.MstrAgents.AnyAsync(x => x.cAgentName == username.ToLower());
        }

        
    }
}