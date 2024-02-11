using API.Entities;
using System.Collections.Generic;

namespace API.DTOs
{
    public class IndentAssignToDto
    {
        public int AssignTo { get; set; }
        public int UserId { get; set; }
        public virtual List<IndentHeader> IndentHeader { get; set; }
    }

    public class IndentHeader
    {
        public long IndentHeaderId { get; set; }
    }
}
