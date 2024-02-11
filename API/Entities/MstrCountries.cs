using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Countries")]
    public class MstrCountries
    {
        [Key]
        public int AutoId {get;set;}
        public string Code {get;set;}
        public string Name {get;set;}
        public string Alpha2Code {get;set;}
        public string Alpha3Code {get;set;}
        public int Numeric {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}