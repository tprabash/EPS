using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.FlexFieldValueList")]
    public class MstrFlexFieldValueList
    {
        [Key]
        public int AutoId {get;set;}
        public int FlexFieldId {get;set;}
        public string FlexFeildVlaue {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}