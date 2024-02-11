using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class ReceiptDto
    {
        public virtual IEnumerable<TransReceiptHeader> ReceiptHeader { get; set; }
        public virtual IEnumerable<ReceiptDetailDto> ReceiptDetails { get; set; }
    }
}