using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class SaveRecipeDto
    {
         public int  Action   { get; set; }
           public int FactoryId { get; set; }
           public int LocationId    { get; set; }
           public int ModuleId  { get; set; }
           public int UserId    { get; set; }
           public int AddNewFlag  { get; set; }
           public int RDidx    { get; set; }
           public int MoveStepFlag    { get; set; }
           public virtual TransRecipeHeader sRecipeHeader {get; set;}
           public virtual TransRecipeColorDetails sRecipeColorDetails {get; set;}
           public virtual TransRecipeDetails sRecipeDetails {get; set;}  

    }
}

