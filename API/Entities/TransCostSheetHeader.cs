using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Entities

{

    [Table("Trans.CostHeader")]

    public class TransCostSheetHeader
    {
        
        [Key]
        public long AutoId {get;set;}
        public string CostRefNo {get;set;}
        public DateTime? CostDate {get;set;}
        public int RevisionNo {get;set;}
        public long RHId {get;set;}
        public long SOId {get;set;}
        public long ArticleId {get;set;}
        public long BrandId {get;set;}
        public string Combination {get;set;}
        public int Status {get;set;}
        public string Remarks {get;set;}
        public DateTime? TransDate {get;set;}
        public bool bActive {get;set;}

    }
}
