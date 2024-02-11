namespace API.DTOs
{
    public class DispatchRequestDto
    {
        public int FromSiteId { get; set; }
        public int Category { get; set; }
        public int ProdGroup { get; set; }
        public int ProdType { get; set; }
    }
}
