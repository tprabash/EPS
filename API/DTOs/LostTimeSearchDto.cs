using System;

namespace API.DTOs
{
    public class LostTimeSearchDto
    {
        public int FactoryId { get; set; }        
        public DateTime Date { get; set; }
        public int DepartmentId { get; set; } 
        public int SectionId { get; set; } 
        public int SectionGroupId { get; set; }
        public int LineId { get; set; } 
    }
}