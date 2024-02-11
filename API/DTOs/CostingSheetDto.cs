using System.Collections.Generic;

namespace API.DTOs
{
    public class CostingSheetDto
    {
        public virtual IEnumerable<CostingHeaderDto> costHeader { get; set; }    
        public virtual IEnumerable<CostingDetailsDto> costDetails { get; set; }        
        public virtual IEnumerable<CostingSpecialDto> costSpecials { get; set; }
    }
}