using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using API.Entities.Ptrack;

namespace API.DTOs
{
    public class PurchaseOrdersDto
    {        
        public virtual IEnumerable<PurchaseOrderDto> PurchaseOrder { get; set; }    
        public virtual IEnumerable<LineItemDto> LineItem { get; set; }    
    }
}