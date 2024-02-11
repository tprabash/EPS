using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace API.Repository
{
    public class StockAdjuestmentRepository : DbConnCartonRepositoryBase, IStockAdjuestmentRepository
    {
        public StockAdjuestmentRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        public async Task<int> SaveStockAdjuestmentAsync(List<TransStockAdjuestment> stock)
        {
            DataTable StockDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            StockDT.Columns.Add("StockId", typeof(int));
            StockDT.Columns.Add("ArticleId", typeof(long));
            StockDT.Columns.Add("ColorId", typeof(long));
            StockDT.Columns.Add("SizeId", typeof(long));
            StockDT.Columns.Add("StockQty", typeof(int));
            StockDT.Columns.Add("Price", typeof(decimal));
            StockDT.Columns.Add("ExpiryDate", typeof(string));
            StockDT.Columns.Add("Remarks", typeof(string));
            StockDT.Columns.Add("ReasonId", typeof(int));

            foreach (var item in stock)
            {
                StockDT.Rows.Add(item.AutoId,
                    item.ArticleId,
                    item.ColorId,
                    item.SizeId,
                    item.StockQty,
                    item.Price,
                    item.ExpireDate,
                    item.Remarks,
                    item.ReasonId);

            }

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            para.Add("SiteId", stock[0].SiteId);
            para.Add("UserId", stock[0].CreatedUserId);
            para.Add("StockDT", StockDT.AsTableValuedParameter("StockAdjType"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<int>("spTransStockAdjuestmentSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<TransInvStockDTO>> GetAdjuestmentGetStockAsync(int siteId)
        {
            IEnumerable<TransInvStockDTO> stockList;
            DynamicParameters para = new DynamicParameters();

            para.Add("SiteId", siteId);

            stockList = await DbConnection.QueryAsync< TransInvStockDTO > ("spTransStockAdjuestmentGetStock", para
                    , commandType: CommandType.StoredProcedure);
            return stockList;
        }



    }
}
