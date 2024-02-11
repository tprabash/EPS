namespace API.DTOs
{
    public class TransfairableDeliveryDto
    {
        public long FPPOID { get; set; }
        public string CustomerRef { get; set; }
        public string OrderRef { get; set; }             
        public string ArticleName { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string BrandCode { get; set; }
        public int TranQty { get; set; }
        public string DelRef { get; set; }
        public decimal Price { get; set; }
        public int ArticleId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int BrandCodeId { get; set; }
        public long SoHeaderId { get; set; } 
        public int DispatchSiteId { get; set; } 
    }
}