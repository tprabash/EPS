using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class SavePOAssociationDto
    {
        public int Action { get; set; }
        public int LocationId { get; set; }
        public int ModuleId { get; set; }
        public int UserId { get; set; }
        public virtual TransPOAssociationHeader sTransPOAssociationHeader { get; set; }
        public virtual List<TransPOAssociationDetails> sTransPOAssociationDetails { get; set; }


        //   public virtual List<TransPOAssociationHeader> sTransPOAssociationHeader { get; set; }

        //     public virtual List<TransPOAssociationDetails> sTransPOAssociationDetails { get; set; }
    }

}