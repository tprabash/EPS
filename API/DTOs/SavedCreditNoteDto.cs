using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class SavedCreditNoteDto
    {
        public virtual TransCreditNoteHeader CreditNoteHeader { get; set; }
        public virtual List<CreditNoteDetailDto> CreditNoteDetails { get; set; }
    }
}
