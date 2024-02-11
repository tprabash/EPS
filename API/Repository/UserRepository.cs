using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities.Admin;
using API.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class UserRepository : DbConnAdminRepositoryBase, IUserRepository
    {
        private readonly IApplicationAdminDbContext _context;

        public UserRepository(IDbConnectionFactory dbConnectionFactory , IApplicationAdminDbContext context) : base(dbConnectionFactory)
        {
            _context = context;
        }

        public async Task<MstrAgents> GetUserByIdAsync(int id)
        {
            return await _context.MstrAgents.FindAsync(id);
        }

        public async Task<IEnumerable<MstrAgentLevel>> AgentLevelsAsync(int agentid)
        {
            var agentLevel = await _context.MstrAgents
                    .Where(x => x.idAgents == agentid)
                    .Select(p => new { p.iCategoryLevel })
                    .SingleOrDefaultAsync();

            return await _context.MstrAgentLevel
                    .Where(x => x.LevelPrority >= agentLevel.iCategoryLevel)
                    .ToListAsync();
        }

        public async Task<IEnumerable<MstrAgents>> GetPermitedAgentsAsync(int agentid)
        {
            /// GET LOGGED USER CATEGORY LEVEL
            var agentLevel = await _context.MstrAgents
                    .Where(x => x.idAgents == agentid)
                    .Select(p => new { p.iCategoryLevel })
                    .SingleOrDefaultAsync();

            return await _context.MstrAgents
                    .Where(x => x.iCategoryLevel >= agentLevel.iCategoryLevel && x.bActive == true)
                    .ToListAsync();
        }

        public async Task<IEnumerable<MstrAgents>> GetUsersAsync()
        {
            return await _context.MstrAgents
                .Where(s => s.bActive == true).ToListAsync();
        }

        public async Task<MstrAgents> GetUserByUsernameAsync(string username)
        {
            return await _context.MstrAgents
                .SingleOrDefaultAsync(x => x.cAgentName == username);
        }

        public async Task<IEnumerable<SystemModule>> GetModuleAsync()
        {
            return await _context.SystemModule.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync(default) > 0;
        }

       
        public async Task<int> DisableUserAsync(StateUserDto agents)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("bActive" , agents.bActive);
            para.Add("AgentId", agents.idAgents);
            para.Add("UserId", agents.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrAgentDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }  

        public async Task<int> ChangeUserPwdAsync(UserUpdateDto agents)
        {
            DynamicParameters para = new DynamicParameters();
         
            para.Add("AgentName", agents.cAgentName);
            para.Add("Password", agents.cPassword);
            para.Add("PasswordHash", agents.PasswordHash);
            para.Add("PasswordSalt", agents.PasswordSalt);
            para.Add("UserId", agents.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrAgentChangePasword", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }  


        // public void Update(MstrAgents user)
        // {
        //     _context.Entry(user).State = EntityState.Modified;
        // }
    }
}