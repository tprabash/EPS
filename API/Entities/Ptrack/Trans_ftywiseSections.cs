using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
     [Table("IE.Trans.ftywiseSections")]
    public class Trans_ftywiseSections
    {
        [Key]
        public int FtyWiseSecId { get; set; }              
        public int ftycodeid { get; set; }       
        public int SecId { get; set; }   
        public int SectionIdx { get; set; }   
        public bool  bActive { get; set; }
    }
}
