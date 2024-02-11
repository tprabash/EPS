using System;

namespace API.DTOs
{
    public class ReturnJobCardDto
    {
        public int JobNo { get; set; }
        public int JobHeaderId { get; set; }
        public string OrderRef { get; set; }
        public string CustomerRef { get; set; }
        public string DeliveryRef { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int SOItemDtId { get; set; }
        public int SODelivDtId { get; set; }
        public int OrderQty { get; set; }
        public int PlanQty { get; set; }
        public int JobQty { get; set; }
        public int OldJobQty { get; set; }
        public int CustomerId { get; set; }
        public int ArticleId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public DateTime PlanDate { get; set; }
        public int CombinId { get; set; }        
        public DateTime JobDate { get; set; }
        public int DelivLocationId { get; set; }
        public string ArticleName { get; set; }
        public string Description1 { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Customer { get; set; }
        public string CustomerLoc { get; set; }
        public string Combination { get; set; }
        public string status { get; set; }

    }
}