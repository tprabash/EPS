using System;

namespace API.DTOs
{
    public class CreditNoteListDto
    {
        public long Autoid { get; set; }
        public string ReferenceNo { get; set; }
        public string CreditRtnType { get; set; }
        public DateTime Transdate { get; set; }
        public string currencycode { get; set; }
        public decimal CrAmt { get; set; }
        public string isActive { get; set; }   
    }
}
