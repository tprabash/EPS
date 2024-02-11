using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.CustomerCurrency")]
    public class MstrCustomerCurrency
    {
        [Key]
        public int AutoId {get;set;}
        public int CustomerId {get;set;}
        public int CurrencyId {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}