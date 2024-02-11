using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class SavePaymentInvoiceDto
    {
        public int Action { get; set; }
        public int FactoryId { get; set; }
        public int AgentId { get; set; } 
        public int ModuleId { get; set; }    
        public string  nowDate { get; set; }       
        public virtual Trans_PaymentInvoiceHeader  sPaymentInvoiceHeader  { get; set; }  
        public virtual Trans_PaymentInvoiceDetails  sPaymentInvoiceDetails  { get; set; }    
        public virtual Trans_PaymentInvoiceAdditionalDetails  sPaymentInvoiceAdditionalDetails { get; set; }             

    }
}

