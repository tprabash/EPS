using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.RejectionReasons")]
    public class MstrRejectionReasons
    {
        [Key]
        public int AutoId {get;set;}
	    public string Details {get;set;}
        public int LocationId {get;set;}
	    public bool IsActive {get;set;}
	    public int ? CreateUserId {get;set;}
	    public DateTime ? CreateDateTime {get;set;}
	    public int ? UpdateUserId {get;set;}
	    public DateTime ? UpdateDateTime {get;set;}   
    }
}