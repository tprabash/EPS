namespace API.DTOs
{
    public class ReceiptDetailDto
    {
        public long AutoId {get;set;}
        public long InvoiceHdId { get; set; }
        public string InvoiceNo { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal OutstandAmount { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal ForeignOutstand { get; set; }
        public decimal ForeignDeposit { get; set; }

    }
}