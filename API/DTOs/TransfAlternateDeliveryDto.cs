namespace API.DTOs
{
    public class TransfAlternateDeliveryDto
    {
        public long FPPODID { get; set; }
        public long SoHeaderId { get; set; }
        public long SoDelvId { get; set; }
        public long soItemId { get; set; }
        public long AutoId { get; set; }
        public string CustomerRef { get; set; }
        public string OrderRef { get; set; }             
        public string ArticleName { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string BrandCode { get; set; }
        public int RequireQty { get; set; }
        public string DeliveryRef { get; set; }
        public string Process { get; set; }
        public int ArticleId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int BrandCodeId { get; set; }
        public int ProcessId { get; set; }
    }
}