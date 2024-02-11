namespace API.DTOs
{
    public class DashboarDetailsDto
    {
        public long id { get; set; }
        public string Ref { get; set; }
        public string CustomerRef { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public int Qty { get; set; }
        public decimal TValue { get; set; }
    }
}