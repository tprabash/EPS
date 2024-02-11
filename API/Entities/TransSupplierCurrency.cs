using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.SupplierCurrency")]
    public class TransSupplierCurrency
    {
        [Key]
        public int SuppCurId { get; set; }
        public int SupplierId { get; set; }
        public int CurrencyId { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
