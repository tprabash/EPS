using System;

namespace API.DTOs
{
    public class CostHeaderDto
    {
        public long AutoId { get; set; }
        public string RefNo { get; set; }  
        public string Combination { get; set; }
	    public int VersionControl {get;set;}
        public int NoOfUps {get;set;}        
        public int BrandCodeId {get;set;}
        public string BrandCode {get;set;}
        public int Tollerence  { get; set; }
        public decimal BoardLength {get;set;}
        public decimal BoardWidth {get;set;}
        public decimal SQM {get;set;}
        public int ReelSize {get;set;}	
        public decimal ActualReal {get;set;}
        public decimal TrimWaste {get;set;}
        public decimal TotNetWeight {get;set;}
        public decimal TotGrossWeight {get;set;}
        public decimal TotalBoxCost {get;set;}
        public decimal MOQCost {get;set;}
        public decimal Markup {get;set;}
        public int CustomerId { get; set; }      
        public decimal SellingPrice {get;set;}
        public decimal Commission {get;set;}
        public decimal TotMOQCost {get;set;}
        public decimal ProfitMarkup {get;set;}        
        public decimal CommSelPrice {get;set;}
        public DateTime TransDate {get;set;}
        public string Color { get; set; }
        public string ArticleName { get; set; }
        public string Size { get; set; }
        public bool isActive { get; set; }
        public string Status { get; set; }
        public long ArtColorSizeId { get; set; }
        //public string OrderRef { get; set; }
        //public string CustomerRef { get; set; }

    }
}