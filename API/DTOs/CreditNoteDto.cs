using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class CreditNoteDto
    {
        public virtual IEnumerable<TransCreditNoteHeader> CreditNoteHeader { get; set; }
        public virtual IEnumerable<CreditNoteDetailDto> CreditNoteDetails { get; set; }
    }
}
