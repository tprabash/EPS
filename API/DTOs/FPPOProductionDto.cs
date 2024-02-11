using System;

namespace API.DTOs
{
    public class FPPOProductionDto
    {
        public long FPPODId {get;set;}
        public string FPONo {get;set;}
        public DateTime DeliveryDate {get;set;}        
        public string DeliveryRef {get;set;}
        public string OrderRef {get;set;}        
        public string Process {get;set;}       
        public int ReceiveSiteId {get;set;}  
        public string ReceiveSite {get;set;}      
        public int DispatchId {get;set;}        
        public string DispatchName {get;set;}
        public int ReqQty {get;set;}
        public int InQty {get;set;}        
        public int BalQty {get;set;}
        public int PrvOutQty {get;set;}        
        public int DamageQty {get;set;}
        public string JobNo {get;set;}
        public string CustomerRef {get;set;}
        public string ArticleName {get;set;}
        public string Color {get;set;}
        public string Size {get;set;}
        public bool LastProcess {get;set;}
        public int OutQty {get;set;}
        
    }
}