using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.GMTValueUpdate")]
    public class TransGMTValueUpdate
    {
        [Key]
        public long AutoId {get;set;}  
        public long CustomerId {get;set;} 
        public long Articleid {get;set;} 
        public long SOHid {get;set;}  
        public string CusWashType {get;set;}      
        public string ActWashType {get;set;}  
     
    }
}
