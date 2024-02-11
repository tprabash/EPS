using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Division")]
    public class MstrDivision
    {
        [Key]
        public int AutoIdx { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string DivisionName { get; set; }
    }
}