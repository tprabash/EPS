using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.JobDetail")]
    public class TransJobDetail
    {
        [Key]
        public long AutoId { get; set; }
        public long JobHeaderId { get; set; }
        public long SOItemDtId { get; set; }
        public long SODelivDtId { get; set; }
        public int OrderQty { get; set; }
        public int JobQty { get; set; }
        public int ? CreateUserId { get; set; }
        public DateTime ? CreateDateTime { get; set; } = DateTime.Now;
        public int ? UpdateUserId { get; set; }
        public DateTime ? UpdateDateTime { get; set; } 
        public virtual TransJobHeader JobHeader {get; set;}
        public virtual TransSalesOrderItemDt SOItemDetail {get; set;}
        public virtual TransSalesOrderDeliveryDt SODelivDetail {get; set;}
        
    }
}