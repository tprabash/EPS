using System;
namespace API.DTOs
{
    public class DashBoardStatsDto
    {
        public long NewOrders { get; set; }
        public long DispatchOrders { get; set; }
        public int CostUnAttachedItems { get; set; }
        public int DispPendingDCPO { get; set; }
        public int Costings { get; set; }
        public int MissedDeliveries { get; set; }
        public int NewOrderPcs { get; set; }
        public int DispatchedPcs { get; set; }
        public int DeliveryPendingPcs { get; set; }
        public int DeliveryMissedPcs { get; set; }
    }
}