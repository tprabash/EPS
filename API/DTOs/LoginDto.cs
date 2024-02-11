using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginDto
    {               
        public string cAgentName { get; set; }       
        public string cPassword { get; set; }
         public int ModuleId { get; set; }
    }
}