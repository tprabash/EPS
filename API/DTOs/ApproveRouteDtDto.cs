namespace API.DTOs
{
    public class ApproveRouteDtDto
    {
        public int UserId { get; set; }
        public bool BuyPass { get; set; }
        public string cAgentName { get; set; }  
        public bool isDefault { get; set; }
        public bool isFinalApprove { get; set; }

    }
}