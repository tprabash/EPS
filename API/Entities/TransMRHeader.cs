using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.MRHeader")]
    public class TransMRHeader
    {
        [Key]
        public long MRHeaderId {get;set;}
	    public DateTime TransDate {get;set;}
	    public string MRNo {get;set;}
	    public int AssignedTo {get;set;}
	    public int SiteId {get;set;}
        public int CategoryId { get; set; }
        public string Remark {get;set;}
	    public bool bActive {get;set;}
	    public MRStatus StatusId {get;set;}
	    public int LocationId {get;set;}
	    public int CreateUserId {get;set;}
	    public DateTime CreateDateTime {get;set;}
	    public int UpdateUserId {get;set;}
	    public DateTime UpdateDateTime {get;set;}
    }

    public enum MRStatus 
    {
        Created=1,
        Waiting=2,
        Approve=3,
        Reject=4,
        Closed=5
    }
}