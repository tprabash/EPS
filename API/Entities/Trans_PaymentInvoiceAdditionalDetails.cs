using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
     [Table("Trans.Payment_Invoice_AdditionalDetails")]  
    public class Trans_PaymentInvoiceAdditionalDetails
    {
        public  long    PIADIdx    { get; set; }
        public  long    PIHIdx    { get; set; }
        public  long    ADIdx     { get; set; }
        public  long    BasisIdx     { get; set; }
        public  decimal AddValue      { get; set; }
        public  decimal RunningTotal    { get; set; }
        public  bool    bActive { get; set; }  

    }

}

