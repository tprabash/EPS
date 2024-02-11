using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.UserSite")]
    public class MstrUserSite
    {
        [Key]
        public int UserSiteId {get;set;}
	    public Type TypeId {get;set;}
	    public int SiteId {get;set;}
	    public int AgentId {get;set;}
	    public bool bActive {get;set;}
	    public int CreateUserId {get;set;}
	    public DateTime? CreateDateTime {get;set;}
	    public int UpdateUserId {get;set;}
	    public DateTime? UpdateDateTime {get;set;}
    }

    public enum Type
    {
        [Display(Name = "Default Site")]
        DefaultSite=1,
        [Display(Name = "From Site")]
        FromSite=2,
        [Display(Name = "To Site")]
        ToSite=3
    }
}