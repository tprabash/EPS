using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.OCHeader")]
    public class TransOCHeader
    {
        [Key]
        public int AutoId {get;set;} 
        public string OCNo {get;set;}
        public DateTime OCDate {get;set;}
        public int CategoryId {get;set;}
        public string PORef {get;set;}
        public int CustomerId {get;set;}
        public int CustomerDepId {get;set;}
        public int SaleAgentId {get;set;}
        public int BuyerUserId {get;set;}
        public int BrandCodeId {get;set;}
        public int CurrencyId {get;set;}
        public int SeasonId {get;set;}
        public int EndCustomerId {get;set;}
        public int originCountryId {get;set;}
        public int PaymentTermId {get;set;}
        public int CustomerAddressId {get;set;}
        public int ProcessTypeId {get;set;}
        public string Remaks {get;set;}
        public int LocationId {get;set;}
        public int SystemId {get;set;}
        public int bActive {get;set;}

    }
}
