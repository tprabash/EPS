using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Process")]
    public class MstrProcess
    {
        [Key]
        public int AutoId { get; set; }        
        public string Process { get; set; }        
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;        
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; } 
        public int LocationId { get; set; }
        public virtual MstrLocation Location {get; set;}
    }

}