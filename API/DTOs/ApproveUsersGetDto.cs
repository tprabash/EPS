using System.Collections.Generic;

namespace API.DTOs
{
    public class ApproveUsersGetDto
    {
        public virtual IEnumerable<ApproveUserDto> approveUsers { get; set; }
        public virtual IEnumerable<ApproveUserDto> userDetails { get; set; }

    }

    
}