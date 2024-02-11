namespace API.DTOs
{
    public class ReturnDto
    {
        public int Result { get; set; }
        public string RefNum { get; set; }
        public int RefNumId { get; set; }
        public int Version { get; set; }
        public string Status { get; set; }
        public string MINNo { get; set; }
        public int MINHeaderId { get; set; }
    }
}