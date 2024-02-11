using API.Entities;
using System.Collections.Generic;

namespace API.DTOs
{
    public class SavePurchaseOrderDto
    {
        public virtual TransPurchaseOrderHeader PurchaseOrderHeader { get; set; }
        public virtual List<TransPurchaseOrderDetails> PurchaseOrderDetails { get; set; }
        public virtual List<TransPurchaseOrderCharges> PurchaseOrderCharges { get; set; }
    }
}
