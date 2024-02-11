using System.Collections.Generic;
using API.Entities;

namespace API.DTOs.MTrack
{
    public class MachineCommonDto
    {
        public virtual IEnumerable<MachineBreakDto> MachineBreakList { get; set; }
        public virtual IEnumerable<MachineRepairDto> MachineRepairList { get; set; }

    }
}