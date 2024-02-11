using API.Entities;
using System;

namespace API.DTOs
{
    public class AdhocIndentHeaderDto
    {
        public long IndentHeaderId { get; set; }
        public string IndentNo { get; set; }
        public string MRNo { get; set; }
        public string AssignTo { get; set; }
        public IndentStatus Status { get; set; }
        public string CreatedUser { get; set; }
        public string CreatedDate { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
    }
}
