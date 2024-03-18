using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans_GRNDetails")]
    public class GRNDetails
    {
        [Key]
        public long GRNMasterIdx { get; set; }
        public long ItemMasterIdx { get; set; }
        public decimal GRNQty { get; set; }
        public decimal FreeQty { get; set; }
        public long UOMId { get; set; }
        public decimal UnitRate { get; set; }
        public decimal Value { get; set; }
        public int bActive { get; set; }
    }
}