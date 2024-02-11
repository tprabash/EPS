using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Entities.Ptrack
{
     [Table("Trans.BreakdownTechnicalDetails")]
    public class Trans_BreakdownTechnicalDetails
    {
        [Key]
        public long AutoId { get; set; }     
        public long BrDoH_id { get; set; }
        public long BrDoHD_id { get; set; }     
        public long FAAid { get; set; }
        public long Footid { get; set; }     
        public long NBrandid { get; set; }
        public long NTypeid { get; set; }     
        public long NSizeid { get; set; }
        public long TBrandid { get; set; }
        public long TTypeid { get; set; }     
        public long TTopSizeid { get; set; }
        public long TBottomSizeid { get; set; }     
        public string InterLining { get; set; }        
        public bool bActive { get; set; }        
    }
}