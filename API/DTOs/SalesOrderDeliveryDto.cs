using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class SalesOrderDeliveryDto
    {
        public int AutoId { get; set; }
        public int SOItemDtId { get; set; }        
        public int SaleHeaderId { get; set; }
        public string DeliveryRef { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int ArticleColorSizeId { get; set; }
        public int CustomerLocId { get; set; }
        public int Qty { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }        
        public long ArticleId { get; set; }
        public virtual TransSalesOrderHd SalesOrderHd {get; set;}
        public virtual List<TransSalesOrderItemDt> SalesItemDt {get; set;}
    }
}