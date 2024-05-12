namespace API.DTOs
{
    public class IssueToProductionDetails
    {
        public long AutoId {get;set;}
        public long MIHId { get; set; }
        public long ItemId { get; set; }
        public long SIHQty { get; set; }
        public long IssueQty { get; set; }
        public string UOM { get; set; }
    }
}