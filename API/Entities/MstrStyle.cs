using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Style")]
    public class MstrStyle
    {
        [Key]
        public int AutoIdx { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string StyleName { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Description { get; set; } 

        [ForeignKey("Link_ProductID")]       
        public virtual MstrProduct Products { get; set; }     

        [ForeignKey("Products")]   
        public int Link_ProductID { get; set; }

        [ForeignKey("Link_BuyerId")] 
        public virtual MstrBuyer Buyers { get; set; }

        [ForeignKey("Buyers")] 
        public int Link_BuyerId { get; set; }
    }
}