using System;
using System.Collections.Generic;
using API.Entities;


namespace API.DTOs
{
    public class SaveProductionDto
    {
        public int Action { get; set; }
        public int LocationId { get; set; }
        public int ModuleId { get; set; }
        public int UserId { get; set; }
         public int RouteCardId { get; set; }
        public virtual IssueToProdcutionHeader sSIssueToProdcutionHeader { get; set; }
        public virtual List<IssueToProdcutionDetails> sIssueToProdcutionDetails { get; set; }
        public virtual IssueToProdcutionHeader sProductionQCHeader { get; set; }
        public virtual List<IssueToProdcutionDetails> sProductionQCDetails { get; set; }
    }
}