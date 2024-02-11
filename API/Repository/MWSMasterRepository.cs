using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Entities.MWS;
using API.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class MWSMasterRepository : DbConnMWSRepositoryBase, IMWSMasterRepository
    {
        private readonly IApplicationMWSDbContext _context;
        public MWSMasterRepository(IApplicationMWSDbContext context, IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
            _context = context;
        }
        public async Task<IEnumerable<PermitMenuDto>> GetAuthMenuListAsyncMWS(UserDto userDto)
        {
              //IEnumerable<PermitMenuDto> menuList = Enumerable.Empty<PermitMenuDto>();
              IEnumerable<PermitMenuDto> menuList = Enumerable.Empty<PermitMenuDto>();
         
            DynamicParameters para = new DynamicParameters();

            para.Add("AgentId" , userDto.UserId);

            if (userDto.ModuleId == 3) {
               return menuList = await DbConnection.QueryAsync<PermitMenuDto>("spMenuListAuthorize" , para
                    , commandType: CommandType.StoredProcedure);
            }
                 
            return menuList;

        }  

    }
}

