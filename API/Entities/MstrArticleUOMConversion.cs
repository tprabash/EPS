using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ArticleUOMConversion")]
    public class MstrArticleUOMConversion
    {
        [Key]
        public long AutoId {get;set;}
        public byte Version {get;set;}
        public long ArticleId {get;set;}
        public int UnitId {get;set;}

        [Column(TypeName = "decimal(9,4)")]
        public decimal Value {get;set;}
        public bool IsActive {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
        
    }
}