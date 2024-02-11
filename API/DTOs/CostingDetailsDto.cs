using System;

namespace API.DTOs
{
    public class CostingDetailsDto
    {        
        public long CostHeaderId {get;set;}
        public string RefNo {get;set;}
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
        public int GSM {get;set;}
        public int FluteId {get;set;}
        public string FluteType {get;set;}
        public decimal Factor {get;set;}
        public string Base {get;set;}
        public int UOMId {get;set;}
        public string UOM {get;set;}
        public decimal ConsBase {get;set;}
        public int MultCon {get;set;}
        public decimal BaseValue {get;set;}
        public int ArtiUOMConvId {get;set;}
        public decimal NetCon {get;set;}
        public decimal Wastage {get;set;}
        public decimal GrossCon {get;set;}
        public decimal CostPcs {get;set;}
        public decimal Cost {get;set;}         
        

    }
}