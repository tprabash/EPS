using System.Collections.Generic;
using API.Entities.Ptrack;


namespace API.DTOs
{
    public class SaveTransportDto
    {
        public int Action { get; set; }
        public int FactoryId { get; set; }
        public int AgentId { get; set; }
        public virtual Master_Transport_VehicleType  sVehicletype  { get; set; }             
        public virtual Master_Transport_VehicleCategory  sVehiclecategory { get; set; }     
        public virtual Master_Transport_BookingType  sBookingtype { get; set; }    
        public virtual Master_Transport_TransportType  sTransporttype { get; set; }       
        public virtual Master_Transport_PaymentType  sPaymenttype { get; set; }      
        public virtual Master_Transport_Route  sRoute { get; set; }     
        public virtual Master_Transport_RouteFactoryAllocation  sRoutefactoryallocation { get; set; } 
        public virtual Master_Transport_VehicleOthers  sVehicleOthers  { get; set; }         
        public virtual Master_Transport_Transpoter  sTranspoter   { get; set; }         
        public virtual Master_Transport_TranspoterFactoryAllocation  sTranspoterFactoryAllocation  { get; set; }   
        public virtual Master_Transport_VehicleRegister  sVehicleRegister  { get; set; }  
        public virtual Trans_Transport_RateMatrix  sRateMatrix  { get; set; }       
        public virtual Trans_Transport_ApprovalMatrix  sApprovalMatrix   { get; set; }    
        public virtual Trans_Transport_BookingRequest  sBookingRequest   { get; set; }
        public virtual Trans_Transport_BookingRoute  sBookingRoute   { get; set; }                    
                               
    }
}

