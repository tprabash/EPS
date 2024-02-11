using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.CustomerAddressList")]
    public class MstrCustomerAddressList
    {
        [Key]
        public int AutoId {get;set;}
        public int CustomerId {get;set;}
        public int CusLocationId {get;set;}
        public int AddressTypeId {get;set;}
        public string AddressTo {get;set;}
        public string Address {get;set;}
        public string City {get;set;}
        public string Email {get;set;}
        public string Tel {get;set;}
        public string ZipPostalCode {get;set;}
        public int CountryId {get;set;}
        public string VatNo {get;set;}
        public string TaxNo {get;set;}
        public string TinNo {get;set;}
        public int CurrencyId {get;set;}
        public bool bActive {get;set;}
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; }
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; }
        public virtual MstrCustomerHeader MstrCustomerHeader { get; set; }
        public virtual MstrCustomerLocation MstrCustomerLocation { get; set; }
        public virtual MstrAddressType MstrAddressType { get; set; }
        public virtual MstrCountries MstrCountries { get; set; }
    }
}