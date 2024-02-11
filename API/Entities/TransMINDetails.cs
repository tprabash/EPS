using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.MINDetails")]
    public class TransMINDetails
    {
        [Key]
        public long AutoId {get;set;}
        public long MINHeaderId {get;set;}
        public long CostDetailId {get;set;}
        public int RequiredQty {get;set;}
        public int IssuedQty {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}