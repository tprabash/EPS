using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class SaveArticleApperaleDetailDto
    {
        public int ActionId { get; set; }
        public int SystemId { get; set; }
        public int LocationId { get; set; }
        public int AgentId { get; set; }
        public virtual MstrArticle  sArticleMaster  { get; set; }    
        public virtual MstrArticleDetails  sArticleDetails  { get; set; }    
    }
}
