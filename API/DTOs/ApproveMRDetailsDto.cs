namespace API.DTOs
{
    public class ApproveMRDetailsDto
    {
        public long ArticleId { get; set; }
        public long ColorId { get; set; }
        public long SizeId { get; set; }
        public int ApproveQty { get; set; }
        public int UOMId { get; set; }
        public long MRDetailsId { get; set; }
    }
}
