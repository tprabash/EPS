using System;

namespace API.DTOs
{
    public class ApproveCenterDto
    {
        public int AutoId {get;set;}
        public string ModuleName {get;set;}
        public int AssigneUser {get;set;}
        public int RequestedBy {get;set;}
        public long RefId {get;set;}
        public string RefNo {get;set;}
        public string ReqUser {get;set;}
        public string Remarks {get;set;}
        public string Details {get;set;}
        public DateTime StatusDate {get;set;}
        public string Status {get;set;}
        public bool IsFinal {get;set;}
        public int ModuleId { get; set; }
    }
}