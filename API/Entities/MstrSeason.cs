using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{  
    [Table("Master.Season")]
    public class MstrSeason
    {
         [Key]
        public int AutoId { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(50)")]

        public string Season { get; set; }
        public string Code {get;set;}        
        public string Description {get;set;}
        public bool bActive {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ?  UpdateDateTime {get;set;}   
    }
}