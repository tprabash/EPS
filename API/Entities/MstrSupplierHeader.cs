using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.SupplierHeader")]
    public class MstrSupplierHeader
    {
        [Key]
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string ShortCode { get; set; }
        public string CustomerID { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        public int CurrencyId { get; set; }
        public string VATNo { get; set; }
        public string TaxNo { get; set; }
        public string TinNo { get; set; }
        public string ZipPostalCode { get; set; }
        public string Attention { get; set; }
        public int CreditDays { get; set; }
        public bool bActive { get; set; }
        public int LocationId { get; set; }
        public int InvTypeId { get; set; }
        public int TaxId { get; set; }
        public int CusTypeId { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public virtual MstrLocation Location { get; set; }
        public virtual MstrCurrency MstrCurrency { get; set; }
        public virtual MstrCountries MstrCountries { get; set; }
    }
}