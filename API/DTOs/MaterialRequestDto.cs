using API.Entities;
using System.Collections.Generic;

namespace API.DTOs
{
    public class MaterialRequestDto
    {
        public TransMRHeader MRHeader { get; set; }
        public List<TransMRDetails> MRDetails { get; set; }
    }
}
