using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
     [Table("Master.Foot")]
    public class Master_Foot
    {
                                [Key]
        public long AutoId { get; set; }       
        public string Code { get; set; }
        public string Description { get; set; }        
        public bool  bActive { get; set; }
    }
}