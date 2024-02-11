using System;

namespace API.DTOs.MTrack
{
    public class MachineBreakDto
    {   
        public int AutoId {get;set;}
        public string Reference {get;set;}
        public string Brand {get;set;}
        public DateTime BreakdownDate {get;set;}
        public long MachineNo {get;set;}
        public string Model {get;set;}
        public string Type {get;set;}
        public string Factory {get;set;}
        public string Location {get;set;}
        public string Status {get;set;}
        public string Remark {get;set;}
        public string RepairEPF {get;set;}
        public int RepaireId {get;set;}
        public string RepaireName {get;set;}
        public DateTime StartDate {get;set;}
    }
}