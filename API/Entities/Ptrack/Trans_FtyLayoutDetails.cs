using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
     [Table("IE.Trans.FtyLayoutDetails")]
    public class Trans_FtyLayoutDetails
    {
        [Key]
        public int FtyLayDId { get; set; }             
        public int FtyLayId { get; set; }       
        public int OpId { get; set; }   
        public int ftyWOpSecId { get; set; }   
        public decimal ftySMV { get; set; }   
        public int ftyHourlyTarget { get; set; }   
        public int ftySequence { get; set; }
        public decimal ManPower { get; set; }   
        public decimal MachineQty { get; set; }   
  

    }
}
