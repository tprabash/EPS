using System;

namespace API.DTOs
{
    public class GRNHeaderListDto
    {
        public long GRNHeaderId { get; set; }
        public string GRNNo { get; set; }
        public int GRNTypeId { get; set; }
        public string GRNType { get; set; }
        public string Supplier { get; set; }
        public string ToSite { get; set; }
        public string FromSite { get; set; }
        public string DocNo { get; set; }
        public bool IsActive { get; set; }
        public string GRNDate { get; set; }
        public string GRNCreateUser { get; set; }
        public string GINNo { get; set; }
        public string GINDate { get; set; }
        public string GINUser { get; set; }
    }
}
