using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.DebitNoteHeader")]
    public class TransDebitNoteHeader
    {
        [Key]
        public long idDebitNoteHd {get;set;}
        public int CustomerId {get;set;}
        public string ReferenceNo {get;set;}
        public DateTime TransDate {get;set;}
        public int ReasonId {get;set;}
        public string Reason { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int CurrencyId {get;set;}
        public int DebitNoteType {get;set;}
        public decimal Exrate { get; set; }
        public decimal Totalamount { get; set; }
        public decimal Allocatedamount { get; set; }
        public bool bActive {get;set;}
        public int ModuleId { get; set; }
        public int CompanyId { get; set; }
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
    }
}