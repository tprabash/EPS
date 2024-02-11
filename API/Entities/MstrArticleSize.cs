using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ArticleSize")]
    public class MstrArticleSize
    {
        [Key]
        public int AutoId { get; set; }        
        public long ArticleId { get; set; }
        public int SizeId { get; set; }
        public int LocationId { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public virtual MstrArticle MstrArticle { get; set; }
        public virtual MstrSize MstrSize { get; set; }
        public virtual MstrLocation MstrLocation { get; set; }
    }
}