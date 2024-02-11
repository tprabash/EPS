using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class InvoiceDto
    {
        public virtual IEnumerable<TransInvoiceHeader> InvoiceHeader { get; set; }
        public virtual IEnumerable<InvoiceDetailsDto> InvoiceDetails { get; set; }
        public virtual IEnumerable<InvoiceChargesDto> InvoiceCharges { get; set; }
    }
}