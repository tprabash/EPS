using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.ReceiptAllocation")]
    public class TransReceiptAllocation
    {
        [Key]
        public long AutoId {get;set;}
        public long ReceiptHdId {get;set;}
        public long InvoiceHdId {get;set;}
        public decimal OutStandingAmt {get;set;}
        public decimal ExchangeRate {get;set;}
        public decimal DepositAmount {get;set;}
        public decimal ForeignDeposit {get;set;}
        public DateTime TransDate {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}