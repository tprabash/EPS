using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.StoreSite")]
    public class MstrStoreSite
    {
        [Key]
        public int AutoId { get; set; }
        [Required]
        public string SiteCode { get; set; }
        [Required]
        public string SiteName { get; set; }
        public bool bActive { get; set; }        
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; } 
     
    }

}