using System;
namespace API.DTOs.ptrack
{
    public class dashBoardTwoDto
    {
        public string tvDetails { get; set; }
        public string tvResult{ get; set; }
        public long tvQty{ get; set; }
        public int tvSeq { get; set; }
        public string factory { get; set; }
        public string season { get; set; }
        public string style { get; set; }
        public string styleId { get; set; }
        public string buyer { get; set; }
        public string buyerDivision { get; set; }
        public long qty{ get; set; }
        public string po { get; set; }
        public DateTime deldate { get; set; }
        public long cutqty{ get; set; }
        public long sewqty{ get; set; }
        public long washqty{ get; set; }
        public long packqty{ get; set; }
        public long shipqty{ get; set; }
    }
}