using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{    [Table("Trans.SalesOderHdDefault")]
    public class TransSalesOrderDefault
    {
        [Key]
        public int CustomerId  { get; set; }
        public int CusCurrencyId  { get; set; }
        public int CustomerLocId  { get; set; }
        public int SalesCategoryId  { get; set; }
        public int CustomerUserId  { get; set; }
        public int SalesAgentId  { get; set; }
        public int CustomerDivId  { get; set; }
        public int PaymentTermId  { get; set; }
    }
}