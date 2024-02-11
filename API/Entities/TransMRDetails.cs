using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
	[Table("Trans.MRDetails")]
    public class TransMRDetails
    {
		[Key]
        public long MRDetailsId {get;set;}
	    public long MRHeaderId {get;set;}
	    public long ArticleId {get;set;}
	    public long ColorId {get;set;}
	    public long SizeId {get;set;}
	    public int ReqQty {get;set;}
	    public int ApprovedQty {get;set;}
	    public int UOMId {get;set;}
		[Column(TypeName = "decimal(9,4)")]
	    public decimal UnitPrice {get;set;}
	    public DateTime RequireDate {get;set;}
	    public int CreateUserId {get;set;}
	    public DateTime CreateDateTime {get;set;}
	    public int UpdateUserId {get;set;}
	    public DateTime UpdateDateTime {get;set;}

    }
}