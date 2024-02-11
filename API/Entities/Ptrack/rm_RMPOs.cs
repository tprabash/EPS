using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("rm_RMPOs")]
    public class rm_RMPOs
    {        
        [ForeignKey("RMHeaders")]
       public string RMPO {get;set;}
        public DateTime CreatedDate {get;set;}
        public long SupplierId {get;set;}
        public long ArticleId {get;set;}
        public string ColorCode {get;set;}
        public string ColorName {get;set;}
        public string UOM {get;set;}
        public string Style {get;set;}
        public long LineSerialId {get;set;}
        public int LineItemNumber {get;set;}
        public decimal OrderQty {get;set;}
        public double UnitPrice {get;set;}
        public string Season {get;set;}
        public string StyleCode {get;set;}

        [ForeignKey("RMPO")]
        public virtual rm_RMPOS_headers RMHeaders {get; set;}
    }
}