using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class DebitNoteDto
    {
        public virtual IEnumerable<TransDebitNoteHeader> DebitNoteHeader { get; set; }
        public virtual IEnumerable<DebitNoteDetailDto> DebitNoteDetails { get; set; }
    }
}