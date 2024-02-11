namespace API.DTOs
{
    public class PendDispatchDto
    {
        public int AutoId {get;set;} /// row number
        public long SOItemId {get;set;}
        public long SODelivDtId {get;set;}
        public string DeliveryRef {get;set;}
        public string OrderRef {get;set;}
        public string ArticleName {get;set;}
        public string Color {get;set;}
        public string Size {get;set;}
        public int ProdQty {get;set;}
        public int DispatchedQty {get;set;}
        public int BalQty {get;set;}
        public int DispatchSiteId {get;set;}
        public int CustomerId {get;set;}
        public string CustomerRef {get;set;}

    }
}