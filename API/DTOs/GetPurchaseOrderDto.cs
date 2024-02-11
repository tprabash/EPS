using API.Entities;
using System.Collections.Generic;

namespace API.DTOs
{
    public class GetPurchaseOrderDto
    {
        public virtual IEnumerable<TransPurchaseOrderHeader> POHeader { get; set; }
        public virtual IEnumerable<PODetailsDto> PODetails { get; set; }
        public virtual IEnumerable<POAdditionalChargeDto> POAdditionalCharges { get; set; }
    }
}
