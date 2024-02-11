namespace API.DTOs
{
    public class TransProdDetailsDto
    {
        public long AutoId {get;set;}
        public long FPPODId {get;set;}
        public int TransType {get;set;}
        public int ValidationQty {get;set;}
        public int Qty {get;set;}
        public int ReceiveSiteId {get;set;}
        public int DispatchId {get;set;}
        public int RejReasonId {get;set;}
        public int CreateUserId {get;set;}
    }
}