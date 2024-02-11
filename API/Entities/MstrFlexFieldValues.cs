using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.FlexFieldValues")]
    public class MstrFlexFieldValues
    {
        [Key]
        public int AutoId {get;set;}
        public int FlexFieldId {get;set;}
        public long ArticleId {get;set;}
        public bool bFlexFieldValue {get;set;}
        public DateTime dFlexFieldValue {get;set;}
        public int iFlexFeildValue {get;set;}
        public double fFlexFeildValue {get;set;}
        public string cFlexFeildValue {get;set;}
        public bool IsActive {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}