using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.SubToMainCategoryAllocation",Schema="FA")]
    public class MstrFASubToMainCategoryAll
    {
        [Key]
        public int AutoId {get;set;}
        public int maincatid {get;set;}
        public int subcatid {get;set;}
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? Companyid { get; set; }
	    public int ? ModuleId { get; set; }
    }
}