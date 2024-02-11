using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.ErrorLog")]
    public class ErrorLog
    {
        [Key]
        public int ErrorLogID {get;set;}
        public DateTime ErrorTime {get;set;}
        public string UserName {get;set;}
        public int ErrorNumber {get;set;}
        public int ErrorSeverity {get;set;}
        public int ErrorState {get;set;}
        public string ErrorProcedure {get;set;}
        public int ErrorLine {get;set;}
        public string ErrorMessage {get;set;}
        public string Module {get;set;}

    }
}