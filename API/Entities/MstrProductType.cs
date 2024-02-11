using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ProductType")]
    public class MstrProductType
    {
        [Key]
        public int AutoId { get; set; }  
        public string ProdTypeName { get; set; }
        public string ProdTypeCode { get; set; }
        // public int CategoryId { get; set; }
        public bool ? bAutoArticle { get; set; }
        public bool ? IsActive { get; set; }        
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; }        
        // public virtual MstrCategory Category {get; set;}
    }

}