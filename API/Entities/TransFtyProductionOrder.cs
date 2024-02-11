using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.FtyProductionOrder")]
    public class TransFtyProductionOrder
    {
        [Key]
        public long AutoId {get;set;}
        public long JobHeaderId {get;set;}
        public string FPONo {get;set;}
        public DateTime StartDate {get;set;}
        public DateTime EndDate {get;set;}
        public byte StatusId {get;set;}
        public string Remarks {get;set;}
        public int Qty {get;set;}
        public bool bActive {get;set;}        
        public long MINHeaderId {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
        public virtual TransJobHeader TransJobHeader {get; set;}
        public virtual MstrStatus MstrStatus {get; set;}
    }
}