﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ShipmentModes")]
    public class MstrShipmentModes
    {
        [Key]
        public int ShipModeId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
