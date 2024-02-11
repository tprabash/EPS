using System;

namespace API.DTOs
{
    public class CreditNoteAllListDto
    {   public int AutoId { get; set; }
        public int CustomerId { get; set; }
        public string CreditNoteNo { get; set; }
        public decimal AllocatedTotal { get; set; }
        public decimal CreditNoteTotal { get; set; }
        public DateTime TransDate { get; set; }
        public decimal ExchangeRate { get; set; }
        public string Currency { get; set; }
        public bool Status { get; set; }
    }
}