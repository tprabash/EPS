using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.SequenceSettings")]
    public class TransSequenceSettings
    {
        [Key]
        public int AutoId { get; set; }
        public string TransType { get; set; }        
        public string Prefix { get; set; }
        public int SeqLength { get; set; }
        public int SeqNo { get; set; }
        public int CurrentYear { get; set; }        
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; }          
        public int LocationId { get; set; }       
        public virtual MstrUserLocation UserLocation {get; set;}
    }

}