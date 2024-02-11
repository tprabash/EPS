using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
     [Table("Master.Operations")]
    public class Master_Operation
    {
        [Key]
        public int OpIdx { get; set; }       
        public string Code { get; set; }
        public string Description { get; set; }  
        public decimal SMV { get; set; }         
        public int OpGIdx { get; set; }       
        public int MachineTypeIdx { get; set; }   
        public int HourlyTarget { get; set; }   
        public int HrsTargetedPercent { get; set; }   
        public bool  bActive { get; set; }
    }
}
