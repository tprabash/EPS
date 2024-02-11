using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("IE.Trans.OperationsAssignToEmployee")]
    public class TransOperationsAndSectionsAssignToEmp
    {
        [Key]
        public long AutoId {get;set;}
        public int FactoryId {get;set;}
        public int OperationId {get;set;}
        public decimal SMV {get;set;}
        public int EmployeeId {get;set;}
        public int Target {get;set;}
        public bool IsActive { get; set; }
        public int CreateUserId {get;set;}
        public DateTime CreateDateTime { get; set; }
        public int UpdateUserId {get;set;}
        public DateTime UpdateDateTime { get; set; }       
    }
}