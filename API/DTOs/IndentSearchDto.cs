using API.Entities;

namespace API.DTOs
{
    public class IndentSearchDto
    {
        public int? CategoryId { get; set; }
        public int? AssignedTo { get; set; }
        public IndentType Type { get; set; }
    }
}
