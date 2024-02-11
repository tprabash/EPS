using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.MenuList")]
    public class MstrMenuList
    {
        [Key]
        public int AutoIdx { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string MenuName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string MenuDescription { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string GroupName { get; set; }

        [Required]
        [Column(TypeName = "char(1)")]
        public string mType { get; set; }

        public int ? CreateUserID { get; set; }

        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        public int ? UpdateUserID { get; set; }

        public DateTime UpdateDateTime { get; set; } = DateTime.Now;
    }
}