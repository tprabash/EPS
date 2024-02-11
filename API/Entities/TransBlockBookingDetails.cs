using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.MWS
{

    [Table("[Trans.BlockBookingDetails]")]

    public class TransBlockBookingDetails

    {

        [Key]
        public int idBBD {get;set;}
        public int idBBH {get;set;}        
        public int idProductGroup {get;set;}
        public int idSubCategory {get;set;}   
        public int idArticle {get;set;} 
        public int idSubCategoryGroup {get;set;}   
        public int idSeason {get;set;} 
        public int cMonth  {get;set;} 
        public int iYear  {get;set;} 
        public DateTime StartDate {get;set;}
        public DateTime EndDate {get;set;}
        public decimal Qty    {get;set;} 
        public decimal Price  {get;set;} 
        public decimal Value  {get;set;} 
        public DateTime BuyerDelDate {get;set;}
 
    }
}