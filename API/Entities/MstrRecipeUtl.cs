using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Entities
{
   [Table("Master.RecipeUtl")]
    public class MstrRecipeUtl
    {
        public int AutoId {get;set;}
        public string Code {get;set;}        
        public string Description {get;set;}
        public string UOM {get;set;}
        public decimal UnitRate {get;set;}
        public bool bActive {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}  

    }
}