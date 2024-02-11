using API.Entities;

namespace API.DTOs
{
    public class ApproveMRHeaderDto
    {
        public long MRHeaderId { get; set; }
        public string RefNo { get; set; }
        public int AssignedTo { get; set; }
        public int ApproveId { get; set; }
        public string Remarks { get; set; }
        public int UserId { get; set; }
        public MRStatus StatusId { get; set; }
        public int AssignUser { get; set; }
        public bool IsFinal { get; set; }
        public int LocationId { get; set; }
        
    }
}
