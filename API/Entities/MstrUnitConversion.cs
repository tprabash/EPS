using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.UnitConversion")]
    public class MstrUnitConversion
    {
        [Key]
        public int AutoId {get;set;}
        public int FromUnitId {get;set;}
        public int ToUnitId {get;set;}
        public double Value {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
        public virtual MstrUnits FromUnit {get;set;}
        public virtual MstrUnits ToUnit {get;set;}

    }
}