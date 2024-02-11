namespace API.DTOs
{
    public class DispatchStockRespondDto
    {
        public long StockId { get; set; }
        public long ArticleId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public string ArticleName { get; set; }
        public string StockCode { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string IndentNo { get; set; }
        public string MRNo { get; set; }
        public long MRHeaderId { get; set; }
        public string GRNDate { get; set; }
        public long GRNHeaderId { get; set; }
        public string GRNNo { get; set; }
        public int StockQty { get; set; }
        public long IndentDetailId { get; set; }
        public long GRNDetailsId { get; set; }
        public int Price { get; set; }

    }
}
