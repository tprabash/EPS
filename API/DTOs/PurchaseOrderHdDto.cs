using API.Entities;
using System;

namespace API.DTOs
{
    public class PurchaseOrderHdDto
    {
        public long POHeaderId { get; set; }
        public POStatus Status { get; set; }
        public string PORef { get; set; }
        public string PONumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Category { get; set; }
        public string Attention { get; set; }
        public string OrderRef { get; set; }
        public string Supplier { get; set; }
        public DateTime DeliveryStartDate { get; set; }
        public DateTime DeliveryCancelDate { get; set; }
        public DateTime DateInHouse { get; set; }
        public string POType { get; set; }
        public string Tax { get; set; }
        public string ForwardingAgent { get; set; }
        public string Currency { get; set; }
        public string PaymentTerm { get; set; }
        public string ShipmentMode { get; set; }
        public string DeliveryTerm { get; set; }
        public int LeadTimeinDays { get; set; }
        public int TransitTimeinDays { get; set; }
        public string SupplierReference { get; set; }
        public string PackingType { get; set; }
        public string StatusName { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
    }
}
