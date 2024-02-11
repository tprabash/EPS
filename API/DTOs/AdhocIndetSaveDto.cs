using API.Entities;
using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class AdhocIndetSaveDto
    {
        public virtual TransIndentHeader indentHeader { get; set; }
        public virtual List<TransIndentSODetails> IndentSODetails { get; set; }
        public virtual List<TransIndentDetails> indentDetails { get; set; }
    }
}
