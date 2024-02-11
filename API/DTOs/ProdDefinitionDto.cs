namespace API.DTOs
{
    public class ProdDefinitionDto
    {
        public int AutoId { get; set; } 
        public int SeqNo { get; set; }
        public string PDName { get; set; } 
        public byte PDHeaderId { get; set; }           
        public int ProcessId { get; set; }  
        public int ReceiveSiteId { get; set; }        
        public int DispatchSiteId { get; set; }        
        public string ReceiveSite { get; set; }  
        public string DispatchSite { get; set; } 
        public string Process { get; set; } 
        public int ? CreateUserId { get; set; }
    }
}