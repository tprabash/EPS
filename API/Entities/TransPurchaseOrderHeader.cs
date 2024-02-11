using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.PurchaseOrderHeader")]
    public class TransPurchaseOrderHeader
    {
        [Key]
        public long POHeaderId { get; set; }
        public POStatus Status { get; set; }
        public string PORef { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CategoryId { get; set; }
        public string Attention { get; set; }
        public string OrderRef { get; set; }
        public long? SupplierId { get; set; }
        public DateTime? DeliveryStartDate { get; set; }
        public DateTime? DeliveryCancelDate { get; set; }
        public DateTime? DateInHouse { get; set; }
        public int? POTypeId { get; set; }
        public int? TaxId { get; set; }
        public int? PortOfLoading { get; set; }
        public int? PortOfDischarge { get; set; }
        public int? CountryOfOrign { get; set; }
        public int? CountryOfConsolidation { get; set; }
        public int? CountryOfFinalDestination { get; set; }
        public int? ForwardingAgent { get; set; }
        public int? CurrencyId { get; set; }
        public int? LocationId { get; set; }
        public int? PaymentTerm { get; set; }
        public int? ShipmentMode { get; set; }
        public int? DeliveryTerm { get; set; }
        public int? LeadTimeinDays { get; set; }
        public int? TransitTimeinDays { get; set; }
        public string SupplierReference { get; set; }
        public string PackingType { get; set; }
        public string Remarks { get; set; }
        public int? CreateUserId { get; set; }
        public int? ConsigneId { get; set; }
        public int? ConsigneAddId { get; set; }
        public int? BillToId { get; set; }
        public int? ShipToId { get; set; }
        public int? NotifyToId { get; set; }
        public int VersionNo { get; set; }
        public string Reason { get; set; }
        public SelectType SelectType { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }

    public enum POStatus
    {
        Created = 1,
        Waiting = 2,
        Approve = 3,
        Reject = 4,
        PartialAllocation = 5,
        Inactive = 6,
        Completed = 7,
    }

    public enum SelectType
    {
        Article = 1,
        Indent = 2
    }
}
