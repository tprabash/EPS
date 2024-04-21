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
        public int OCHId {get;set;}
        public int SOHId {get;set;}
        public int MISPId {get;set;}
        public int MSId {get;set;}
        public int MPId {get;set;}
        public int OrderQty {get;set;}
        public int ProductionOutQty {get;set;}
        public int DamageQty {get;set;}
        public int ArticleColorSizeId {get;set;}
        public Decimal Price {get;set;}
        public Decimal Qty {get;set;}
        public Decimal DailyCommitQty {get;set;}
        public int bActive {get;set;}

    }
}
