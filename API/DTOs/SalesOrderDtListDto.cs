using System;

namespace API.DTOs
{
    public class SalesOrderDtListDto
    {
        public int SOHeaderId { get; set; }
        public string OrderRef { get; set; }
        public string CustomerRef { get; set; }        
        public long ArticleId { get; set; }    
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int SaleOrderId { get; set; }
        public string Article { get; set; }
        public string StockCode { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }       
        public int SODelId { get; set; }       
        public string DeliveryRef { get; set; }
        public string Category { get; set; }
        public string UOM { get; set; }
        public int UOMId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int DelQty { get; set; }
       
    }
}
