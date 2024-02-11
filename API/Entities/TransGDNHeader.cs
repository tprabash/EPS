using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.GDNHeader")]
    public class TransGDNHeader
    {
        [Key]
        public long GDNHeaderId { get; set; }
        public string GDNNo { get; set; }
        public int GDNType { get; set; }
        public int SupplierId { get; set; }
        public int FromSiteId { get; set; }
        public int ToSIteId { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
