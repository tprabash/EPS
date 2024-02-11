using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("tms.Master.VehicleRegister")]  
    public class Master_Transport_VehicleRegister
    {
        [Key]
        public int idVR { get; set; }  
        public string VehicleNo { get; set; }                  
        public DateTime ? RevenueLicense_ExpDate { get; set; }       
        public DateTime ? Insurance_ExpDate { get; set; }
        public int TransporterId { get; set; }
        public int idVT { get; set; }
        public int idVCat { get; set; }
        public int AttachedLocation { get; set; }                                  
        public DateTime ?  Create_at { get; set; }       
        public DateTime ?  Update_at { get; set; }
        public bool  bActive { get; set; }              
    }
}

