using API.Entities;

namespace API.DTOs
{
    public class MRSearchDto
    {
        public string CreatedBy { get; set; }
        public string MRNo { get; set; }
        public string CreatedDate { get; set; }
        public string SiteId { get; set; }
        public string CategoryId { get; set; }
        public string ArticleName { get; set; }
        public string ReqDate { get; set; }
        public string StatusId { get; set; }
    }
}
