namespace API.DTOs
{
    public class IssueToProductionHeader
    {
        public long AutoId {get;set;}
        public long MIHId { get; set; }
        public long SOHId { get; set; }
        public long StyleId { get; set; }
        public string DocNo { get; set; }
        public string Remarks { get; set; }
    }
}