using API.Entities;
using System;
using System.Collections.Generic;

namespace API.DTOs
{
    

    public class TransDispatchDetailsDto
    {
        public long AutoId { get; set; }
        public long DispatchHdId { get; set; }
        public long? SOItemId { get; set; }
        public long? SODelivDtId { get; set; }
        public long? GRNHeaderId { get; set; }
        public long? GRNDetailsId { get; set; }
        public long? StockId { get; set; }
        public decimal? Price { get; set; }
        public int ProducedQty { get; set; }
        public int DispatchedQty { get; set; }
        public int BalDispatchQty { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
