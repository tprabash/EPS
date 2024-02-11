using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("FA.Master.MachineType")]
    public class Master_MachineType
    {
        [Key]
        public int MachineTypeId { get; set; }       
        public string Code { get; set; }
        public string Description { get; set; }        
        public bool  bActive { get; set; } 
    }
}
