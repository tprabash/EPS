using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class TranferGetDto
    {
        public virtual IEnumerable<TransTransferHeader> TranferHeader { get; set; }
        public virtual IEnumerable<TranferGetDetailsDto> TranferDetails { get; set; }
    }
}