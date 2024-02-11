namespace API.DTOs
{
    public class ProdGroupDto
    {
        public int AutoId { get; set; }
        public string ProdGroupName { get; set; }
        public string ProdGroupCode { get; set; }
        public int SerialNo { get; set; }
        public int ProdTypeId { get; set; }
        public string ProdTypeName { get; set; }
        public bool IsActive { get; set; }

    }
}