using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class SavedTranferDto
    {
        public virtual TransTransferHeader TranferHeader { get; set; }
        public virtual List<TranferDetailsDto> TranferDetails { get; set; }
    }
}