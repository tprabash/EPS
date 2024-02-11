using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.CostingDetails")]
    public class TransCostingDetails
    {
        [Key]
        public long AutoId {get;set;}
        public long CostHeaderId {get;set;}
        public byte CostGroupId {get;set;}
        public int GroupOrder {get;set;}
        public long ArticleId {get;set;}
        public int ColorId {get;set;}
        public int SizeId {get;set;}
        public long ArticleColorSizeId {get;set;}
        public int UnitId {get;set;}
        public int GSM {get;set;}
        public int FluteId {get;set;}
        
        [Column(TypeName = "decimal(9,4)")]
        public decimal Factor  { get; set; }
        public string Base {get;set;}
        public decimal BaseValue {get;set;}
        public int ArtiUOMConvId {get;set;}
        public decimal NetCon {get;set;}
        public decimal Wastage {get;set;}
        public decimal GrossCon {get;set;}
        public decimal CostPcs {get;set;}
        public decimal ConsBase {get;set;}
        public int MultiCon {get;set;}
        public decimal Cost {get;set;}        
        public decimal Consumption {get;set;}        
        public decimal Weight {get;set;}
        public decimal Requirment {get;set;}       
        public decimal Value {get;set;}
        public int ? CreateUserId {get;set;}
        public DateTime ? CreateDateTime {get;set;}
        public int ? UpdateUserId {get;set;}
        public DateTime ? UpdateDateTime {get;set;}
        public TransCostingHeader CostingHeader {get;set;}
        public MstrCostingGroup CostingGroup {get;set;}
        public MstrArticle Article {get;set;}
        public MstrColor Color {get;set;}
        public MstrSize Size {get;set;}
        public MstrUnits Units {get;set;}

    }
}