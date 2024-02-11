using System;
using System.ComponentModel.DataAnnotations.Schema;



namespace API.Entities
{
    [Table("Master.MachineMaster")]
    public class MstrMachineMaster
    {
        public int AutoId {get;set;}
        public int Machintypeid {get;set;}        
        public int MachineNo {get;set;}
        public int MaxKg {get;set;}
        public int Lots {get;set;}
        public decimal ElectricityConsumption {get;set;}
        public decimal SteamConsumption {get;set;}
        public decimal WaterConsumption {get;set;}
        public bool bActive {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}         
      

    }
}