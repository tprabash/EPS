using System;

namespace API.DTOs
{
    public class SalesItemDto
    {
        public long AutoId { get; set; }
        public string ArticleName { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }  
        public string BrandCode { get; set; }   
        public int BrandCodeId { get; set; }   
        public int Qty {get;set;}
        public decimal Price {get;set;}
        public long CostingId {get;set;}
        public long ArticleColorSizeId { get; set; }
        public string RefNo {get;set;}
        
    }
}