using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.DispatchAdditionalData")]
    public class TransDispatchAdditionalData
    {
        [Key]
        public long AutoId {get;set;}
        public long DHId {get;set;}
        public string ManualDspNo {get;set;}
        public int GTPassWshCatId {get;set;}
        public string NoOfBags {get;set;}
        public string CountedBy { get; set; }
        public string MemoNo { get; set; }
        public string FWDRName { get; set; }
        public string Comments { get; set; }
    }
}