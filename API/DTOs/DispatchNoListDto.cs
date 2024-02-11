using System;

namespace API.DTOs
{
    public class DispatchNoListDto
    {
        public long DispatchHdId {get;set;}
        public string DispatchNo {get;set;}
        public string DispatchSite {get;set;}
        public string CustomerRef {get;set;}
        public string DeliveryLoc {get;set;}
        public string VehicleNo {get;set;}
        public string Reason {get;set;}
        public bool IsActive {get;set;}
        public DateTime TransDate {get;set;}
        public string Supplier { get; set; }
        public string ToSite { get; set; }
        public int DispatchType { get; set; }
    }
}