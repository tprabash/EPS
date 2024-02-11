using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Mapping")]
    public class MstrMapping
    {
        [Key]
        public long autoId { get; set; }
        public int MappingTypeId { get; set; }
        public long CustomerId {get;set;}
        public long RefId {get;set;}
        public string MappedTo {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? modudeId {get;set;}
        public int ? locationId {get;set;}
    }
}
