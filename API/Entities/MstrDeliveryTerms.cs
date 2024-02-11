using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.DeliveryTerms")]
    public class MstrDeliveryTerms
    {
        [Key]
        public int DeliTermsId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
