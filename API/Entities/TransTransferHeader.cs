using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.TransferHeader")]
    public class TransTransferHeader
    {
        [Key]
        public long idTransH { get; set; }
        public long SoHeaderId { get; set; }
        public DateTime TransDate { get; set; }
        public string TransNo { get; set; }
        public int idSite { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModuleId { get; set; }
        public int LocationId { get; set; }
        public string OrderRef { get; set; }

    }
}