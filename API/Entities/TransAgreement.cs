using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Entities.Admin;

namespace API.Entities
{
    [Table("Trans.Agreement")]
    public class TransAgreement
    {
        [Key]
        public int AutoId {get;set;}
        public string AgreementNo {get;set;}
         public int CustomerId {get;set;}
        public int Articleid {get;set;}
        public int SOHid {get;set;}
        public DateTime AgreementDate {get;set;}
        public bool isSent {get;set;}
        public DateTime AggrementSentDate {get;set;}
        public int AgreementSentUser {get;set;}
        public int CustomerAgreedPrice {get;set;}
        public bool IsRecived {get;set;}
        public bool IsPriceIssue {get;set;}
        public bool IsFalloutIssue {get;set;}
        public int AgreementReceviedModeId {get;set;}
        public DateTime AgreementReceviedDate {get;set;}
        public int AgreementReceviedUser {get;set;}
        public bool IsCancelled {get;set;}
        public int AgreementCancelledUser {get;set;}
        public DateTime AgreementCancelledDate {get;set;}
     
    }
}
