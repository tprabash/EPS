using System;

namespace API.DTOs.ptrack
{
    public class FROrderDto
    {
        public string ProformaInvoiceNum2 {get;set;}
        public string BuyerPONumId {get;set;}
        public string ColorCodes {get;set;}
        public string Style {get;set;}
        public string orderNo {get;set;}
        public string Operation {get;set;}
        public int Qty {get;set;}
        public int FROrderIdx {get;set;}
        public int FROrderQty {get;set;}
        public int TotProdQty {get;set;}
    }
}