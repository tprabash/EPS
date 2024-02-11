using System;
using API.Entities;

namespace API.DTOs
{
    public class FacProdOrderDto
    {
        public long SODelivDtId {get;set;}
        public long SOItemDtId {get;set;}
        public int Qty {get;set;}
        public virtual TransFtyProductionOrder FtyProductionOrderHd {get; set;}

    }
}