using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.InvoiceDetails")]
    public class TransInvoiceDetails
    {
        [Key]
        public long AutoId {get;set;}
        public long InvoiceHdId {get;set;}
        public long DispatchDtId {get;set;}
        public long SOItemDtId {get;set;}
        public string Description {get;set;}
        public int Qty {get;set;}
        public int UOM {get;set;}
        public decimal UnitPrice {get;set;}
        public decimal Value {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
        public TransInvoiceHeader InvoiceHeader { get; set; }
    }
}