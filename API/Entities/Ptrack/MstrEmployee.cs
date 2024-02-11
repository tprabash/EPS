using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("mas_Employee")]
    public class MstrEmployee
    {
        [Key]
        public long AutoId {get;set;}
        public string EmployeeNo {get;set;}
        public string Name {get;set;}
        public string Location {get;set;}
        public int link_Department {get;set;}
        public int link_Desgnation {get;set;}
        public string email {get;set;}
        public string Description { get; set; }      
    }
}