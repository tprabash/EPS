using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Entities
{

    [Table("Trans.IssueToProdcutionHeader")]
    public class IssueToProdcutionHeader
    {
            [Key]
    public long ITPHId { get; set; }
    public string DocNo { get; set; }
    public long CustomerId { get; set; }
    public long ArticleId { get; set; }
    public long RHId { get; set; }
    public long LocationId { get; set; }
    public long ModuleId { get; set; }
    public long AgentId { get; set; }
    public string Remarks { get; set; }
    public string IssuedEpf { get; set; }
    public string ReceivedEpf { get; set; }
    public string LotWeight { get; set; }
    public DateTime TransDate { get; set; }

    }
}