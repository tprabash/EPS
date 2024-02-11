using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class SavedDebitNoteDto
    {
        public virtual TransDebitNoteHeader DebitNoteHeader { get; set; }
        public virtual List<DebitNoteDetailDto> DebitNoteDetails { get; set; }
    }
}
