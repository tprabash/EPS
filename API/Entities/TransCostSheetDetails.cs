using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Entities

{
    [Table("Trans.CostDetails")]
    public class TransCostSheetDetails
    {

        [Key]
        public long AutoId {get;set;}
        public long CHId {get;set;}
        public long ArticleId {get;set;}
        public long ColorId {get;set;}
        public long StyleColorId {get;set;}
        public long SizeId {get;set;}
        public long StyleSizeId {get;set;}
        public long UOMId {get;set;}
        public long FluteTypeId {get;set;}
        public decimal Factor {get;set;}
        public long BaseId {get;set;}
        public decimal Wastage {get;set;}
        public long SuppilerId  {get;set;}
        public decimal ConsQty {get;set;}
        public decimal Cost {get;set;}
        public decimal Value {get;set;}
    }
}
