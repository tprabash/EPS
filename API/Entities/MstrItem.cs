using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Entities.Admin;

namespace API.Entities
{
    [Table("Master_Item")]
    public class MstrItem
    {
        [Key]
        public int AutoId { get; set; }
        public int ModuleId { get; set; }        
        public string Code { get; set; }
        public string ItemName { get; set; }
        public int Barcode { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int SizeId { get; set; }
        public int PrintId { get; set; }
        public int buyingRate { get; set; }
        public int SellingRate { get; set; }
        public decimal ROL { get; set; }
        public int bActive { get; set; }
 

    }
}