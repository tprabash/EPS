using System;

namespace API.Entities
{
    public class TransStockAdjuestment
    {
        public int AutoId { get; set; }
        public long? ArticleId { get; set; }
        public long? ColorId { get; set; }
        public long SizeId { get; set; }
        public int SiteId { get; set; }
        public DateTime? TransDate { get; set; }
        public int StockQty { get; set; }
        public decimal Price { get; set; }
        public string ExpireDate { get; set; }
        public string Remarks { get; set; }
        public int? ReasonId { get; set; }
        public int? CreatedUserId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
