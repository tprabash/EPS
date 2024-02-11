namespace API.DTOs
{
    public class DebitNoteDetailDto
    {
        public long idDebitNoteDetails {get;set;}
        public long InvoiceHdId { get; set; }
        public string InvoiceNo { get; set; }
        public decimal OutstandAmount { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal ForeignOutstand { get; set; }
        public decimal ForeignDeposit { get; set; }
        public decimal InvoiceAmount { get; set; }
        public string ReferenceNo { get; set; }
    }
}
