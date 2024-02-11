namespace API.Entities
{
    public class UserReportList
    {
        public int AutoId { get; set; }       
        public string Module { get; set; }
        public string ReportNo { get; set; }
        public string ReportName { get; set; }
        public int isPermit{ get; set; }
    }
}
