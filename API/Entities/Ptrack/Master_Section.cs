using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("IE.Master.Section")]
    public class Master_Section
    {
        [Key]
        public int SecId { get; set; }       
        public string Code { get; set; }
        public string Description { get; set; }        
        public bool  bActive { get; set; }
    }
}
