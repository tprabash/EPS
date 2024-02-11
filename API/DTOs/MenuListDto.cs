using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs
{
    public class MenuListDto
    {
        public int AutoIdx { get; set; }       
        public string MenuName { get; set; }     
        public string MenuDescription { get; set; }      
        public string GroupName { get; set; }       
        public string mType { get; set; }

        [Column(TypeName = "tinyint")]
        public int AgentLevelId { get; set; }
        public int LevelPrority { get; set; }
        public string LevelDescription { get; set; }
        public int ? AgentId { get; set; }    }
}