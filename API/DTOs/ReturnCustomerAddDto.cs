namespace API.DTOs
{
    public class ReturnCustomerAddDto
    {
        public int AutoId {get;set;}
        public int CustomerId {get;set;}
        public int CusLocationId {get;set;}
        public string Location {get;set;}
        public int AddressTypeId {get;set;}
        public string AddressTo {get;set;}
        public string AddressCode {get;set;}
        public string AddressCodeName {get;set;}
        public string Address {get;set;}
        public string City {get;set;}
        public string Email {get;set;}
        public string Tel {get;set;}
        public string ZipPostalCode {get;set;}
        public int CountryId {get;set;}
        public string Country {get;set;}
        public string VatNo {get;set;}
        public string TaxNo {get;set;}
        public string TinNo {get;set;}
        public bool bActive {get;set;}
        public int CurrencyId {get;set;}
        public string Currency {get;set;}
    }
}