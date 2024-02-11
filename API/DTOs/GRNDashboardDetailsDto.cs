using API.Entities;
using System.Collections;
using System.Collections.Generic;

namespace API.DTOs
{
    public class GRNDashboardDetailsDto
    {
        public IEnumerable<TransGRNHeader> GRNHeader { get; set; }
        public IEnumerable<GRNDetailsDto> GRNDetails { get; set; }
    }
}
