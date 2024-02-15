using System.Collections.Generic;
using API.Entities;


namespace API.DTOs
{
    public class SaveMWSMasterDto
    {
        public int ActivityNo { get; set; }
        public int ModuleNo { get; set; }
        public int CompanyNo { get; set; }
        public int LocationNo { get; set; }
        public int AgentNo { get; set; }
        public int bActive { get; set; }

        public virtual   MstrCompany  sCompany  { get; set; }  
         public virtual   MstrCategory  sCategory  { get; set; } 
       
    }
}