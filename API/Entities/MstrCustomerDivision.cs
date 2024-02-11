using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.CustomerDivision")]
    public class MstrCustomerDivision
    {
        [Key]
        public int AutoId {get;set;}
        public int CustomerId {get;set;}
        public string Details {get;set;}
        public bool bActive {get;set;}
        public int CreateUserId {get;set;}
        public DateTime CreateDateTime {get;set;}
        public int UpdateUserId {get;set;}
        public DateTime UpdateDateTime {get;set;}
        public virtual MstrCustomerHeader CustomerHeader {get; set;}

    }
}