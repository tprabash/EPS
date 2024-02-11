using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class TransIndentSODetails
    {
        public long IndentHeaderId { get; set; }
        public long SODelId { get; set; }
    }
}
