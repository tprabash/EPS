using System;

namespace API.DTOs
{
    public class DebitNoteListDto
    {   
        public long idDebitNoteHd { get; set; }
        public string ReferenceNo { get; set; }
        public decimal Allocatedamount { get; set; }
        public string Reason { get; set; }
        public decimal Totalamount { get; set; }
        public DateTime DOCDate { get; set; }
        public bool Status {get;set;}
        public decimal Exrate { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public int ReasonId { get; set; }
    }
}