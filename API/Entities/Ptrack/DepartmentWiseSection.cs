using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace API.Entities.Ptrack
{
    [Table("dbo.DepartmentWiseSections")]
    public class DepartmentWiseSection
    {
        [Key]
        public int DeptSecId { get; set; }
        public int DeptId { get; set; }
        public int SecId { get; set; }
        public bool bActive { get; set; }=true;

    }
}