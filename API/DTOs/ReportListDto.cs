using System;

namespace API.DTOs
{
    public class ReportListDto
    {
        public int AutoIdx {get;set;}
        public string cReportName {get;set;}
        public string cModule {get;set;}
        public string ReportName {get;set;}
        public bool IsActive {get;set;}
        public bool FromDate {get;set;}
        public bool ToDate {get;set;}
        public string iReportNo {get;set;}
        public bool IsDelDate {get;set;}
        public bool IsBuyerDelDate {get;set;}
        public bool IsShipDate {get;set;}
        public bool IsUpdateDate {get;set;}
        public string Comments {get;set;}
        public string FilterList {get;set;}
        public byte ProcessStatus {get;set;}
        public string Purpose {get;set;}
        public string SSRSReportName {get;set;}
        public bool IsSSRS {get;set;}
        public int SSRSReportId {get;set;}
        public bool IsGroup {get;set;}
        public bool IsMultiFactory {get;set;}
        public bool IsSort {get;set;}
        public string Condition {get;set;}
        
    }
}