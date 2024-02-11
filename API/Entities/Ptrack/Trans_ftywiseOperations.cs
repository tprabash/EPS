using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
   [Table("IE.Trans.ftywiseOperations")]
    public class Trans_ftywiseOperations
    {
        [Key]
        public int ftyWOpId { get; set; }              
        public int ftycodeid { get; set; }       
        public int OpId { get; set; }   
        public int HourlyTarget { get; set; }   
        public decimal  SMV { get; set; }
    }
}
