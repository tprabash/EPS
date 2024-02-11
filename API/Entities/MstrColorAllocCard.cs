using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ColorAllocCard")]
    public class MstrColorAllocCard
    {
        [Key]
        public int AutoId {get;set;}
        public byte ColorCardId {get;set;}
        public int ColorId {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}