using API.Entities;
using System;

namespace API.DTOs
{
    public class MRHeaderDto
    {
        public long MRHeaderId { get; set; }
        public string TransDate { get; set; }
        public string MRNo { get; set; }
        public int AssignedTo { get; set; }
        public int SiteId { get; set; }
        public int CategoryId { get; set; }
        public string Remark { get; set; }
        public bool bActive { get; set; }
        public MRStatus StatusId { get; set; }
        public int LocationId { get; set; }
        public string Location { get; set; }
        public string SiteName { get; set; }
        public string AgentName { get; set; }
        public string Category { get; set; }
    }
}
