using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.SalesOrderHeader")]
    public class TransSalesOrderDeatils
    {
        [Key]
        public int AutoId {get;set;} 
        public int SOHId {get;set;}
        public int MISPId {get;set;}
        public int MSId {get;set;}
        public int MPId {get;set;}
        public int OrderQty {get;set;}
        public int ProductionOutQty {get;set;}
        public int DamageQty {get;set;}
        public Decimal Price {get;set;}

    }
}
