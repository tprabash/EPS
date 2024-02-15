using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{//
    [Table("Master_Company")]
    public class MstrCompany
    {
        [Key]
        public int AutoId { get; set; }
        public string Code { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string TelNo { get; set; }
        public string email { get; set; }
        public int ? VatNo {get;set;}
         public int ? bActive {get;set;}

        public string CompanyName { get; set; }
        public int DefCurrencyId { get; set; }
        public string SVATNo { get; set; }
        public string BOIRegNo { get; set; }
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}

}
}