using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class SaveArticleDto
    {
        public virtual MstrArticle Article { get; set; }        
        // public int LocationId {get;set;}
        public virtual List<FlexFieldDto> FlexField { get; set; }
    }
}