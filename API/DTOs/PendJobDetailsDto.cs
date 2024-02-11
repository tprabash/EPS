using System;

namespace API.DTOs
{
    public class PendJobDetailsDto
    {          
        public string DeliveryRef { get; set; }
        public int SOItemDtId { get; set; }
        public int SODelivDtId { get; set; }
        public int FPOQty { get; set; }
        public int JobQty { get; set; }
        public int BalQty { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public int ColorId { get; set; }        
        public string Color { get; set; }
        public int SizeId { get; set; }
        public string Size { get; set; }
        public int CombinId { get; set; }     
        public string Combination { get; set; }        

    }
}