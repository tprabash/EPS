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
        public int OCHIdx { get; set; }
        public DateTime BuyerDelDate { get; set; }
        public string PoNo { get; set; }

    }
}
