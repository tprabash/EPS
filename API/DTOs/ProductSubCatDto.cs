namespace API.DTOs
{
    public class ProductSubCatDto
    {
        public int AutoId { get; set; }
        public string ProdSubCatName { get; set; }
        public string ProdSubCatCode { get; set; }
        public int ProdGroupId { get; set; }
        public string ProdGroupName { get; set; }
        public bool ? IsActive { get; set; }   
    }
}