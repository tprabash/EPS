using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Buyer")]
    public class MstrBuyer
    {
        [Key]
        public int AutoIdx { get; set; }

        [Required]
        public int BuyerCompanyCode { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string CompanyName { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string CompanyShortName { get; set; }
       
        public virtual ICollection<MstrStyle> Styles { get; set; }

    }
}