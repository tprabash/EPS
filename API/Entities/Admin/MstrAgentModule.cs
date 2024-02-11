using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Admin
{
    [Table("Master.AgentModule")]
    public class MstrAgentModule
    {
        [Key]
        public int AutoId { get; set; }

        [ForeignKey("User")] 
        public int UserId { get; set; }

        [ForeignKey("sysModule")] 
        public int SysModuleId { get; set; }

        [ForeignKey("UserId")] 
        public virtual MstrAgents User { get; set; }

        [ForeignKey("SysModuleId")] 
        public virtual SystemModule sysModule { get; set; }
    }
}