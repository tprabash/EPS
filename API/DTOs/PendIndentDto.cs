namespace API.DTOs
{
    public class PendIndentDto
    {
        public long IndentHeaderId { get; set; }
        public long MRHeaderId { get; set; }
        public long MRDetailsId { get; set; }
        public long ArticleId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int Type { get; set; }
        public string IndentNo { get; set; }
        public string DispatchNo { get; set; }
        public string ArticleName { get; set; }
        public string StockCode { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Unit { get; set; }
        public int OpenQty { get; set; }
        public int IndentDetailId { get; set; }
        public int UOMId { get; set; }
        public string TransDate { get; set; }
        public decimal UnitPrice { get; set; }
        public int OrderQty { get; set; }
        public string IndentLineNo { get; set; }
    }
}
