namespace API.DTOs
{
    public class TransInvStockDTO
    { 
        public long AutoId { get; set; }
        public long StockId { get; set; }
        public long ArticleId { get; set; }
        public int UnitId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int MeasurementId { get; set; }
        public string ArticleName { get; set; }
        public string StockCode { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string UnitCode { get; set; }
        public string Measurement { get; set; }
        public decimal Price { get; set; }
        public int StockQty { get; set; }
    }
}
