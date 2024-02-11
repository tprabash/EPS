using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Forwarder")] 
    public class MstrForwarder
    {
        [Key]
        public int ForwarderId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string EmailId { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
