namespace API.DTOs
{
    public class ProductTypeDto
    {
        public int AutoId { get; set; }  
        public string ProdTypeName { get; set; }
        public string ProdTypeCode { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public bool ? bAutoArticle { get; set; }
        public bool ? IsActive { get; set; }       
    }
}