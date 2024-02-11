using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Model")]
    public class MstrModel
    {
        [Key]
        public long AutoId { get; set; }
        public string ModelName { get; set; }        
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? Companyid { get; set; }
	    public int ? ModuleId { get; set; }
        public bool IsActive { get; set; }
    }
}