using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.IndentHeader")]
    public class TransIndentHeader
    {
        [Key]
        public long IndentHeaderId { get; set; }
        public string IndentNo { get; set; }
        public long MRHeaderId { get; set; }
        public int AssignTo { get; set; }
        public DateTime TransDate { get; set; }
        public IndentType Type { get; set; }
        public IndentStatus Status { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }

    public enum IndentStatus
    {
        Active = 1,
        Inactive = 2,
        Complete = 3,
        Cancelled = 4,
        WattingForApproval = 5
    }

    public enum IndentType
    {
        MRIndent = 1,
        AdhocIndentSalesOrder = 2,
        AdhocIndentNone = 3,
        OCIndent = 4
    }
}
