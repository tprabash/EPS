using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.FlexFieldDetails")]
    public class MstrFlexFieldDetails
    {
        [Key]
        public int AutoId {get;set;}
        public int CategoryId {get;set;}
        public int ProdTypeId {get;set;}
        public byte ModuleId {get;set;}
        public string FlexFieldName {get;set;}
        public string FlexFieldCode {get;set;}
        public string DataType {get;set;}
        public bool ValueList {get;set;}
        public bool isActive {get;set;}
        public bool Mandatory {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
        public virtual MstrCategory MstrCategory { get; set; }
        public virtual MstrProductType MstrProductType { get; set; }
    }
}