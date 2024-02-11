using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.SpecialSubCategory")]
    public class MstrSubCategory
    {
        [Key]
        public int AutoId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public int ModuleId { get; set; }
        public int LocationId { get; set; }
        public bool IsActive { get; set; }
    }
}
