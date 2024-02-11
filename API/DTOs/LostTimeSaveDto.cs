using System;

namespace API.DTOs
{
    public class LostTimeSaveDto
    {   
        public long AutoId { get; set; }
        public int UserId { get; set; }
        public int FactoryId { get; set; }        
        public DateTime Date { get; set; }
        public int DepartmentId { get; set; } 
        public int SectionId { get; set; } 
        public int SectionGroupId { get; set; }
        public int LineId { get; set; }
        public int ReasonId { get; set; }
        public int Minuts { get; set; }
    }
}