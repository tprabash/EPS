using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities.MWS;

namespace API.Interfaces
{
    public interface IMWSMasterRepository
    {
        Task<IEnumerable<PermitMenuDto>> GetAuthMenuListAsyncMWS(UserDto userDto);
    }
}
