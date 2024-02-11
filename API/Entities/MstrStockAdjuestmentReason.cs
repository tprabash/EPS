using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.StockAdjuestmentReason")]
    public class MstrStockAdjuestmentReason
    {
        [Key]
        public int ReasonId { get; set; }
        public string Reason { get; set; }
        public string Sign { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
