using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.Payment_Invoice_Header")]    
    public class Trans_PaymentInvoiceDetails
    {
        [Key]
        public  long    PIDIdx { get; set; }
        public  long    PIHIdx { get; set; }
        public  long    GRNDIdx { get; set; }
        public  long    ArticleColrSizeIdx { get; set; }          
        public  long    UOMIdx { get; set; }        
        public  decimal GRNQty { get; set; }   
        public  decimal InvoiceQty { get; set; }
        public  decimal FOCQty { get; set; }
        public  decimal InvoiceUnitAmount { get; set; }   
        public  decimal InvoiceAmount { get; set; } 
        public  decimal ComInvoiceamount { get; set; }                      
    }
}

