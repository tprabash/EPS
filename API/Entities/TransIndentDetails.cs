using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.IndentDetails")]
    public class TransIndentDetails
    {
        [Key]
        public long IndentDetailId { get; set; }
        public long IndentHeaderId { get; set; }
        public string IndentLineNo { get; set; }
        public long MRDetailsId { get; set; }
        public long ArticleId { get; set; }
        public long ColorId { get; set; }
        public long SizeId { get; set; }
        public int OpenQty { get; set; }
        public int OrderQty { get; set; }
        public int UOMId { get; set; }
        [Column(TypeName = "decimal(9,4)")]
        public decimal UnitPrice { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }

    
}
