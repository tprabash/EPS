using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{   
    [Table("Master.CartonBoxType")]
    public class MstrCartonType
    {   
        [Key]
        public int AutoId { get; set; }
        public string Name { get; set; }
        public string Description { get; set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}