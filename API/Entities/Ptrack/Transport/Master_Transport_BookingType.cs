using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("tms.Master.BookingType")]   
    public class Master_Transport_BookingType
    {
        [Key]
        public int idBType { get; set; }       
        public string Details { get; set; }
        public bool  bActive { get; set; }           
    }
}
