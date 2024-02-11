using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.GDNDetails")]
    public class TransGDNDetails
    { 
        [Key]
        public long GDNDetailsId { get; set; }
        public long GDNHeaderId { get; set; }
        public long GRNDetailsId { get; set; }
        public int DispatchQty { get; set; }
        public decimal DispatchRate { get; set; }
        public int CurrencyId { get; set; }
        public int DispatchUOM { get; set; }
        public decimal UOMConv { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
