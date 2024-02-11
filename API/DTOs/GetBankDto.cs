namespace API.DTOs
{
    public class GetBankDto
    {
        public int AutoId { get; set; }
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public string Branch { get; set; }
        public long NextChequeNo { get; set; }
        public int CurrencyId { get; set; }
        public string Code { get; set; }
    }
}