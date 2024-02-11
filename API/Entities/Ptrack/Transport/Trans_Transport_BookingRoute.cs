using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("tms.Trans.BookingRoute")]  
    public class Trans_Transport_BookingRoute
    {
        [Key]
        public int idBRRoute { get; set; } 
        public int idBR { get; set; } 
        public int idRouteFrom { get; set; }
        public int idRouteTo { get; set; }  
        public string StartDateTime { get; set; } 
        public string EndDateTime { get; set; }
        public int idRMat { get; set; }                     
    }
}
