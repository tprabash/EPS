using System;
using System.Collections.Generic;

namespace API.DTOs.ptrack
{
    public class FRwfxDto
    {
        public string Action {get;set;}
        public string Season {get;set;}
        public string Style {get;set;}
        public string Spo {get;set;}
        public string Ordercode {get;set;}
        public int FROrderQty {get;set;}
        public int TotProdQty {get;set;}
        public int BalQty {get;set;}
        public int Sequence {get;set;}
        public int LineIn {get;set;}
        public string ProformaInvoiceNum2 {get;set;}
        public string BuyerPONumID {get;set;}
        public string ColorCodes {get;set;}
        public long POAssDetailIdx {get;set;}
        public virtual List<Spo> Spolist { get; set; }
    }

    public class Spo
    {
        public string spo { get; set; }
    }

}