using API.Entities;
using System.Collections;
using System.Collections.Generic;

namespace API.DTOs
{
    public class GRNSaveDto
    {
        public virtual TransGRNHeader GRNHeader { get; set; }
        public virtual IEnumerable<TransGRNDetails> GRNDetails { get; set; }
    }
}
