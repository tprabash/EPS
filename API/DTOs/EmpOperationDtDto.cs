using System;

namespace API.DTOs
{
    public class EmpOperationDtDto
    {
        public int AutoId {get;set;}
        public string EmployeeNo {get;set;}
        public string Employee {get;set;}
        public int EmployeeId { get; set; }
        public string Operation {get;set;}
        public string OperationCode {get;set;}
        public string Factory {get;set;}
        public decimal SMV {get;set;}
        public int Target {get;set;}
        public int OperationId { get; set; }
        public bool IsActive { get; set; }
    }
}