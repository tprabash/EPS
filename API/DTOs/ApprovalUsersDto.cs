namespace API.DTOs
{
    public class ApprovalUsersDto
    {
        public int AutoId { get; set; }
        public int idAgents { get; set; }
        public string cAgentName { get; set; }
        public bool BuyPass { get; set; }
        public bool isDefault { get; set; }
        public bool isFinalApprove { get; set; }
        public int UserId { get; set; }
        public string Module { get; set; }
    }
}
