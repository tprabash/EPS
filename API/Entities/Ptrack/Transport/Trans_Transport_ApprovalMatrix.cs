using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("tms.Trans.ApprovalMatrix")]  
    public class Trans_Transport_ApprovalMatrix
    {
        [Key]
        public int idAMat { get; set; }
        public int idBType { get; set; }
        public int idTType { get; set; }      
        public int RequetedLocationid { get; set; }           
        public int ApprovedAgentsId { get; set; }
        public DateTime CreateDate { get; set; }  
        public bool bActive { get; set; }                        
    }
}

