using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.SalesOrderDeliveryDt")]
    public class TransSalesOrderDeliveryDt
    {
        [Key]
        public long AutoId { get; set; }
        public long SOItemDtId { get; set; }        
        public long SaleHeaderId { get; set; }
        public string DeliveryRef { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int ArticleColorSizeId { get; set; }
        public int CustomerLocId { get; set; }
        public int Qty { get; set; }
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; } 
        public virtual TransSalesOrderHd SalesOrderHd {get; set;}
        public virtual TransSalesOrderItemDt SalesItemDt {get; set;}
    }
}