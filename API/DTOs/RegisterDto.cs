using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Key]
        public int idAgents { get; set; }
        [Required]
        public string cAgentName { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string cPassword { get; set; }
        public string cDescription { get; set; }        
        public string cEmail { get; set; }   

        [Required]
        public int iCategoryLevel { get; set; }
        

    }
}