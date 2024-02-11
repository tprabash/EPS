using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.ReceiptHeader")]
    public class TransRecipeHeader
    {
        [Key]
        public long AutoId {get;set;}      
        public string RecipeCode {get;set;}  
        public DateTime RecipeDate {get;set;}   
        public long RecipeRevisionNo {get;set;}  
        public long CopyRHId {get;set;} 
        public long SalesordertypeId {get;set;} 
        public long SOHId {get;set;} 
        public long RecipeTypeId {get;set;} 
        public long MachineTypeId {get;set;} 
        public long MachineMasterId {get;set;} 
        public long pcsperBath {get;set;} 
        public Decimal pcsweight {get;set;} 
        public Decimal Loadweight {get;set;}
        public string TopComment {get;set;}   
        public string EndComment {get;set;}   
        public string ActualWashType {get;set;}   
        public bool bActive {get;set;}
    }
}

