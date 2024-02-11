using System;

namespace API.DTOs
{
    public class PurchaseOrderDto
    {
        public string PurchaseOrderNumber {get;set;}
        public string PurchaseOrderVersion {get;set;}
        public DateTime PurchaseOrderDate {get;set;}
        public DateTime LastRevisionDate {get;set;}
        public string Currency {get;set;}
        public string ConsigneeName {get;set;}
        public string ConsigneeAdd1 {get;set;}
        public string ConsigneeAdd2 {get;set;}
        public string ContactPerson {get;set;}
        public string ContactTel {get;set;}
        public string ContactEMail {get;set;}
        public string SalesPer {get;set;}
        public string SupplierName {get;set;}
        public string SupplierAdd1 {get;set;}
        public string SupplierAdd2 {get;set;}
        public string SupplierTel {get;set;}
        public string SupplierFax {get;set;}
        public string BillToName {get;set;}
        public string BillToName2 {get;set;}
        public string BillToAdd1 {get;set;}
        public string BillToAdd2 {get;set;}
        public string NotifyName {get;set;}
        public string NotifyName2 {get;set;}
        public string NotifyAdd1 {get;set;}
        public string NotifyAdd2 {get;set;}
        public string CusField1 {get;set;}
        public string EndBuyerAccnt {get;set;}
        public string EndBuyer {get;set;}
        public string EndBuyerCode {get;set;}
        public string DeliveryAddress {get;set;}


    }
}