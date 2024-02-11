namespace API.DTOs
{
    public class POAdditionalChargeDto
    {
        public long POChargesId { get; set; }
        public string Description { get; set; }
        public int AddChargeId { get; set; }
        public int BasisId { get; set; }
        public string Basis { get; set; }
        public decimal Value { get; set; }
    }
}
