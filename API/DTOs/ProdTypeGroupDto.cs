namespace API.DTOs
{
    public class ProdTypeGroupDto
    {
        public int AutoId { get; set; }  
        public string ProdGroupName { get; set; }
        public string ProdGroupCode { get; set; }
        public int SerialNo { get; set; }
        public bool IsAssign { get; set; }
    }
}