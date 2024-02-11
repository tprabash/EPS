using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ArticleCompAllocation")]
    public class MstrArticleCompAllocation
    {
        [Key]
        public long AutoId {get;set;}
        public long ArticleId {get;set;}
        public int CompanyId {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}