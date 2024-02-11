using System;

namespace API.DTOs
{
    public class InvoiceNoListDto
    {
        public long InvoiceHdId {get;set;}
        public string InvoiceNo {get;set;}
        public string Attention {get;set;}
        public string InvCurrency {get;set;}
        public string VatNo {get;set;}
        public string TaxNo {get;set;}
        public DateTime TransDate {get;set;}
    }
}