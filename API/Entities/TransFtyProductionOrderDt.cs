using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.FtyProductionOrderDt")]
    public class TransFtyProductionOrderDt
    {
        [Key]
        public long AutoId {get;set;}
        public long FPOId {get;set;}
        public long SODelivDtId {get;set;}
        public int Qty {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
        public virtual TransFtyProductionOrder TransFtyProdOrder {get; set;}
        public virtual TransSalesOrderDeliveryDt SODelivDetail {get; set;}
    }
}