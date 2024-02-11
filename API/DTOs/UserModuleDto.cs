namespace API.DTOs
{
    public class UserModuleDto
    {
        public int RowNo { get; set; }
        public int UserId { get; set; }                
        public int LocationId { get; set; }
        public int SysModuleId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int UserModId { get; set; }
        public int UserLocId { get; set; }
        public string ModuleName { get; set; }
        public string DBName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }
}