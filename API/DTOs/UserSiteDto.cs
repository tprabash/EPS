namespace API.DTOs
{
    public class UserSiteDto
    {   
        public int UserSiteId { get; set; }     
        public int TypeId { get; set; }
        public int SiteId { get; set; }
        public int AgentId { get; set; }
        public int createUserId { get; set; }
        public string SiteName { get; set; }
    }
}
