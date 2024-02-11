using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.AddChargeModule")]
    public class MstrAddChargeModule
    {
        [Key]
        public int AutoId { get; set; }
        public int ModuleId { get; set; }
        public string Module { get; set; }
        public int AddChargeId { get; set; }
        public string AddChargeType { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public bool bActive { get; set; }
    }
}