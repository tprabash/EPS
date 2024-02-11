using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Entities.Admin;

namespace API.Entities
{
    [Table("Trans.ApproversByModule")]
    public class TransApproversByModule
    {
        [Key]
        public int AutoId {get;set;}
        public int ARMId {get;set;}
        public int UserId {get;set;}
        public bool isDefault {get;set;}
        public bool isFinalApprove {get;set;}  
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}              
        public virtual TransApprovalRoutingModules ApproveRotModule { get; set; }
        public virtual MstrAgents Agents { get; set; }
    }
}