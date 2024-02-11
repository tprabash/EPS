using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.MWS;

namespace API.DTOs
{
    public class SaveBlockBookingDto
    {

        public int ActiveCode  { get; set; }
        public int ModuleId { get; set; }
        public int LocationId { get; set; }
        public virtual   TransBlockBookingHeader  sBBHeader  { get; set; }  
        public virtual   TransBlockBookingDetails  sBBDetails { get; set; }
        
    }
}
