using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ProdGroupSubCategory")]
    public class MstrProdGroupSubCat
    {
        [Key]
        public int AutoId { get; set; }         
        public int ProdGroupId { get; set; }
        public int SubCatId { get; set; }
        public int ? CreateUserId { get; set; }
        public int ModuleId { get; set; }
        public int LocationId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; }        
        public virtual MstrProductGroup Group {get; set;}
        public virtual MstrSubCategory SubCategory {get; set;}
    }
}