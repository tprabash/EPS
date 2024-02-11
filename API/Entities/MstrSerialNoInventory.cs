using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    [Table("Master.SerialNoInventory")]
    public class MstrSerialNoInventory
    {
        [Key]
        public int SerialInvId {get;set;}
		public string TransType {get;set;}
		public int SiteId {get;set;}
		public string InFix {get;set;}
		public string PreFix {get;set;}
		public int SerialNo {get;set;}
		public int CreateUserId {get;set;}
		public DateTime CreateDateTime {get;set;}
		public int UpdateUserId {get;set;}
		public DateTime UpdateDateTime {get;set;}
    }
}