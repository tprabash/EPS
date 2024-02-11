using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Trans.SalesOrderFileUpload")]
    public class TransSalesOrderFileUpload
    {
        [Key]
        public long AutoId {get;set;}
        public long SOHeaderId {get;set;}
        public byte[] DocFile {get;set;}
        public string FileName {get;set;}
        public long FileSize {get;set;}
        public int UploadUserID {get;set;}
        public DateTime UploadDate {get;set;}
    }
}