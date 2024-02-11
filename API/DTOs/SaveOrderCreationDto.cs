using System.Collections.Generic;
using API.Entities;


namespace API.DTOs
{
    public class SaveOrderCreationDto
    {
           public int  Action	{ get; set; }
           public int FactoryId	{ get; set; }
           public int LocationId	{ get; set; }
           public int ModuleId	{ get; set; }
           public int UserId	{ get; set; }
           public virtual TransAgreement sAgreement {get; set;}
           public virtual TransOCHeader sOCHeader {get; set;}
           public virtual TransSalesOrderHeader sSalesOrderHeader {get; set;}
           public virtual TransSalesOrderDeatils sSalesOrderDeatails {get; set;}
           public virtual TransSalesOrderAddtionalValue sSalesOrderAddtionalValue {get; set;}
           public virtual TransGMTValueUpdate sGMTValueUpdate {get; set;}
           public virtual TransGMTValueConfirmation sGMTValueConfirmation {get; set;}
           public virtual TransGMTPriceDetails sGMTPriceDetails {get; set;}
    }
}