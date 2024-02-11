using System;

namespace API.DTOs
{
    public class PendInvoiceDto
    {
        public long DispatchDtId { get; set; }
        public long SOItemId { get; set; }
        public long ArticleId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public string OrderRef { get; set; }
        public string DispatchNo { get; set; }
        public string ArticleName { get; set; }
        public string StockCode { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string UOM { get; set; }
        public int MeasurementId { get; set; }
        public decimal Price { get; set; }
        public int BalQty { get; set; }
        public decimal Value { get; set; }
        public string CustomerRef { get; set; }
        public string CostNo { get; set; }
        public int CostingId { get; set; }
        public DateTime DispatchDate { get; set; }

    }
}