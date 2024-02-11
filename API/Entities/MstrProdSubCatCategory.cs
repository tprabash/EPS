using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ProdSubCatCategory")]
    public class MstrProdSubCatCategory
    {
        [Key]
        public int AutoId { get; set; }         
        public int SubCatId { get; set; }
        public int CatId { get; set; }
        public int ? CreateUserId { get; set; }
        public int ModuleId { get; set; }
        public int LocationId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; }        
        public virtual MstrSubCategory SubCategory {get; set;}
        public virtual MstrSpecialCategory Category {get; set;}
    }
}