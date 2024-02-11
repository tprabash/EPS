using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.MainCategory",Schema="FA")]
    public class MstrMainCategory
    {
        [Key]
        public int AutoId { get; set; }  
        public string Code { get; set; }
        public string Name { get; set; }
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? Companyid { get; set; }
	    public int ? ModuleId { get; set; }
       
    }
}