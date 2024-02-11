using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.CreditNoteHeader")]
    public class TransCreditNoteHeader
    {
        [Key]
        public long AutoId {get;set;}
        public int CustomerId {get;set;}
        public string ReferenceNo {get;set;}
        public DateTime TransDate {get;set;}
        public int noteTypeId {get;set;}
        public int idReason {get;set;}
        public string Remark { get; set; }
        public string Reference { get; set; }
        public int idCurrency {get;set;}
        public decimal ExchangeRate { get; set; }
        public decimal AllocatedTotal { get; set; }
        public bool IsActive {get;set;}
        public int ? CreateUserId {get;set;}
        public int ModuleId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ? CreateDateTime {get;set;}
        public string Code {get;set;}
    }
}
