using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Entities
{
    [Table("Trans.IssueToProductionDetails")]
    public class IssueToProdcutionDetails
    {
        [Key]
        public long ITPDId { get; set; }
        public long ITPHId { get; set; }
        public long SOHIdx { get; set; }
        public long SODIdx { get; set; }
        public long ReceiveSiteId { get; set; }
        public long ArticleColorSizeId { get; set; }
        public int OrderQty { get; set; }
        public int IssuedQty { get; set; }
        public int IssueQty { get; set; }
    }
}