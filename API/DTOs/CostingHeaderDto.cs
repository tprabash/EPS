using System;

namespace API.DTOs
{
    public class CostingHeaderDto
    {
        public long ArticleId { get; set; }
        public string RefNo { get; set; }
        public int CustomerId { get; set; }
        public long AutoId { get; set; }
        public string ArticleName { get; set; }
        public string StockCode { get; set; } 
        public string Measurement { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int CombinId { get; set; }
        public string Combination { get; set; }
	    public int VersionControl {get;set;}
        public int NoOfUps {get;set;}
        public int BrandCodeId {get;set;}
        public string BrandCode {get;set;}
        public byte PDHeaderId {get;set;}
        public decimal Tollerence  { get; set; }
        public decimal Length {get;set;}
        public decimal Width {get;set;}
        public decimal Height {get;set;}
        public decimal BoardLength {get;set;}
        public decimal BoardWidth {get;set;}
        public decimal SQM {get;set;}
        public int ReelSize {get;set;}	
        public decimal TotalBoxCost {get;set;}
        public decimal MOQCost {get;set;}
        public decimal Markup {get;set;}        
        public decimal SellingPrice {get;set;}
        public decimal Commission {get;set;}
        public decimal TotMOQCost {get;set;}
        public decimal ProfitMarkup {get;set;}        
        public decimal CommSelPrice {get;set;}
        public decimal ActualReal {get;set;}
        public decimal TrimWaste {get;set;}
        public decimal TotNetWeight {get;set;}
        public decimal TotGrossWeight {get;set;}
        public DateTime TransDate {get;set;}
        public bool IsActive {get;set;}
        public string Status { get; set; }
        public int CartonTypeId { get; set; }
        public int DimensionId { get; set; }
    }
}