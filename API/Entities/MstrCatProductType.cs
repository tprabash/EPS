using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.CatProductType")]
    public class MstrCatProductType
    {
        [Key]
        public int AutoId { get; set; }         
        public int CategoryId { get; set; }
        public int ProdTypeId { get; set; }
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; }        
        public virtual MstrCategory Category {get; set;}
        public virtual MstrProductType ProductType {get; set;}
    }
}