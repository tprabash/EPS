namespace API.DTOs
{
    public class UserLocationDto
    {  
        public int LocationId { get; set; }        
        public int CompanyId { get; set; }
        public string Location { get; set; }
        public string CompanyName { get; set; }
        public bool IsDefault { get; set; }
    }
}