using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans_GRNHeader")]
    public class GRNHeader
    {
        [Key]
        public long AutoId { get; set; }
        public long GRNTypeId { get; set; }
        public string DocNo { get; set; }
        public DateTime DocDate { get; set; }
        public long SupplierIdx { get; set; }
        public string SupplierRef { get; set; }
        public int bActive { get; set; }
        public long bApprove { get; set; }
        public DateTime Transdatetime { get; set; }
        public long publicGRNTypeId { get; set; }
    }
}