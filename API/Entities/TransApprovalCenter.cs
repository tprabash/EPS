using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.ApprovalCenter")]
    public class TransApprovalCenter
    {
        [Key]
        public int AutoId {get;set;}
        public string ModuleName {get;set;}
        public int AssigneUser {get;set;}
        public int RequestedBy {get;set;}
        public long RefId {get;set;}
        public string RefNo {get;set;}
        public DateTime TransDate {get;set;}
        public string Remarks {get;set;}
        public string Details {get;set;}
        public DateTime StatusDate {get;set;}
        public string Status {get;set;}
        public bool IsFinal {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}

    }
}