using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
     [Table("Trans.POAssociationDetails")]
    public class TransPOAssociationDetails
    {
         [Key]
        public long AutoId {get;set;}      
        public long PAHId {get;set;} 
        public long SOHId {get;set;} 
        public long Qty {get;set;} 
        public long bActive {get;set;} 
    }
}