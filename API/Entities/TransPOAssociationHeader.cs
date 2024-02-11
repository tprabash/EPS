using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Entities
{
    [Table("Trans.POAssociationHeader")]
    public class TransPOAssociationHeader
    {
         [Key]
        public long AutoId {get;set;}      
        public long CustomerId {get;set;} 
        public long ArticleId {get;set;} 
        public string PoName {get;set;}  
        public long PoQty {get;set;} 
        public Decimal PoPrice {get;set;} 
        public DateTime PoDate {get;set;}   
        public long AgentID {get;set;} 
    }
}