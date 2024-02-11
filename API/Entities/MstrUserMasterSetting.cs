using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
	[Table("Master.UserMasterSetting")]
    public class MstrUserMasterSetting
    {
		[Key]
        public int UserMasterSetId {get;set;}
	    public int AgentId {get;set;}
	    public bool bIntent {get;set;}
		public bool CostAtt {get;set;}
	    public int CreateUserId {get;set;}
	    public DateTime CreateDateTime {get;set;}
	    public int UpdateUserId {get;set;}
	    public DateTime UpdateDateTime {get;set;}
    }
}