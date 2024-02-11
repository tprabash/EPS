using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("ptSectionProduction")]
    public class cpd
    {
        [Key]
        public int AutoIdx { get; set; }   
        public int FactoryIdx { get; set; }              
        public int LineCodeIdx { get; set; }       
        public int SectionIdx { get; set; }   
        public int StyleIdx { get; set; }   
        public int ProfileDtIdx { get; set; }   
        public int BundleIdx { get; set; }      
        public int InQty { get; set; }   
        public int OutQty { get; set; }             

    }
}
