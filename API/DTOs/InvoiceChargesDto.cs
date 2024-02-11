namespace API.DTOs
{
    public class InvoiceChargesDto
    {
        public int AddChargeId {get;set;}
        public int BasisId { get; set; }
        public string AddChargeType { get; set; }
        public string BasisType { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}