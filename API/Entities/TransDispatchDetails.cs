using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.DispatchDetails")]
     public class TransDispatchDetails
    {
        [Key]
        public long AutoId {get;set;}
        public long DispatchHdId {get;set;}
        public long SOItemId {get;set;}
        public long SODelivDtId {get;set;}
        public long GRNHeaderId { get; set; }
        public long GRNDetailId { get; set; }
        public long StockId { get; set; }
        public int ProducedQty {get;set;}
        public int DispatchedQty {get;set;}
        public int BalDispatchQty {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
        public virtual TransDispatchHeader DispatchHeader { get; set; }
        public virtual TransSalesOrderItemDt SalesOrderItem { get; set; }
        public virtual TransSalesOrderDeliveryDt SalesOrderDelivery { get; set; }
    }
}