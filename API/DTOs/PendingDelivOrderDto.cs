using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class PendingDelivOrderDto
    {
        public string OrderRef { get; set; }
        public string CustomerRef { get; set; }
        public string DeliveryRef { get; set; }

        // [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }
        public int SOItemDtId { get; set; }
        public int SODelivDtId { get; set; }
        public int OrderQty { get; set; }
        public int PlanQty { get; set; }
        public int JobQty { get; set; }
        public bool ? Status { get; set; }

    }
}