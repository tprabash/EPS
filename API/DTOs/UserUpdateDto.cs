namespace API.DTOs
{
    public class UserUpdateDto
    {             
        public string cAgentName { get; set; }
        public string cPassword { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int CreateUserId { get; set; }
    }
}