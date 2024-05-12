using API.Entities;
using System.Collections;
using System.Collections.Generic;

namespace API.DTOs
{
    public class IssueToProductionSaveDto
    {
        public int ActivityNo { get; set; }
        public int ModuleNo { get; set; }
        public int CompanyNo { get; set; }
        public int LocationNo { get; set; }
        public int AgentNo { get; set; }
        public bool bActive { get; set; }
        public virtual IssueToProductionHeader sIssueToProductionHeader { get; set; }
        public virtual IEnumerable<IssueToProductionDetails> sIssueToProductionDetails { get; set; }
    }
}
