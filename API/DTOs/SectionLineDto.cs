using System.Collections.Generic;


namespace API.DTOs
{
    public class SectionLineDto
    {
        public int FactoryId { get; set; }
        public int DepartmentId { get; set; }
        public int SectionId { get; set; }
        public int SubSectionId { get; set; }


        public int LineId { get; set; }        
        public string Description { get; set; }
    }
}