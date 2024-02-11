using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("tms.Master.Route")] 
    public class Master_Transport_Route
    {
        [Key]
        public int idRoute { get; set; }       
        public string cShortCode { get; set; }
        public string Details { get; set; }        
        public bool  bActive { get; set; }             
    }
}
