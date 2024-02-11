using API.Entities;
using System.Collections.Generic;

namespace API.DTOs
{
    public class TransDispatchDto
    {
        public virtual List<TransDispatchDetailsDto> DispatchDetails { get; set; }
        public virtual TransDispatchHeader DispatchHeader { get; set; }
    }
}
