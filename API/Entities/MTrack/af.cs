using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.MTrack
{
    [Table("Agent_Factories")]
    public class af
    {
        [Key]
        public int iAgent { get; set; }   
        public string cFactory { get; set; }             

    }
}
