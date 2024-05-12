using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Entities
{
    [Table("Trans_ProductionoutHeader")]
    public class TransProductionoutHeader
    {
         [Key]
        public long AutoId {get;set;}      
        public long ArticleId {get;set;} 
        public string DocNo {get;set;}  
        public int bActive { get; set; }
        public DateTime Transdatetime {get;set;}   
    }
}