using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ProductSubCategory")]
    public class MstrProductSubCat
    {
        [Key]
        public int AutoId { get; set; }
        public string ProdSubCatName { get; set; }
        public string ProdSubCatCode { get; set; }
        public int ProdGroupId { get; set; }
        public bool IsActive { get; set; }        
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; }        
        public virtual MstrProductGroup PGroup {get; set;}
    }

}