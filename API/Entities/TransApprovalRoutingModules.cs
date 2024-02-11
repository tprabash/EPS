using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Entities.Admin;

namespace API.Entities
{
    [Table("Trans.ApprovalRoutingModules")]
    public class TransApprovalRoutingModules
    {
        [Key]
        public int AutoId {get;set;}
        public int UserId {get;set;}
        public int MenuId {get;set;}
        public bool BuyPass {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
        public virtual MstrAgents Agents { get; set; }
        public virtual MstrMenuList Menus { get; set; }
    }
}