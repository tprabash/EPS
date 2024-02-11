using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("tms.Master.RouteFactoryAllocation")]   
    public class Master_Transport_RouteFactoryAllocation
    {
        [Key]
        public int idRAllocation { get; set; }       
        public int AutoId { get; set; }
        public int idRoute { get; set; }        
      
    }
}
