using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.CodeSetting")]
    public class MstrCodeSetting
    {
        [Key]
        public int AutoId {get;set;}
        public int ProdTypeId {get;set;}
        public int ProdGroupId {get;set;}
        public bool isLength {get;set;}
        public bool isWidth {get;set;}
        public bool isHeight {get;set;}
        public string Symbol1 {get;set;}
        public string Symbol2 {get;set;}
        public string symbol3 {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}