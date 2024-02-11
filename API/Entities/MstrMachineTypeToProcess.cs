using System;
using System.ComponentModel.DataAnnotations.Schema;



namespace API.Entities
{
    [Table("Master.MachineTypeToProcess")]
    public class MstrMachineTypeToProcess
    {        
        public int AutoId {get;set;}
        public int ProcessId {get;set;}        
        public int MachineTypeId {get;set;}
        public bool bActive {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}  

    }
}