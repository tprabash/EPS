using System.Collections.Generic;


namespace API.DTOs
{
    public class SectionDetailsDto
    {
        public int FactoryId { get; set; }
        public int DepartmentId { get; set; }


        public int SectionId { get; set; }        
        public string SectionCode { get; set; }
        public string SectionDescription { get; set; }  
    }
}