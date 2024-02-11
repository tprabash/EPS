using System;

namespace API.DTOs
{
    public class ReturnFPODetailsDto
    {
        public long AutoId {get;set;}
        public long JobHeaderId {get;set;}
        public string FPONo {get;set;}
        public DateTime StartDate {get;set;}
        public DateTime EndDate {get;set;}
        public string Status {get;set;}
        public string StatusId {get;set;}
        public string JobNo {get; set;}        
        public string Remarks {get;set;} 
        public int TotFPOQty { get; set; }       
        public string DeliveryRef { get; set; }
        public int SOItemDtId { get; set; }
        public int SODelivDtId { get; set; }
        public int FPOQty { get; set; }
        public int PrvFPOQty { get; set; }
        public int JobQty { get; set; }
        public int BalQty { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public int ColorId { get; set; }        
        public string Color { get; set; }
        public int SizeId { get; set; }
        public string Size { get; set; }
        public int CombinId { get; set; }     
        public string Combination { get; set; }    
        public long MINHeaderId {get;set;}
        public string MINNo {get;set;}  
        // public bool bIssued {get;set;} 
    }
}