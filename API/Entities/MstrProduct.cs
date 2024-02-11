using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Product")]
    public class MstrProduct
    {
        [Key]
        public int AutoIdx { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string ProductName { get; set; }
        public virtual ICollection<MstrStyle> Styles { get; set; }
    }
}