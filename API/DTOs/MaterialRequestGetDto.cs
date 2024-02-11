using API.Entities;
using System;

namespace API.DTOs
{
    public class MaterialRequestGetDto
    {
        public long MRHeaderId { get; set; }
        public string TransDate { get; set; }
        public string MRNo { get; set; }
        public int AssignedTo { get; set; }
        public int SiteId { get; set; }
        public string Remark { get; set; }
        public bool bActive { get; set; }
        public MRStatus StatusId { get; set; }
        public int CategoryId { get; set; }
        public int LocationId { get; set; }
        public long MRDetailsId { get; set; }
        public string ArticleName { get; set; }
        public string StockCode { get; set; }
        public long ArticleId { get; set; }
        public long ColorId { get; set; }
        public string Color { get; set; }
        public long SizeId { get; set; }
        public string Size { get; set; }
        public int ReqQty { get; set; }
        public int ApprovedQty { get; set; }
        public int UOMId { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public string RequireDate { get; set; }
        public string Location { get; set; }
        public string SiteName { get; set; }
        public string Category { get; set; }
        public string AgentName { get; set; }
    }
}
