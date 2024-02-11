using System.Collections.Generic;


namespace API.DTOs
{
    public class TransfairablePoRefDto
    {
        public long AutoId { get; set; }
        public long SoHeadrId { get; set; }        
        public string OrderRef { get; set; }
        public string CustomerRef { get; set; }
        public string DeliveryRef { get; set; }
        public string ArticleName { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Qty { get; set; }
        public string BrandCode { get; set; }
    }
}