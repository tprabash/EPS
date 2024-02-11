using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.Ports")]
    public class MstrPorts
    {
        [Key]
        public int PortId { get; set; }
        public string PortCode { get; set; }
        public string PortName { get; set; }
        public int CountryId { get; set; }
        public bool PortOfLoading { get; set; }
        public bool PortOfDischarge { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
