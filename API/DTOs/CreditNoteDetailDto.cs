namespace API.DTOs
{
    public class CreditNoteDetailDto
    {
        public long AutoId {get;set;}
        public long InvoiceHdId { get; set; }
        public string InvoiceNo { get; set; }
        public string DispatchNo { get; set; }
        public decimal amount { get; set; }
        public decimal allocatedamount { get; set; }
        public int qty { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal Outstand { get; set; }
        public decimal ForeignOutstand { get; set; }
    }
}
