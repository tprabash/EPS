using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Admin
{
    [Table("Master.Agents")]
    public class MstrAgents
    {
        [Key]
        public int idAgents { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string cAgentName { get; set; }

        [Required]
        [Column(TypeName = "varchar(24)")]
        public string cPassword { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        public string cDescription { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string cEmail { get; set; }       

        [Required]      
        public bool bActive { get; set; }

        [Required] 
        public byte[] PasswordHash { get; set; }

        [Required] 
        public byte[] PasswordSalt { get; set; }
        public int ? CreateUserId { get; set; }
        public int ? UpdateUserId { get; set; }
        public DateTime ? CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime ? UpdateDateTime { get; set; } = DateTime.Now;        

        [ForeignKey("Category_Link")] 
        public int iCategoryLevel { get; set; }  

        [ForeignKey("iCategoryLevel")] 
        public virtual MstrAgentLevel Category_Link { get; set; }
        public virtual ICollection<MstrMenuList> Menus { get; set; }

    }
}