using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.SalesAgent")]
    public class MstrSalesAgent
    {
        [Key]
        public int AutoId {get;set;}
        public string Name {get;set;}        
        public string Email {get;set;}
        public int LocationId {get;set;}
        public bool bActive {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}        
        public virtual MstrLocation Location {get; set;}
    }
}