using System;

namespace API.DTOs
{
    public class ReceiptNoListDto
    {
        public string ReceiptNo { get; set; }
        public decimal AllocatedTotal { get; set; }
        public string ReceiptType { get; set; }
        public string Paymode { get; set; }
        public decimal ReceiptTotal { get; set; }
        public DateTime DocDate { get; set; }
        public long ReceiptHdId { get; set; }
        public string Status {get;set;}
        public decimal ExchangeRate { get; set; }
        public string Currency { get; set; }

    }
}
