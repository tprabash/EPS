using System;

namespace API.DTOs
{
    public class CreditNoteDispatchDto
    {
        public long DispatchDid {get;set;}
        public long InvoiceDid {get;set;}
        public string DispatchNo {get;set;}
        public int InvoiceQty {get;set;}
        public int DispatchQty {get;set;}
        public int DispatchSiteId {get;set;}
        public string SiteName {get; set;}
        public string FPONo {get;set;}
        public decimal UnitPrice {get;set;}
        public int ReturnedQty {get;set;}
        public int ReturnableQty {get;set;}
    }
}
