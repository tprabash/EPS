using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class SavedCostingDto
    {
        public virtual TransCostingHeader CostingHeader { get; set; }
        public virtual List<TransCostingDetails> CostingDetails { get; set; }
        public virtual List<TransCostingSpecial> CostingSpecial { get; set; }
    }
}