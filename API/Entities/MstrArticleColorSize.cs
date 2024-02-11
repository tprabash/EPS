using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ArticleColorSize")]
    public class MstrArticleColorSize
    {
        [Key]
        public long AutoId {get;set;}
        public long ArticleId {get;set;}
        public int ColorId {get;set;}
        public int SizeId {get;set;}
        public int CostingId {get;set;}
        public int ShortOrder {get;set;}
        public int QtyInStock {get;set;}
        [Column(TypeName = "decimal(18,4)")]
        public decimal AvgCostPrice {get;set;}
        [Column(TypeName = "decimal(18,4)")]
        public decimal LastCostPrice {get;set;}
        [Column(TypeName = "decimal(18,4)")]
        public decimal MaxCostPrice {get;set;}
        public DateTime PODate {get;set;}
        [Column(TypeName = "decimal(18,4)")]
        public decimal QtyOnOrder {get;set;}
        [Column(TypeName = "decimal(18,4)")]
        public decimal QtyAllocated {get;set;}
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; }
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; }
        public virtual MstrArticle MstrArticle { get; set; }
        public virtual MstrColor MstrColor { get; set; }
        public virtual MstrSize MstrSize { get; set; }

    }
}