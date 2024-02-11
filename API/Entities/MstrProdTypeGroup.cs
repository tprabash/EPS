using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ProdTypeGroup")]
    public class MstrProdTypeGroup
    {
        [Key]
        public int AutoId { get; set; }         
        public int ProdGroupId { get; set; }
        public int ProdTypeId { get; set; }
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; }        
        public virtual MstrProductType ProductType {get; set;}
        public virtual MstrProductGroup ProductGroup {get; set;}
    }
}