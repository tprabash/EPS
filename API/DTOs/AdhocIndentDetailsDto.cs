using API.Entities;
using System;

namespace API.DTOs
{
    public class AdhocIndentDetailsDto
    {
        public long IndentDetailId { get; set; }
        public long ArticleId { get; set; }
        public string IndentLineNo { get; set; }
        public long IndentHeaderId { get; set; }
        public int CategoryId { get; set; }
        public string IndentNo { get; set; }
        public string ArticleName { get; set; }
        public string StockCode { get; set; }
        public long ColorId { get; set; }
        public string Color { get; set; }
        public long SizeId { get; set; }
        public string Size { get; set; }
        public int UOMId { get; set; }
        public string Unit { get; set; }
        public int OpenQty { get; set; }
        public int OrderQty { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
