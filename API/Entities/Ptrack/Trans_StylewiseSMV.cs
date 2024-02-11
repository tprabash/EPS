using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("pt_SMV_Master")]
    public class Trans_StylewiseSMV
    {
        [Key]
        public int autoid { get; set; }  
        public string factory { get; set; } 
        public string style { get; set; } 
        public string season { get; set; } 
        public int link_Cat { get; set; }  
        public decimal sMV_Value { get; set; }        
        public int styleId { get; set; }
                   
    }
}
