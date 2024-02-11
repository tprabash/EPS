using System;

namespace API.DTOs
{
    public class SalesOrderRetDto
    {
        public int SOHeaderId { get; set; }
        public string OrderRef { get; set; }
        public string CustomerRef { get; set; }
        public int CustomerId { get; set; }
        public int CustomerLocId { get; set; }
        public int CustomerUserId {get;set;}
        public int CusCurrencyId {get;set;}
        public int SalesCategoryId {get;set;}
        public int CurrencyId {get;set;}
        public int CountryId {get;set;}
        public int FileId {get;set;}
        public string FileName { get; set; }
        public int PaymentTermId {get;set;}
        public long ArticleId {get;set;}
        public int SalesAgentId {get;set;}
        public bool IsChargeable {get;set;}
        public int CustomerDivId {get;set;}
        public DateTime TrnsDate { get; set; }
        public DateTime DelDate { get; set; }
        public int SOItemId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int SaleOrderId { get; set; }       
        public string Article { get; set; }
        public int BrandCodeId { get; set; }       
        public string BrandCode { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int CostingId { get; set; }
        public int ItemQty { get; set; }
        public int ArticleColorSizeId { get; set; }
        public decimal Price { get; set; }
        public bool IsIntendCreated { get; set; }
        public int SODelId { get; set; }
        public int DeliCustLocId { get; set; }
        public string DeliveryRef { get; set; }
        public string Category { get; set; }
        public string UnitCode { get; set; }
        public string ProdGroupName { get; set; }        
        public string ProdTypeCode { get; set; }
        public string StockCode { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string CusDelLoc { get; set; }        
        public DateTime DeliveryDate { get; set; }
        public int DelQty { get; set; }
        public int JobQty { get; set; }
        public bool JobCreated { get; set; }
        public string CostingRef { get; set; }
        public DateTime exchDate { get; set; }
    }
}