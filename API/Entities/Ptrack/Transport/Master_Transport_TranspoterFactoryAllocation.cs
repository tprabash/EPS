using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("tms.Master.TranspoterFactoryAllocation")]  
    public class Master_Transport_TranspoterFactoryAllocation
    {
         [Key]
        public int idAllocation { get; set; }       
        public int AutoId { get; set; }      
        public int idTrans { get; set; }         
    
    }
}

