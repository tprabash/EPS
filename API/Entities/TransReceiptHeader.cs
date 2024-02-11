using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.ReceiptHeader")]
    public class TransReceiptHeader
    {
        [Key]
        public long AutoId {get;set;}
        public string ReceiptNo {get;set;}
        public int BankId {get;set;}
        public int CustomerId {get;set;}
        public string Reference {get;set;}
        public DateTime DocDate {get;set;}
        public int DefaultCurrencyId {get;set;}
        public int PaymodeId {get;set;}
        public int ReceiptTypeId {get;set;}
        public decimal ReceiptTotal {get;set;}
        public decimal ExchangeRate {get;set;}
        public decimal AllocatedTotal {get;set;}
        public bool IsActive {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
        public int CustomerBankId {get;set;}
    }
}