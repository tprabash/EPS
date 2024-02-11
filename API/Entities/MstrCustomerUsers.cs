using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.CustomerUsers")]
    public class MstrCustomerUsers
    {
        [Key]
        public int AutoId {get;set;}
	    public string Title {get;set;}
	    public string FirstName {get;set;}
	    public string LastName {get;set;}
	    public string Designation {get;set;}
		public string Email {get;set;}
	    public bool IsActive {get;set;}
        public int CustomerId { get; set; }
	    public int ? CreateUserId {get;set;}
	    public DateTime ? CreateDateTime {get;set;}
	    public int ? UpdateUserId {get;set;}
	    public DateTime ? UpdateDateTime {get;set;}
    }
}