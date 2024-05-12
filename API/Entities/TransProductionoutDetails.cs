using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Entities
{
    [Table("Trans_ProductionoutDetails")]
    public class TransProductionoutDetails
    {
         [Key]
        public long AutoIdx {get;set;}      
        public long POHId {get;set;} 
        public long SODId {get;set;} 
        public int OrderQty {get;set;} 
        public int ProductionOutQty {get;set;} 
        public int DamageQty {get;set;} 
        public int bActive {get;set;} 

    }
}