using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Units")]
    public class MstrUnits
    {
        [Key]
        public int AutoId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
        
        public int ? CreateUserId { get; set; }

        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;

        public int ? UpdateUserId { get; set; }

        public DateTime ? UpdateDateTime { get; set; } 
    }

}