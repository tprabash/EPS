using System.Collections.Generic;
using API.Entities.Ptrack;

namespace API.DTOs.ptrack
{
    public class SaveWorkStudyDto
    {
        public int Action { get; set; }
        public int FactoryId { get; set; }
        public int AgentId { get; set; }
        public virtual Master_OperationGroup  SOperationGroup { get; set; }
        public virtual Master_Section  SSection { get; set; }
        public virtual Master_MachineType  SMachinetype { get; set; }
        public virtual Master_Operation  SOperation { get; set; }
        public virtual Trans_ftywiseSections  SFtywiseSections { get; set; }
        public virtual Trans_ftywiseOperations  SFtywiseOperations { get; set; }   
        public virtual Trans_ftywiseOperationsAndSections  SFtywiseOperationsAndSectionss { get; set; }
        public virtual Trans_BreakdownHeader  SBreakdownHeader  { get; set;}                                                        
        public virtual Trans_BreakdownDetails  SBreakdownDetails  { get; set;} 
        public virtual Trans_FtyLayoutHeader  SFtyLayoutHeader  { get; set; }                                                        
        public virtual Trans_FtyLayoutDetails  SFtyLayoutDetails  { get; set; }
        public virtual Trans_StylewiseSMV  SStylewiseSMV  { get; set; }
        public virtual cpd  Scpd  { get; set; }   
        public virtual SectionwiseProduction  Swp { get; set; } 
         public virtual DepartmentWiseSection sDepartmentWiseSection { get; set; }
        public virtual Master_folderandattachmentcs  Sfolderandattachment { get; set; }
        public virtual Master_Foot  SFoot { get; set; }
        public virtual Master_Brand  SBrand { get; set; }
        public virtual Master_Type  SType  { get; set; }
        public virtual Master_Size SSize  { get; set; }
        public virtual Trans_BreakdownTechnicalDetails SBreakdownTechnicalDetails  { get; set; }
       

    }
}

