using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("tms.Trans.RateMatrix")]  
    public class Trans_Transport_RateMatrix
    {
        [Key]
        public int idRMat { get; set; }
        public int idVT { get; set; }
        public int idVCat { get; set; }  
        public int idRouteFrom { get; set; } 
        public int idRouteTo { get; set; } 
        public int distance { get; set; } 
        public int Amount { get; set; }  
        public DateTime ? CreatedDate { get; set; } 
        public int idAgents { get; set; }  
        public DateTime ? DeletedDate { get; set; } 
        public int idAgentsDelete { get; set; } 
        public bool bActive { get; set; } 
        public int RouteType { get; set; }                                                                              
    }
}

