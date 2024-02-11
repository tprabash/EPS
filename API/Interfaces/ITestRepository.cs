using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface ITestRepository
    {
        Task<IEnumerable<MenuJoinList>> GetMenuListAsync();
    }
}