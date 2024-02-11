using API.Entities;
using System;

namespace API.DTOs
{
    public class GRNReceiveListDto
    {
        public long POHeaderId { get; set; }
        public string PONo { get; set; }
        public string DeliveryCancelDate { get; set; }
        public string DeliveryStartDate { get; set; }
        public int CurrencyId { get; set; }
        public string Currency { get; set; }

    }
}
