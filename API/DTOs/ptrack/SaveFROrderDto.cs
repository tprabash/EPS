using System;
using System.Collections.Generic;

namespace API.DTOs.ptrack
{
    public class SaveFROrderDto
    {
        public string BuyerPONumID {get;set;}
        public string ColorCodes {get;set;}
        public string FRStyle {get;set;}
        public string MStyle {get;set;}
        public string OrderNo {get;set;}
        public string ProformaInvoiceNum2 {get;set;}
        public string Season {get;set;}
        public int ExtraOrderQty {get;set;}
        public virtual List<FRwfxRtnDto> FROrderDetails { get; set; }
    }

}