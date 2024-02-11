namespace API.DTOs
{
    public class GRNReceiveDetails
    {
        public long PODetailsId { get; set; }
        public long POHeaderId { get; set; }
        public long ArticleId { get; set; }
        public long ColorId { get; set; }
        public long SizeId { get; set; }
        public int UOMId { get; set; }
        public decimal UnitPrice { get; set; }
        public int ReceivedQty { get; set; }
        public int RejectQty { get; set; }
        public int FOCQty { get; set; }
        public int POQty { get; set; }
        public int PrevReceivedQty { get; set; }
        public string ArticleName { get; set; }
        public string StockCode { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Unit { get; set; }
    }
}
