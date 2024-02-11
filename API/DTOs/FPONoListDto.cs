using System;

namespace API.DTOs
{
    public class FPONoListDto
    {
        public long FPOId {get;set;}
        public string FPONo {get;set;}
        public string JobNo {get;set;}
        public string CustomerRef {get;set;}
        public DateTime StartDate {get;set;}
        public DateTime EndDate {get;set;}

    }
}