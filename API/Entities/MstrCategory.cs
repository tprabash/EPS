using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Category")]
    public class MstrCategory
    {
        [Key]
        public int AutoId { get; set; }       
         public int ModuleId { get; set; } 
        public string Code { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; } 
         public bool bActive { get; set; }        
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; } 
    }

}