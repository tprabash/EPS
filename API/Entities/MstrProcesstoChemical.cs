using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Entities
{
         [Table("Master.ProcesstoChemical")]
    public class MstrProcesstoChemical
    {
                [Key]
        public int AutoId {get;set;}
        public int ProcessID {get;set;}        
        public int ArticleId {get;set;}
        public int bActive {get;set;} 

    }
}