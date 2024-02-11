using System;

namespace API.DTOs
{
    public class LostTimeDto
    {
        public int AutoId { get; set; }
        public DateTime Date { get; set; }
        public string Factory { get; set; }
        public int FactoryId { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public string SectionCode { get; set; }
        public int SectionId { get; set; }
        public string SubSection { get; set; }
        public int SubSectionId { get; set; }
        public string LineName { get; set; }
        public int LineId { get; set; }
        public string Reason { get; set; }
        public int ReasonId { get; set; }
        public int Minuts { get; set; }
    }
}