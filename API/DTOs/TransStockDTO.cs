namespace API.DTOs
{
    public class TransStockDTO
    {
        public long AutoId { get; set; }
        public string ArticleName { get; set; }
        public string StockCode { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string UOM { get; set; }
        public int OrderQty { get; set; }
        public int GRNQty { get; set; }
        public int StockInQty { get; set; }
        public int MRRaisedQty { get; set; }
        public int PendRecivedQty { get; set; }
    }
}
