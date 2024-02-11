using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class TransProductionDetails
    {
        [Key]
        public long AutoId {get;set;}
        public long FPPODId {get;set;}
        public DateTime TransDate {get;set;}
        public int TransType {get;set;}
        public int ValidationQty {get;set;}
        public int Qty {get;set;}
        public int ReceiveSiteId {get;set;}
        public int DispatchId {get;set;}
        public bool ? IsActive {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
        public TransFtyProductionProcessOrderDt FPPODetails { get; set; }
        public MstrStoreSite ReceiveSite { get; set; }
        public MstrStoreSite DispatchSite { get; set; }

    }
}