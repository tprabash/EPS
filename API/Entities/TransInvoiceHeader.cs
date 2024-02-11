using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.InvoiceHeader")]
    public class TransInvoiceHeader
    {
        [Key]
        public long AutoId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime TransDate { get; set; }
        public int LocationId { get; set; }
        public int CustomerId { get; set; }
        public string VatNo { get; set; }
        public string TaxNo { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NBTRate { get; set; }
        public decimal NBTAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal NetValue { get; set; }
        public decimal TaxValue { get; set; }
        public decimal BankCharge { get; set; }
        public int CustomerAddId { get; set; }
        public int InvCurrencyId { get; set; }
        public int BaseCurrencyId { get; set; }
        public string InvCurrency { get; set; }
        public string BaseCurrency { get; set; }
        public decimal ExchangeRate { get; set; }
        public string Attention { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public decimal ReceivedValue { get; set; }
        public bool? bActive { get; set; }
        public bool? IsApproved { get; set; }
        public int? ApprovedAgent { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}