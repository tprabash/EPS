using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.GMTPriceDetails")]
    public class TransGMTPriceDetails
    {
        [Key]
        public long AutoId {get;set;}  
        public long GMTVLupdateId {get;set;} 
        public long GMTVLconfirmId {get;set;} 
        public int WashStdId {get;set;}  
        public int Price {get;set;}          
    }
}
