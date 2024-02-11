using System.Collections.Generic;

namespace API.DTOs
{
    public class ApproveMRDto
    {
        public ApproveMRHeaderDto ApproveMRHeader { get; set; }
        public List<ApproveMRDetailsDto> ApproveMRDetails { get; set; }
    }
}
