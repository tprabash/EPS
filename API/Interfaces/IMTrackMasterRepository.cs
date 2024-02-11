using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.DTOs.MTrack;
using API.Entities.MTrack;
using API.Entities.Admin;

namespace API.Interfaces
{
    public interface IMTrackMasterRepository
    {
        Task<IEnumerable<PermitMenuDto>> GetAuthMenuListAsyncMtrack (UserDto userDto);
        Task<MachineCommonDto> GetFactoryWiseMachineBreakAsync(string location);
        Task<int> UpdateMachineBreakAsync(MachineBreakUpdateDto machine);
        Task<IEnumerable<SampleDetailsGetDto>> GetSampleDetailsAsync(SampleDto sample);
    }
}