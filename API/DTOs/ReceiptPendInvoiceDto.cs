using System;

namespace API.DTOs
{
    public class ReceiptPendInvoiceDto
    {
        public long InvoiceHdId {get;set;}
        public string Reference {get;set;}
        public DateTime TransDate {get;set;}
        public int CurrencyId {get;set;}
        public decimal ExchangeRate {get;set;}
        public decimal InvoiceAmount {get;set;}
        public decimal OutstandingAmount {get;set;}
        public decimal ForeignDebit {get;set;}
        public decimal ForeignOutstand {get;set;}
        public int AgeDays {get;set;}
    }
}