using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class SaveCostSheetDto
    {
        
           public int  Action   { get; set; }
           public int FactoryId { get; set; }
           public int LocationId    { get; set; }
           public int ModuleId  { get; set; }
           public int UserId    { get; set; }
           public virtual TransCostSheetHeader sCostSheetHeader {get; set;}
           public virtual TransCostSheetDetails sCostSheetDetails {get; set;}

    }
}

