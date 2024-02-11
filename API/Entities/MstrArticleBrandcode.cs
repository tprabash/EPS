using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ArticleBrandCode")]
    public class MstrArticleBrandcode
    {
        [Key]
        public long AutoId { get; set; }        
        public long ArticleId { get; set; }
        public int BrandCodeId { get; set; }
        public long ? ColorId { get; set; }
        public long ? SizeId { get; set; }
    }
}
