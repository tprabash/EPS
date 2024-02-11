namespace API.DTOs
{
    public class GRNDetailsDto
    {
        public long GRNDetailsId { get; set; }
        public long GRNHeaderId { get; set; }
        public long POHeaderId { get; set; }
        public string PONo { get; set; }
        public long PODetailsId { get; set; }
        public long ArticleId { get; set; }
        public string ArticleName { get; set; }
        public string StockCode { get; set; }
        public long ColorId { get; set; }
        public string Color { get; set; }
        public long SizeId { get; set; }
        public string Size { get; set; }
        public int UOMId { get; set; }
        public string Unit { get; set; }
        public int POQty { get; set; }
        public decimal PORate { get; set; }
        public int CurrencyId { get; set; }
        public string Currency { get; set; }
        public int ReturnedQty { get; set; }
        public int ReceivedQty { get; set; }
        public int PreReceivedQty { get; set; }
        public int PayableQty { get; set; }
        public int FOCQty { get; set; }
        public int RejectedQty { get; set; }
        public int IssuedQty { get; set; }
        public int StorageUnitId { get; set; }
        public int UnitConvId { get; set; }
    }
}
