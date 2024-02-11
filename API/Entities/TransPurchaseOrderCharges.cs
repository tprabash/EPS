using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.PurchaseOrderCharges")]
    public class TransPurchaseOrderCharges
    {
        [Key]
        public long POChargesId { get; set; }
        public long POHeaderId { get; set; }
        public int AddChargeId { get; set; }
        public int BasisId { get; set; }
        public decimal Value { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
