using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ProductGroup")]
    public class MstrProductGroup
    {
        [Key]
        public int AutoId { get; set; }
        public string ProdGroupName { get; set; }
        public string ProdGroupCode { get; set; }
        public int SerialNo { get; set; }
        // public int ProdTypeId { get; set; }
        public bool ? IsActive { get; set; }        
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; }        
        // public virtual MstrProductType ProductType {get; set;}
    }

}