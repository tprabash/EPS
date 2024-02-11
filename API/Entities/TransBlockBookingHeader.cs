using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.MWS

{
     [Table("[Trans.BlockBookingHeader]")]
     
    public class TransBlockBookingHeader

    {
        [Key]
        public int idBBH {get;set;}
        public string BookingNo {get;set;}        
        public DateTime BookDate {get;set;}
        public int idBuyer {get;set;}   
        public int idCategory {get;set;} 
        public int idCurrency {get;set;}   
        public int idBuyerDepartment {get;set;} 
        public int BBType  {get;set;} 
        public int IdMerchandierMaster  {get;set;} 
        public int iStatusId  {get;set;} 
        public int idSystem  {get;set;} 
        public int idLocationId {get;set;} 
        public int CreateUserId  {get;set;} 
        public int bActive {get;set;}

    }
}
