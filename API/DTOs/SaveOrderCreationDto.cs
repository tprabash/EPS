using System.Collections.Generic;
using API.Entities;


namespace API.DTOs
{
    public class SaveOrderCreationDto
    {

           
        public int ActivityNo { get; set; }
        public int ModuleNo { get; set; }
        public int CompanyNo { get; set; }
        public int LocationNo { get; set; }
        public int AgentNo { get; set; }
        public int bActive { get; set; }

           public virtual TransSalesOrderHeader sSalesOrderHeader {get; set;}
           public virtual TransSalesOrderDeatils sSalesOrderDeatails {get; set;}
           
    }
}