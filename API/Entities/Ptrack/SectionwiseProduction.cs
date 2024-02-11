using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("ptSectionwiseProduction")]
    public class SectionwiseProduction
    {
         [Key]
        public int AutoIdx { get; set; }   
        public DateTime ProductionDate { get; set; }              
        public int SectionHierarchyId { get; set; }       
        public int ftywiseTimeBeltId { get; set; }   
        public int BundleIdx { get; set; }   
        public int InQty { get; set; }   
        public int OutQty { get; set; }      
        public int iStatus { get; set; }   
    }
}