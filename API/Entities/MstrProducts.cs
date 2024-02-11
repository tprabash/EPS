
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Product")]
    public class MstrProducts
    {
        [Key]
        public int productId { get; set; }  
        public string Description { get; set; }
        public bool ? bActive { get; set; }        
        public DateTime ? CreatedDate { get; set; } = DateTime.Now;
        public int ? CompanyId { get; set; }
        public int ? ModuleId { get; set; }
        public int ? createUserId { get; set; }
      
    }

}