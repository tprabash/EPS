using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.GMTValueConfirmation")]
    public class TransGMTValueConfirmation
    {
        [Key]
        public long AutoId {get;set;}  
        public long GMTVLupdateId {get;set;} 
        public bool OrderChargeble {get;set;} 
        public bool AgremntRequired {get;set;}  
        public bool GRNBlock {get;set;} 
        public decimal FallOutPercentage {get;set;}      
        public string Comment {get;set;}  
        public string RewashFaultDesc {get;set;}  
        public int TotalValue {get;set;}  
        public bool WashTypeConfirm {get;set;}       
    }
}
