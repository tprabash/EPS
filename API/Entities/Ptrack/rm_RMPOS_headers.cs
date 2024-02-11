using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Ptrack
{
    [Table("rm_RMPOS_headers")]
    public class rm_RMPOS_headers
    {
        // [Key]
        public long ? id {get;set;}
        [Key]
        public string PONo {get;set;}
        public long SupplierCode {get;set;}
        public string DeliveryLocation {get;set;}
        public DateTime CreateOn {get;set;}
        public string RevisedOn {get;set;}
        public string ApprovedOn {get;set;}
        public string Currency {get;set;}
        public DateTime DeliveryRequireDt {get;set;}
        public string CreatedBy {get;set;}
        public string BrandCode {get;set;}
        public bool IsMappingError {get;set;}
        public bool IsSendToSupplier {get;set;}
        public bool IsSupplierConfirmed {get;set;}
        public bool IsMailSendToUser {get;set;}
        public long CompanyCode {get;set;}
        public string POType {get;set;}
        public string Buyer {get;set;}
        public string RivisonCode {get;set;}
    }
}