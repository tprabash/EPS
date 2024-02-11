using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.RecipeColorDetails")]
    public class TransRecipeColorDetails
    {
        [Key]
        public long AutoId {get;set;}  
        public long RHId {get;set;} 
        public long ColorId {get;set;} 
        public bool bActive {get;set;}            
    }
}
