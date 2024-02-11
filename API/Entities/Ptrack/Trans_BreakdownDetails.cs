using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("IE.Trans.BreakdownDetails")]
    public class Trans_BreakdownDetails
    {
        [Key]
        public int BrDoHD_id { get; set; }             
        public int BrDoH_id { get; set; }       
        public int OpId { get; set; }   
        public int Sequence { get; set; }    
        public decimal Occurrence { get; set; }    
        public decimal TotSMV { get; set; }  
    
    }
}
