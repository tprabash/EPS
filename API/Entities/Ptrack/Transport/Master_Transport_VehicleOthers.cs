using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("tms.Master.VehicleOthers")]
    public class Master_Transport_VehicleOthers
    {
        [Key]
        public int idOther { get; set; }       
        public string OtherDetails { get; set; }
        public bool  bActive { get; set; }        
    }
}

