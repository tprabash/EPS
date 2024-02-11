namespace API.DTOs
{
    public class ReturnCustomerHdDto
    {
        public int AutoId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string ShortCode {get;set;}
	    public string CustomerID {get;set;}
	    public string City {get;set;}
	    public int CountryId {get;set;}
	    public int CurrencyId {get;set;}
        public string CountryCode {get;set;}
	    public string CurrencyCode {get;set;}
	    public string VATNo {get;set;}
	    public string TaxNo {get;set;}
	    public string TinNo {get;set;}
	    public string ZipPostalCode {get;set;}
        public string Attention {get;set;}
	    public int CreditDays {get;set;}
        public bool bActive { get; set; }
        public int LocationId { get; set; }
        public int InvTypeId { get; set; }
        public int CusTypeId { get; set; }
        public string InvoiceType {get;set;}
        public string CustomerType {get;set;}
        public int TaxId { get; set; }
        public string Tax {get;set;}
    }
}