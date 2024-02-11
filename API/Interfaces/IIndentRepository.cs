using API.DTOs;
using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IIndentRepository
    {
        Task<IEnumerable<AdhocIndentHeaderDto>> GetIntentHeaderAsync(IndentSearchDto searchDto);
        Task<IEnumerable<AdhocIndentDetailsDto>> GetIntentDetailsAsync(long indentHeaderId);
        Task<int> SaveAdhocIndentAsync(AdhocIndetSaveDto indentSaveDto);
        Task<int> ChangeIntentAssignedToAsync(IndentAssignToDto indentHd);
        Task<IEnumerable<AdhocIndentDetailsDto>> GetIntentDetailsByIdsAsync(IndentIdListDto indent);
    }
}
