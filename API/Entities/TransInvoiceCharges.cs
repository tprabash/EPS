using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.InvoiceCharges")]
    public class TransInvoiceCharges
    {
        [Key]
        public long InvoiceHdId {get;set;}
        public int AddChargeId { get; set; }
        public int BasisId { get; set; }
        public decimal Value { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public int UpdateDateTime { get; set; }
        public string Description { get; set; }
        public int TaxId { get; set; }
    }
}