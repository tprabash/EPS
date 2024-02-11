using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Entities
{
    [Table("[Master.UseBy]")]
    public class MstrUseBy
    {
                [Key]
        public int AutoId {get;set;}
        public string Code {get;set;}        
        public string Description {get;set;}
        public string UseBy {get;set;}        
        public string Units {get;set;}
        public string UOM {get;set;}
        public bool bActive {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}        

    }
}