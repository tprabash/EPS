using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.CostingSpecial")]
    public class TransCostingSpecial
    {
        [Key]
        public long AutoId {get;set;}
        public long CostHeaderId {get;set;}
        public int SpeInstId {get;set;}
        public string Value {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}        
        public TransCostingHeader CostingHeader {get;set;}

    }
}