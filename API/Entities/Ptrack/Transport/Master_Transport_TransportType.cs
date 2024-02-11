using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("tms.Master.TransportType")]   
    public class Master_Transport_TransportType
    {
        [Key]
        public int idTType { get; set; }       
        public string Details { get; set; }
        public bool  bActive { get; set; }           
    }
}

