using API.DTOs;
using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IStockAdjuestmentRepository
    {
        Task<int> SaveStockAdjuestmentAsync(List<TransStockAdjuestment> stock);
        Task<IEnumerable<TransInvStockDTO>> GetAdjuestmentGetStockAsync(int siteId);
    }
}
