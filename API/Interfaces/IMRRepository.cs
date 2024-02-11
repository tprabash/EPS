using API.DTOs;
using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IMRRepository
    {
        Task<ReturnDto> SaveMaterialRequestAsync(MaterialRequestDto mrDto);
        Task<int> SendMaterialRequestAsync(TransMRHeader mrHeader);
        Task<IEnumerable<MaterialRequestGetDto>> GetMRDetailsAsync(long mrHeaderId);
        Task<IEnumerable<MRHeaderDto>> GetMRNoListAsync(MRSearchDto searchDto);
        Task<int> ApproveMaterialRequestAsync(ApproveMRDto approveMRDto);
        Task<int> CancelMRAsync(TransMRHeader mrHeader);
        Task<IEnumerable<TransStockDTO>> GetInventoryStockAsync(long mrHeaderId);
    }
}
