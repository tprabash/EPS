using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
     [Table("IE.Trans.BreakdownHeader")]
    public class Trans_BreakdownHeader
    {
        [Key]
        public int BrDoH_id { get; set; } 
        public string Code { get; set; }             
        public int styleID { get; set; }       
        public int bVersion { get; set; }   
        public int idAgent { get; set; }   
        public bool bActive { get; set; }
        public decimal iEfficiency { get; set; }   
        public bool bRevised { get; set; }        
    }
}

