using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("IE.Trans.FtyLayoutHeader")]
    public class Trans_FtyLayoutHeader
    {
        [Key]
        public int FtyLayId { get; set; } 
        public string Code { get; set; }             
        public int styleID { get; set; }           
        public int ftycodeid { get; set; }  
        public int BrDoH_id { get; set; }          
        public int idAgent { get; set; }   
        public bool bActive { get; set; }
    }
}
