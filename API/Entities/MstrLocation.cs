using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Entities.Admin;

namespace API.Entities
{
    [Table("Master.Location")]
    public class MstrLocation
    {
        [Key]
        public int AutoId { get; set; }
        public int SysModuleId { get; set; }        
        public int CompanyId { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int TimeZone { get; set; }
        public virtual SystemModule SysModule { get; set; }
        public virtual MstrCompany MstrCompany {get; set;}

    }
}