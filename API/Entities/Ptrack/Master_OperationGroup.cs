using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{  [Table("Master.OperationGroup")]
    public class Master_OperationGroup
    {
        [Key]
        public long OpGrId { get; set; }       
        public string Code { get; set; }
        public string Description { get; set; }        
        public bool  bActive { get; set; }
    }
}
