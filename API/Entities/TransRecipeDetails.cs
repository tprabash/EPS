using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.RecipeDetails")]
    public class TransRecipeDetails
    {
        [Key]
        public long AutoId {get;set;} 
        public long RHId {get;set;}     
        public long MainStep {get;set;}   
        public long SubStep {get;set;}   
        public long ParentId {get;set;}    
        public string Description {get;set;} 
        public long ProcessId {get;set;} 
        public long ArticleId {get;set;} 
        public Decimal WaterRatio {get;set;} 
        public Decimal WaterLevel {get;set;} 
        public Decimal PH {get;set;} 
        public long UseById {get;set;} 
        public Decimal ChemicalRatio {get;set;} 
        public Decimal UseQty {get;set;} 
        public long UOMId {get;set;} 
        public string MiddleRemaks {get;set;} 
        public long ProcessTime {get;set;} 
        public long Temperature {get;set;} 
        public Decimal RPM {get;set;} 
        public string StepRemaks {get;set;} 
        public bool bActive {get;set;}
        public bool AddRouteCard {get;set;}
    }
}
