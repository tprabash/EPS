using System.Collections.Generic;


namespace API.DTOs
{
    public class SubSectionDto
    {
        public int FactoryId { get; set; }
        public int DepartmentId { get; set; }
        public int SectionId { get; set; }


        public int SubsectionId { get; set; }        
        public string Description { get; set; }
    }
}