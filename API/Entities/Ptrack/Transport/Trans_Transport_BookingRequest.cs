using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("tms.Trans.BookingRequest")]  
    public class Trans_Transport_BookingRequest
    {
        [Key]
        public int idBR { get; set; } 
        public string RefNo { get; set; }  
        public string Requesteduser { get; set; }  
        public int RequetedLocationid { get; set; }
        public DateTime CreatedDate { get; set; }  
        public DateTime UpdateDate { get; set; } 
        public DateTime RequestedDate { get; set; } 
        public DateTime RequestValidDate { get; set; } 
        public int idBType { get; set; }  
        public int idTType { get; set; }    
        public int idVCat { get; set; }  
        public int idVR { get; set; }   
        public string DriverName { get; set; }  
        public string DriverID { get; set; }   
        public int TransporterID { get; set; }  
        public int iStatus { get; set; }   
        public string Remarks { get; set; }   
        public int AgenId { get; set; }
        public decimal transdays { get; set; }               
    }
}

