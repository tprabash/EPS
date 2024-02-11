using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Dapper;

namespace API.Repository
{
    public class TestRepository : DbConnCartonRepositoryBase ,ITestRepository 
    {
       
        public TestRepository(IDbConnectionFactory dbConnectionFactory): base(dbConnectionFactory)
        {
            
        }

        ///SINGLE RETURN
        public async Task<IEnumerable<MenuJoinList>> GetMenuListAsync()
        {
            IEnumerable<MenuJoinList> menuList;

            menuList = await DbConnection.QueryAsync<MenuJoinList>("spMenuListGetXML" , null
                    , commandType: CommandType.StoredProcedure);
            return menuList;
        }


        //// MULTI RETURNS
        // public async Task<IEnumerable<MenuJoinList>> GetMenuListAsync()
        // {
        //     IEnumerable<MenuJoinList> menuList;
        //     IEnumerable<MenuJoinList> menuList2;

        //     using (var connection = new SqlConnection(_connectionString.Value))
        //     {
        //         await connection.OpenAsync();

        //         using (var menus = await connection.QueryMultipleAsync("spMenuListGet",commandType: CommandType.StoredProcedure))
        //         {
        //             menuList = menus.Read<MenuJoinList>();
        //             menuList2 = menus.Read<MenuJoinList>();
        //         }                                
        //     }
        //     return menuList2;


        // }
    }
}