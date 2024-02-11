using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Admin
{
    [Table("Admin.SystemModule")]
    public class SystemModule
    {
        [Key]
        [Column(TypeName = "tinyint")]
        public int AutoId { get; set; }
        public string ModuleName { get; set; }
        public string DBName { get; set; }
    }
}