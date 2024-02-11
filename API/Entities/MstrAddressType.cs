using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.AddressType")]
    public class MstrAddressType
    {
        [Key]
        public int AutoId {get;set;}
        public string AddressCode {get;set;}
        public string AddressCodeName {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}

    }
}