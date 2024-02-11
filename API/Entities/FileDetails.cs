using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("FileDetails")]
    public class FileDetails
    {
        [Key]
        public int FileId {get;set;}
	    public string UserName {get;set;}
	    public byte[] DocFile {get;set;}
	    public string FileName {get;set;}
	    public long FileSize {get;set;}
	    public DateTime UploadDate {get;set;}
    }
}