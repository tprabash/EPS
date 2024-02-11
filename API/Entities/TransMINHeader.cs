using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.MINHeader")]
    public class TransMINHeader
    {
        [Key]
        public long AutoId {get;set;}
        public string MINNo {get;set;}
        public long FPOId {get;set;}
        public int FPOQty {get;set;}
        public DateTime TransDate {get;set;}
        public bool bActive {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}