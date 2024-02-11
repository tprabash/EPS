using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.UserLocation")]
    public class MstrUserLocation
    {
        [Key]
        public int AutoId { get; set; }
        public int UserId { get; set; }        
        public int LocationId { get; set; }
        public bool IsDefault { get; set; }
        public virtual MstrLocation Location { get; set; }
    }
}