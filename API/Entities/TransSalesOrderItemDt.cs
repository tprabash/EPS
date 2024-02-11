using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.SalesOrderItemDt")]
    public class TransSalesOrderItemDt
    {
        [Key]
        public long AutoId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public long SOHeaderId { get; set; }
        public int ArticleId { get; set; }
        public long CostingId { get; set; }
        public int ArticleColorSizeId { get; set; }
        public int BrandCodeId { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public bool IsIntendCreated { get; set; }
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; } 
        public virtual TransCostingHeader CostHeader {get; set;}
        public virtual MstrArticle MstrArticle {get; set;}
        public virtual MstrColor MstrColor {get; set;}
        public virtual MstrSize MstrSize {get; set;}
        public virtual TransSalesOrderHd SalesOrderHd {get; set;}
    }
}