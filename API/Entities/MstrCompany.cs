using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{//
    [Table("Master.Company")]
    public class MstrCompany
    {
        [Key]
        public int AutoId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public int DefCurrencyId { get; set; }
        public string SVATNo { get; set; }
        public string BOIRegNo { get; set; }
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
    }
}