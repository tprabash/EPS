namespace API.DTOs
{
    public class FileUploadResponseData
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string FileName { get; set; }
        public string ErrorMessage { get; set; }
    }
}