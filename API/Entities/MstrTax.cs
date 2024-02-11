using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Tax")]
    public class MstrTax
    {
        [Key]
        public int AutoId {get;set;}
        public string Description {get;set;}
        [Column(TypeName = "decimal(18,4)")]
        public decimal RateOnInvoice { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Rate {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}