using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.SupplierHeader")]
    public class TransSupplierHeader
    {
        [Key]
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public string ShortCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipPostalCode { get; set; }
        public int CountryId { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public int CurrencyId { get; set; }
        public int SupTypeId { get; set; }
        public string VATNo { get; set; }
        public string TaxNo { get; set; }
        public string TinNo { get; set; }
        public int CreditDays { get; set; }
        public int ShipmentTolorence { get; set; }
        public int AccountGroupId { get; set; }
        public bool bActive { get; set; }
        public int LocationId { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
