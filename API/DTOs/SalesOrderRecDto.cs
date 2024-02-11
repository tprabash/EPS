using System;

namespace API.DTOs
{
    public class SalesOrderRecDto
    {
        public int AutoId { get; set; }
        public string OrderRef { get; set; }
        public string CustomerRef { get; set; }
        public int CustomerId { get; set; }
        public int CustomerDtId { get; set; }
        public DateTime DelDate { get; set; }
        public int UserId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int SaleOrderId { get; set; }
        public int ArticleId { get; set; }
        public int CostingId { get; set; }   
        public int CustomerLocId {get;set;}
        public int CustomerUserId {get;set;}
        public int SalesCategoryId {get;set;}
        public int CurrencyId {get;set;}
        public int CountryId {get;set;}
        public int PaymentTermId {get;set;}
        public int SalesAgentId {get;set;}
        public bool IsChargeable {get;set;}
        public int BillToId {get;set;}
        public DateTime TrnsDate { get; set; }
        public int Qty { get; set; }        
        public int price { get; set; }
        public bool IsIntendCreated { get; set; }
        public int SOItemDtId { get; set; }
        public string DeliveryRef { get; set; }
        public string status { get; set; }
        public DateTime DeliveryDate { get; set; }


    }
}