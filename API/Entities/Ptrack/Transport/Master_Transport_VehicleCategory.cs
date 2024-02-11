using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("tms.Master.VehicleCategory")]   
    public class Master_Transport_VehicleCategory
    {
        [Key]
        public int idVCat { get; set; }       
        public string Details { get; set; }
        public bool  bActive { get; set; }        
    }
}

