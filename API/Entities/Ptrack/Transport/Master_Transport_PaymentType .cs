using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("tms.Master.PaymentType")]  
    public class Master_Transport_PaymentType
    {
        [Key]
        public int idPayType { get; set; }       
        public string Details { get; set; }
        public bool  bActive { get; set; }     
    }
}

