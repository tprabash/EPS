using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.GRNDetails")]
    public class TransGRNDetails
    {
        [Key]
        public long GRNDetailsId { get; set; }
        public long GRNHeaderId { get; set; }
        public long POHeaderId { get; set; }
        public long PODetailsId { get; set; }
        public long ArticleId { get; set; }
        public long ColorId { get; set; }
        public long SizeId { get; set; }
        public int UOMId { get; set; }
        public int POQty { get; set; }
        public decimal PORate { get; set; }
        public int CurrencyId { get; set; }
        public int ReturnedQty { get; set; }
        public int ReceivedQty { get; set; }
        public int PayableQty { get; set; }
        public int FOCQty { get; set; }
        public int RejectedQty { get; set; }
        public int IssuedQty { get; set; }
        public int StorageUnitId { get; set; }
        public int UnitConvId { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
