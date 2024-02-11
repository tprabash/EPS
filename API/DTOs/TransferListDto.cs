using System;
namespace API.DTOs
{
    public class TransferListDto
    {
        public long AutoId { get; set; }            
        public string TransNo { get; set; }
        public DateTime TransDate { get; set; } 
        public string SiteName { get; set; }
        public string ArticleName { get; set; } 
        public string Color { get; set; }
        public string Size { get; set; }
        public string BrandCode { get; set; }
        public string Remark { get; set; }
        //public int CustomerId { get; set; } 
    }
}