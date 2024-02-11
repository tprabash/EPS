using System;

namespace API.DTOs
{
    public class MINDetailsDto
    {
        public long CostHeaderId {get;set;}
        public string RefNo {get;set;}
        public string StockCode {get;set;}
        public long CostDetailId {get;set;}
        public byte CostGroupId {get;set;}
        public string CostGroup {get;set;}
        public int GroupOrder {get;set;}
        public long ArticleId {get;set;}        
        public string ArticleName {get;set;}
        public int ColorId {get;set;}
        public string Color {get;set;}
        public int SizeId {get;set;}        
        public string Size {get;set;}        
        public int UnitId {get;set;}
        public string Unit {get;set;}       
        public decimal GrossCon {get;set;}
        public int FPOQty {get;set;}
        public int RequiredQty {get;set;}
        public DateTime TransDate {get;set;}        
        public int IssuedQty {get;set;}
    }
}