namespace API.DTOs
{
    public class TranferDetailsDto
    {
       public long idFPPOD_From { get; set; }
       public long SOHeaderId_From { get; set; }
       public int iQty { get; set; }
       public long idFPPOD_To { get; set; }
       public long SOHeaderId_To { get; set; }
    }
}