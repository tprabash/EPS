using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("tms.Master.Transpoter")]      
    public class Master_Transport_Transpoter
    {
        [Key]
        public int idTrans { get; set; }       
        public string SupplierName { get; set; }
        public string OwnerName { get; set; }       
        public string ThirdPartyName { get; set; }
        public int BankCode { get; set; }     
        public string BankName { get; set; }  
        public int BranchCode { get; set; }  
        public string BranchName { get; set; }       
        public string BankACNo { get; set; }                            
    }
}

