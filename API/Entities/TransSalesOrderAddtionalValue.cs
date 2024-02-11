using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.SalesOrderAddtionalValue")]
    public class TransSalesOrderAddtionalValue
    {
        [Key]
        public long AutoId {get;set;} 
        public long SOHIdx {get;set;}
        public long SOAId {get;set;}
        public string DetailsVal {get;set;}
        public bool bActive {get;set;}
    }
}