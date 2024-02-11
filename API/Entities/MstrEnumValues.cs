using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.EnumValues")]
    public class MstrEnumValues
    {
        [Key]
        public int EnumValueId { get; set; }
        public string EnumType { get; set; }
        public int ValueId { get; set; }
        public string ValueCode { get; set; }
        public string DisplayName { get; set; }
        public int IsActive { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
