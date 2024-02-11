using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
     [Table("Trans.FtyProductionProcessOrderDetails")]
    public class TransFtyProductionProcessOrderDt
    {
        [Key]
        public long AutoId {get;set;}
        public long FPPOId {get;set;}
        public long SODelivDtId {get;set;}
        // public byte TypeId {get;set;}
        public int ReqQty {get;set;}
        public int InQty {get;set;}        
        public int OutQty {get;set;}        
        public int DamageQty {get;set;}
        public int ReceiveSiteId {get;set;}        
        public int DispatchId {get;set;}
        public bool bActive {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
        public virtual TransFtyProductionProcessOrder TransFtyProdProcessOrder {get; set;}
        public virtual TransSalesOrderDeliveryDt TransSalesOrderDeliveryDt {get; set;}
        public virtual MstrStoreSite ReceiveSite {get; set;}
        public virtual MstrStoreSite DispatchSite {get; set;}
    }
}