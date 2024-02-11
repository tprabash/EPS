using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
     [Table("Master.ArticleDetails")]
    public class MstrArticleDetails
    {
        [Key]
        public long AutoId {get;set;}
        public long ArticleId {get;set;}
        public string FabComp {get;set;}
        public long CustomerHeaderId {get;set;}
        public long BuyerId {get;set;}
        public long SeasonId {get;set;}
        public long GenderId {get;set;}
        public long FabricCategoryId {get;set;}
        public long WashTypeId {get;set;}
        public string CusWashRef {get;set;}
        public string BuyerStyRef {get;set;}
        public string ShipStyleName {get;set;}
        public string MaterialDes {get;set;}
    }
}