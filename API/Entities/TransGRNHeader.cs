using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.GRNHeader")]
    public class TransGRNHeader
    {
        [Key]
        public long GRNHeaderId { get; set; }
        public long GINHeaderId { get; set; }
        public string GRNNo { get; set; }
        public int GRNTypeId { get; set; }
        public int SupplierId { get; set; }
        public int ToSiteId { get; set; }
        public int FromSiteId { get; set; }
        public int FromLocation { get; set; }
        public string DocNo { get; set; }
        public bool IsActive { get; set; }
        public DateTime TransDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }

    }
}
