
using System;
namespace API.DTOs.ptrack
{
    public class dashBoardTwoSearchDto
    {
        public int action { get; set; }
        public DateTime fDate {get;set;}
	    public DateTime tDate {get;set;}
        public int locationId { get; set; }

    }
}