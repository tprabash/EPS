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

        
        public int ActivityNo { get; set; }
        public int ModuleNo { get; set; }
        public int CompanyNo { get; set; }
        public int LocationNo { get; set; }
        public int AgentNo { get; set; }
        public int bActive { get; set; }

        public virtual TransPOAssociationHeader sTransPOAssociationHeader { get; set; }
        public virtual List<TransPOAssociationDetails> sTransPOAssociationDetails { get; set; }

        
        public virtual TransOCHeader sOCHeader {get; set;}
        public virtual TransSalesOrderHeader sSalesOrderHeader {get; set;}
        public virtual TransSalesOrderDeatils sSalesOrderDeatails {get; set;}


        //   public virtual List<TransPOAssociationHeader> sTransPOAssociationHeader { get; set; }

        //     public virtual List<TransPOAssociationDetails> sTransPOAssociationDetails { get; set; }
    }

}