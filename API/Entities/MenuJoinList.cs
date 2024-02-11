using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    //[NotMapped]
    public class MenuJoinList
    {
        public int AutoIdx { get; set; }
        public string MenuName { get; set; }
        public string MenuDescription { get; set; }
        public string GroupName { get; set; }
        public string mType { get; set; }        
        public string MenuType { get; set; }
        
        //[Column(TypeName = "tinyint")]
        public int LevelPrority { get; set; }
        public string LevelDescription { get; set; }    

        //[Column(TypeName = "tinyint")]
        public int iCategoryLevel { get; set; }

    }
}