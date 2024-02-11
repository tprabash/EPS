using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.FtyProductionProcessOrder")]
    public class TransFtyProductionProcessOrder
    {
        [Key]
        public long AutoId {get;set;}
        public long FPOId {get;set;}
        public int ProcessId {get;set;}
        public DateTime StartDate {get;set;}
        public DateTime EndDate {get;set;}
        public int Qty {get;set;}
        public bool bActive {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
        public virtual TransFtyProductionOrder TransFtyProdOrder {get; set;}
        public virtual MstrProcess MstrProcess {get; set;}
    }
}