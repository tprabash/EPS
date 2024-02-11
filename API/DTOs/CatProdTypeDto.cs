namespace API.DTOs
{
    public class CatProdTypeDto
    {
        public int AutoId { get; set; }        
        public string ProdTypeName { get; set; }
        public string ProdTypeCode { get; set; }
        public bool bAutoArticle { get; set; }
        public bool IsAssign { get; set; }

    }
}