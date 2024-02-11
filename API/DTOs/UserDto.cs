using System.Collections.Generic;


namespace API.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }        
        public int ModuleId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public IEnumerable<UserLocationDto> Locations { get; set; }
        public IEnumerable<PermitMenuDto> permitMenus { get; set; }

    }
}