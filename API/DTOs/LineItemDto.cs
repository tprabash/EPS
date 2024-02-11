using System;

namespace API.DTOs
{
    public class LineItemDto
    {
        public string PONo { get; set; }
        public string ColorCode { get; set; }
        public string PurchaseOrderItem {get;set;}
        public string MaterialCode {get;set;}
        public string VendorMaterial {get;set;}
        public string RefMaterial {get;set;}
        public string RefMaterial2 {get;set;}
        public string ItemText {get;set;}
        public string MatPoText {get;set;}
        public string PageFormat {get;set;}
        public string MaterialDescription {get;set;}
        public string SalesOrder {get;set;}
        public string SalesOrderItem {get;set;}
        public DateTime DeliveryDate {get;set;}
        public string Quantity {get;set;}
        public string UOM {get;set;}
        public string NetPrice {get;set;}        
        public string Per {get;set;}
        public string Text {get;set;}
        public string ProductType {get;set;}
        public string Gender {get;set;}
    }
}