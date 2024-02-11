using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Reason")]
    public class MstrReason
    {
        [Key]
        public int AutoId {get;set;}
        public int ModuleId {get;set;}
        public string Module {get;set;}
        public string Reason {get;set;}
        public bool IsActive { get; set; }
        public int LocationId {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}
