namespace API.DTOs
{
    public class PODetailsDto
    {
        public long IndentDetailId { get; set; }
        public long ArticleId { get; set; }
        public long ColorId { get; set; }
        public long SizeId { get; set; }
        public int OpenQty { get; set; }
        public int OrderQty { get; set; }
        public int OrderedQty { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitId { get; set; }
        public string Unit { get; set; }
        public string ArticleName { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string IndentNo { get; set; }
        public string IndentLineNo { get; set; }

    }
}
