using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ProductionDefinitionDt")]
    public class MstrProductionDefinitionDt
    {
        [Key]
        public int AutoId { get; set; } 
        public byte PDHeaderId { get; set; }  
        public int ProcessId { get; set; }  
        public int ReceiveSiteId { get; set; }        
        public int DispatchSiteId { get; set; }
        public virtual MstrProductionDefinitionHd ProdDefinitionHd {get; set;}
        public virtual MstrStoreSite Receive {get; set;}
        public virtual MstrProcess Process {get; set;}
        public virtual MstrStoreSite Dispatch {get; set;}
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; }
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; }  
    }
}