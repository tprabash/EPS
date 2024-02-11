using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.SupplierAddresses")]
    public class TransSupplierAddresses
    {
        [Key]
        public int SuppAddId { get; set; }
        public int SupplierId { get; set; }
        public int AddressTypeId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipPostalCode { get; set; }
        public int CountryId { get; set; }
        public string Tel { get; set; }
        public string VATNo { get; set; }
        public string TaxNo { get; set; }
        public string TinNo { get; set; }
        public bool bActive { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
