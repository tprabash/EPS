using System;
namespace API.DTOs
{
    public class ReportsReturnDto
    {
        public int AutoId {get;set;}
        public string Module {get;set;}
        public string ReportNo {get;set;}
        public string ReportName {get;set;}
        public string SSRSReportName {get;set;}
        public bool IsActive {get;set;}
        public bool FromDate {get;set;}
        public bool ToDate {get;set;}
        public string Purpose {get;set;}
        public int ? CreateUserID {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserID {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
        public bool bCustomer {get;set;}
        public bool bDeliveryLocation {get;set;}
        public bool bCurrency {get;set;}
        public bool bBrand {get;set;}
        public bool bBrandCode {get;set;}
        public bool bCustomerPO { get; set;}
        public bool bInvoiceType { get; set; }
        public bool AsAtDate { get; set; }
        public int ModuleId { get; set; }
    }
}