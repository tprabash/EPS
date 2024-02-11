using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.SalesOderHd")]
    public class TransSalesOrderHd
    {
        [Key]
        public long AutoId { get; set; }
        public string OrderRef { get; set; }
        public string CustomerRef { get; set; }
        public int CustomerId { get; set; }       
        public int CustomerLocId {get;set;}
        public int CustomerUserId {get;set;}
        public int SalesCategoryId {get;set;}
        public int CusCurrencyId {get;set;}
        public int CountryId {get;set;}
        public int PaymentTermId {get;set;}
        public long ArticleId {get;set;}
        public int SalesAgentId {get;set;}
        public bool IsChargeable {get;set;}
        public DateTime ? Exchdate { get; set; } 
        public int CustomerDivId {get;set;}
        public DateTime TrnsDate { get; set; }
        public DateTime DelDate { get; set; }
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; } 
        public virtual MstrCustomerHeader MstrCustomerHeader { get; set; }
    }
}