using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.BrandCode")]
    public class MstrBrandCode
    {
        [Key]
        public int AutoId { get; set; }
        public string Name { get; set; }        
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; }         
        public int BrandId { get; set; }
        public bool IsActive {get;set;}      
        public virtual MstrBrand Brand {get; set;}
    }

}