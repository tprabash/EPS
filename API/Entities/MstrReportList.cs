using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Master.ReportList")]

    public class MstrReportList
    {
        [Key]
        public int AutoId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string ReportDescription { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Module  { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string ReportName  { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsActive  { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool FromDate  { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool ToDate  { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(10)")]
        public string ReportNo  { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsDelDate  { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsBuyerDelDate  { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsShipDate  { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsUpdateDate  { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        public string Comments  { get; set; }
        
        [Column(TypeName = "varchar(max)")]
        public string FilterList  { get; set; }

        [Required]
        [Column(TypeName = "tinyint")]
        [DefaultValue(0)]
        public int ProcessStatus  { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Purpose  { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string SSRSReportName  { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsSSRS  { get; set; }

        [Required]
        public int SSRSReportId  { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsGroup  { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsMultiFactory  { get; set; }
	
        [Required]
        [DefaultValue(false)]
        public bool IsSort  { get; set; }  
        
        [Column(TypeName = "varchar(10)")]
        public string Condition  { get; set; }       
        public int CreateUserID  { get; set; }       
        public DateTime CreateDateTime  { get; set; } = DateTime.Now;    
        public int UpdateUserID  { get; set; }        
        public DateTime UpdateDateTime  { get; set; } = DateTime.Now;
    }
}