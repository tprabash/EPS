using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.PurchaseOrderDetails")]
    public class TransPurchaseOrderDetails
    {
        [Key]
        public long PODetailsId { get; set; }
        public long POHeaderId { get; set; }
        public long IndentId { get; set; }
        public long ArticleId { get; set; }
        public long ColorId { get; set; }
        public long SizeId { get; set; }
        public int OpenQty { get; set; }
        public int OrderQty { get; set; }
        public decimal UnitPrice { get; set; }
        public int UOMId { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
