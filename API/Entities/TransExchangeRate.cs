using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.ExchangeRate")]
    public class TransExchangeRate
    {
        [Key]
        public int AutoId {get;set;}
        public int CurrencyFId {get;set;}
        public int CurrencyTId {get;set;}
        [Column(TypeName = "decimal(9,4)")]
        public decimal Rate {get;set;}
        public DateTime ValidFrom {get;set;}
        public DateTime ValidTo {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}

    }
}