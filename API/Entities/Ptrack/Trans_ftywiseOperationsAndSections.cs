using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
      [Table("IE.Trans.ftywiseOperationsAndSections")]
    public class Trans_ftywiseOperationsAndSections
    {
        [Key]
        public int ftyWOpSecId { get; set; }              
        public int ftycodeid { get; set; }       
        public int OpId { get; set; }   
        public int FtyWiseSecId { get; set; }   
        public bool  bActive { get; set; }

        
    }
    
}
