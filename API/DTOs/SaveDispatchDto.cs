using System;
using System.Collections.Generic;
using API.Entities;


namespace API.DTOs
{
    public class SaveDispatchDto
    {
        public int Action { get; set; }
        public int LocationId { get; set; }
        public int ModuleId { get; set; }
        public int UserId { get; set; }
        public virtual List<TransDispatchDetailsDto> sDispatchDetails { get; set; }
        public virtual TransDispatchHeader sDispatchHeader { get; set; }
        public virtual TransDispatchAdditionalData sDispatchAdditionalData{ get; set; }

    }
}