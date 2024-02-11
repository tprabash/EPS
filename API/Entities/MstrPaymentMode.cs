using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{   
    [Table("Master.PaymentMode")]
    public class MstrPaymentMode
    {   [Key]
        public int AutoId{ get; set; }
        public string Name { get; set; }
        public int ? CreateUserId { get; set; }
        public DateTime ? CreatedDateTime { get; set; }
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}