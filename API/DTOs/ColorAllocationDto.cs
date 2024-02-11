namespace API.DTOs
{
    public class ColorAllocationDto
    {
        public int ColorId { get; set; }  
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsAsign { get; set; }
    }
}