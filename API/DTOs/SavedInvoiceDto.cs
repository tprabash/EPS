using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class SavedInvoiceDto
    {
        public virtual TransInvoiceHeader InvoiceHeader { get; set; }
        public virtual List<TransInvoiceDetails> InvoiceDetails { get; set; }
        public virtual List<TransInvoiceCharges> InvoiceCharges { get; set; }
    }
}