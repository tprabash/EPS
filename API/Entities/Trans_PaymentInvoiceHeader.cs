using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.Payment_Invoice_Header")]
    public class Trans_PaymentInvoiceHeader
    {
        [Key]
        public  long        PIHIdx { get; set; }    
        public  long        InvoiceTypeIdx  { get; set; }   
        public  long        SupplierIdx { get; set; }   
        public  string      InvoiceNo   { get; set; }   
        public  int         InvoiceDocNo    { get; set; }   
        public  DateTime    InvoiceDate { get; set; }   
        public  long        CurrencyId  { get; set; }   
        public  long        PaymentTermsId  { get; set; }   
        public  long        PurchaseTypeId  { get; set; }   
        public  long        TaxTypeId   { get; set; }   
        public  DateTime    GLDate  { get; set; }   
        public  DateTime    DueDate { get; set; }   
        public  decimal     ExchangeRate    { get; set; }   
        public  string      ReferenceNo { get; set; }   
        public  string      Remarks { get; set; }   
        public  decimal     GRNamount   { get; set; }   
        public  decimal     AdditionalChargeamount  { get; set; }   
        public  decimal     Invoiceamount   { get; set; }   
        public  decimal     ComGRNamount{ get; set; }   
        public  decimal     ComAdditionalChargeamount   { get; set; }   
        public  decimal     ComInvoiceamount    { get; set; }   
        public  bool        bActive { get; set; }   
        public  int         AgentId { get; set; }   
        public  DateTime    TransactionDate { get; set; }   
        public  int         ModuleId    { get; set; }   
        public  int         FactionId   { get; set; }   
        public  int         RevisionNo  { get; set; }
        public  bool        bApprove    { get; set; }                
    }
}

