using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.SizeAllocCard")]
    public class MstrSizeAllocCard
    {
        [Key]
        public int AutoId {get;set;}
        public byte SizeCardId {get;set;}
        public int SizeId {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}