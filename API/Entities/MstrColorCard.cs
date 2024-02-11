using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ColorCard")]
    public class MstrColorCard
    {
        [Key]
        //[Column(TypeName = "tinyint")]
        public int AutoId { get; set; }        
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; } 
    }
}