using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.SalesOrderHeader")]
    public class TransSalesOrderHeader
    {
     
       [Key]
        public int AutoId { get; set; }
        public int SalesCategoryId { get; set; }
        public int POTypeId { get; set; }
        public int OCHIdx { get; set; }
        public int ParentId { get; set; }
        public string DocRefNo { get; set; }
        public DateTime BuyerOrderDate { get; set; }
        public DateTime BuyerDelDate { get; set; }
        public DateTime OCDelDate { get; set; }
        public DateTime FIHDate { get; set; }
        public int ShipmentModeId { get; set; }
        public int FinaldesCountrId { get; set; }
        public string FinalDest { get; set; }
        public int MarketId { get; set; }
        public int ArticleId { get; set; }
        public int BlockbookingId { get; set; }
        public string BuyerStyleRef { get; set; }
        public int ShipToAddressId { get; set; }
        public int BillToId { get; set; }
        public int DeliveryTermId { get; set; }
        public int SampleIssueToId { get; set; }
        public Decimal PcsWeight { get; set; }
        public int WashItemTypeId { get; set; }
        public string WashItemName { get; set; }
        public string Remaks { get; set; }
        public int SpecialOperationId { get; set; }
        public string CusWashType { get; set; }
        public int WashStandId { get; set; }
        public int SampleTypeId { get; set; }
        public string Coordinater { get; set; }
        public string Sender { get; set; }
        public Decimal CustomerPrice { get; set; }
        public Decimal TotalQuantity { get; set; }
        public int bActive { get; set; }
        public int CustomerLocId { get; set; }
        public int SalesordertypeId { get; set; }
        public int ProcessTypeId { get; set; }
        public string DeliveryOCId { get; set; }

    }
}
