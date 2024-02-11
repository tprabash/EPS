using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Entities.Admin;

namespace API.Entities
{
    [Table("Master.MenuUser")]
    public class MstrMenuUser
    {
        [Key]
        public int AutoId { get; set; }

        [ForeignKey("User")] 
        public int UserId { get; set; }

        [ForeignKey("Menu")] 
        public int MenuId { get; set; }

        [ForeignKey("UserId")] 
        public virtual MstrAgents User { get; set; }

        [ForeignKey("MenuId")] 
        public virtual MstrMenuList Menu { get; set; }
    }
}