using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LocationDto
    {
        public int AutoId { get; set; }

        [Required]
        public string Location { get; set; }
    }
}