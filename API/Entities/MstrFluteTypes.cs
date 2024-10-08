using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.FluteTypes")]
    public class MstrFluteTypes
    {
        [Key]
        public int AutoId {get;set;}
        public string Code {get;set;}
        [Column(TypeName = "decimal(18,4)")]
        public decimal Factor {get;set;}
        public bool IsActive {get;set;}
        public int LocationId {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}