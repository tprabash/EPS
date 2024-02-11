using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("tms.Master.VehicleType")]
    public class Master_Transport_VehicleType
    {
        [Key]
        public int idVT { get; set; }       
        public string Details { get; set; }
        public bool  bActive { get; set; }
    }
}

