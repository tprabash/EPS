namespace API.DTOs
{
    public class TranferGetDetailsDto
    {
        public long AutoId {get;set;}
        public string FromOrderRef { get; set; }
        public string ToOrderRef { get; set; }
        public string FromDelRef { get; set; }
        public string ToDelRef { get; set; }
        public string ArticleName { get; set; }
        public string BrandCode { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Qty { get; set; }
    }
}