using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class MstrUnitsDto
    {
        [Key]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }

        

    }
}