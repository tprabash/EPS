using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities.Admin;

namespace API.Interfaces
{
    public interface IUserRepository
    {
         //void Update(MstrAgents user);

         Task<bool> SaveAllAsync();
         Task<IEnumerable<MstrAgents>> GetUsersAsync();
         Task<MstrAgents> GetUserByIdAsync(int id);
         Task<MstrAgents> GetUserByUsernameAsync(string username);
         Task<IEnumerable<SystemModule>> GetModuleAsync();
         Task<IEnumerable<MstrAgentLevel>> AgentLevelsAsync(int agentid);
         Task<IEnumerable<MstrAgents>> GetPermitedAgentsAsync(int agentid);
         Task<int> DisableUserAsync(StateUserDto agents);
         Task<int> ChangeUserPwdAsync(UserUpdateDto agents);
         
    }
}