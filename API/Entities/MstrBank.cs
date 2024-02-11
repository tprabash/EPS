using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Bank")]
    public class MstrBank
    {
        [Key]
        public int AutoId {get;set;}
        public string Name {get;set;}
        public string Branch {get;set;}
        public string AccountNo {get;set;}
        public int CurrencyId {get;set;}
        public long NextChequeNo {get;set;}
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; }
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; }      
    }
}