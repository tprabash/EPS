using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Size")]
    public class MstrSize
    {
        [Key]
        public int AutoId { get; set; }
        // [Required]
        public string Code { get; set; }

        // [Required]
        public string Name { get; set; }

        // [Required]
        // [ForeignKey("SizeCard")] 
        // public int LinkSizeCard { get; set; }

        // [ForeignKey("LinkSizeCard")] 
        // public virtual MstrSizeCard SizeCard {get; set;}

        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; } 
    }
}