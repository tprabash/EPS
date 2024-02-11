using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.CostingHeader")]
    public class TransCostingHeader
    {
        [Key]
        public long AutoId { get; set; }
        public string RefNo { get; set; }
        public int CustomerId { get; set; }
        public long ArticleId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int CombinId { get; set; }
        
        public string Combination { get; set; }
        public int VersionControl { get; set; }
        // public int RealSize {get;set;}
        public int NoOfUps { get; set; }
        public int BrandCodeId { get; set; }
        public byte PDHeaderId { get; set; }
        
        [Column(TypeName = "decimal(9,4)")]
        public decimal Tollerence { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal BoardLength { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal BoardWidth { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal SQM { get; set; }
        public int ReelSize { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal ActualReal { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal TrimWaste { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal TotNetWeight { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal TotGrossWeight { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal TotalBoxCost { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal MOQCost { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal Markup { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal SellingPrice { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal Commission { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal TotMOQCost { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal ProfitMarkup { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal CommSelPrice { get; set; }
        public int CartonTypeId { get; set; }
        public DateTime TransDate { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public int DimensionId { get; set; }
        public long ReferenceCostId { get; set; }
        public string CostCoppyType { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public virtual MstrCustomerHeader LinkCustomerHeader { get; set; }
        public virtual MstrArticle LinkArticle { get; set; }
        public virtual MstrColor LinkColor { get; set; }
        public virtual MstrSize LinkSize { get; set; }
        public virtual MstrCombination LinkCombination { get; set; }

    }
}