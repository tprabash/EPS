using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.DispatchHeader")]
    public class TransDispatchHeader
    {
        [Key]
        public long AutoId {get;set;}
        public string DispatchNo {get;set;}
        public int? CustomerId {get;set;}
        public int? CusLocationId {get;set;}
        public DateTime? TransDate {get;set;}
        public int DispatchSiteId {get;set;}
        public int DispatchType { get; set; }
        public int? SupplierId { get; set; }
        public int? ToSiteId { get; set; }
        public string Reason {get;set;}
        public int? LocationId {get;set;}
        public bool? IsActive {get;set;}
        public string VehicleNo { get; set; }
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
        public int ? CancelUserId {get;set;}
        public DateTime ? CancelDateTime {get;set;}
    }
}