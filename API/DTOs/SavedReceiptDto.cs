using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class SavedReceiptDto
    {
        public virtual TransReceiptHeader ReceiptHeader { get; set; }
        public virtual List<ReceiptDetailDto> ReceiptDetails { get; set; }
    }
}