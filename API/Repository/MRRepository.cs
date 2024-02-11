using API.DTOs;
using API.Entities;
using API.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using API.Data;

namespace API.Repository
{
    public class MRRepository : DbConnCartonRepositoryBase, IMRRepository
    {
        // private readonly IApplicationCartonDbContext _context;

        public MRRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        //public MRRepository(IApplicationCartonDbContext context, IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        //{
        //    _context = context;
        //}

        public async Task<int> SendMaterialRequestAsync(TransMRHeader mrHeader)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("MRHeaderId", mrHeader.MRHeaderId);
            para.Add("UserId", mrHeader.CreateUserId);
            para.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spTransMaterialRequestSave", para
                , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<ReturnDto> SaveMaterialRequestAsync(MaterialRequestDto mrDto)
        {
            DataTable MRDetails = new DataTable();
            DynamicParameters para = new DynamicParameters();

            MRDetails.Columns.Add("ArticleId", typeof(long));
            MRDetails.Columns.Add("ColorId", typeof(long));
            MRDetails.Columns.Add("SizeId", typeof(long));
            MRDetails.Columns.Add("ReqQty", typeof(int));
            MRDetails.Columns.Add("UOMId", typeof(int));
            MRDetails.Columns.Add("UnitPrice", typeof(decimal));
            MRDetails.Columns.Add("RequireDate", typeof(string));

            foreach (var item in mrDto.MRDetails)
            {
                MRDetails.Rows.Add(item.ArticleId
                        , item.ColorId
                        , item.SizeId
                        , item.ReqQty
                        , item.UOMId
                        , item.UnitPrice
                        , item.RequireDate
                    );
            }

            para.Add("MRHeaderId", mrDto.MRHeader.MRHeaderId);
            para.Add("MRNo", mrDto.MRHeader.MRNo);
            para.Add("SiteId", mrDto.MRHeader.SiteId);
            para.Add("LocationId", mrDto.MRHeader.LocationId);
            para.Add("AssignedTo", mrDto.MRHeader.AssignedTo);
            para.Add("StatusId", MRStatus.Created);
            //para.Add("Remark", mrDto.MRHeader.Remark);
            para.Add("CategoryId", mrDto.MRHeader.CategoryId);
            para.Add("UserId", mrDto.MRHeader.CreateUserId);

            para.Add("MRDetailsDT", MRDetails.AsTableValuedParameter("MRDetailsType"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransMaterialRequestSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<MaterialRequestGetDto>> GetMRDetailsAsync(long mrHeaderId)
        {
            IEnumerable<MaterialRequestGetDto> mrDetails;
            DynamicParameters para = new DynamicParameters();

            para.Add("MRHeaderId", mrHeaderId);

            mrDetails = await DbConnection.QueryAsync<MaterialRequestGetDto>("spTransMaterialRequestGetDt", para
                    , commandType: CommandType.StoredProcedure);
            return mrDetails;
        }

        public async Task<IEnumerable<MRHeaderDto>> GetMRNoListAsync(MRSearchDto searchDto)
        {
            IEnumerable<MRHeaderDto> mrHeader;
            DynamicParameters para = new DynamicParameters();

            para.Add("CategoryId", searchDto.CategoryId);
            para.Add("SiteId", searchDto.SiteId);
            para.Add("StatusId", searchDto.StatusId);
            para.Add("MRNo", searchDto.MRNo);
            para.Add("CreatedBy", searchDto.CreatedBy);
            para.Add("CreatedDate", searchDto.CreatedDate);
            para.Add("ArticleName", searchDto.ArticleName);
            para.Add("ReqDate", searchDto.ReqDate);

            mrHeader = await DbConnection.QueryAsync<MRHeaderDto>("spTransMaterialRequestGetMRList", para
                    , commandType: CommandType.StoredProcedure);
            return mrHeader;
        }

        public async Task<int> ApproveMaterialRequestAsync(ApproveMRDto approveMRDto)
        {
            DataTable MRDetails = new DataTable();
            DynamicParameters para = new DynamicParameters();

            MRDetails.Columns.Add("MRDetailsId", typeof(long));
            MRDetails.Columns.Add("ApproveQty", typeof(int));
            MRDetails.Columns.Add("ArticleId", typeof(long));
            MRDetails.Columns.Add("ColorId", typeof(long));
            MRDetails.Columns.Add("SizeId", typeof(long));
            MRDetails.Columns.Add("UOMId", typeof(int));

            foreach (var item in approveMRDto.ApproveMRDetails)
            {
                MRDetails.Rows.Add(item.MRDetailsId
                        , item.ApproveQty
                        , item.ArticleId
                        , item.ColorId
                        , item.SizeId
                        , item.UOMId
                    );
            }

            para.Add("MRHeaderId", approveMRDto.ApproveMRHeader.MRHeaderId);
            para.Add("RefNo", approveMRDto.ApproveMRHeader.RefNo);
            para.Add("ApproveId", approveMRDto.ApproveMRHeader.ApproveId);
            para.Add("Remarks", approveMRDto.ApproveMRHeader.Remarks);
            para.Add("AssignTo", approveMRDto.ApproveMRHeader.AssignedTo);
            para.Add("StatusId", approveMRDto.ApproveMRHeader.StatusId);
            para.Add("IsFinal", approveMRDto.ApproveMRHeader.IsFinal);
            para.Add("AssignUser", approveMRDto.ApproveMRHeader.AssignUser);
            para.Add("UserId", approveMRDto.ApproveMRHeader.UserId);
            para.Add("LocationId", approveMRDto.ApproveMRHeader.LocationId);
            para.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            para.Add("MRApproveDT", MRDetails.AsTableValuedParameter("MRApproveType"));

            await DbConnection.ExecuteAsync("spTransMaterialRequestApprove", para
                , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<int> CancelMRAsync(TransMRHeader mrHeader)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("UserId", mrHeader.CreateUserId);
            para.Add("MRHeaderId", mrHeader.MRHeaderId);
            para.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await DbConnection.QueryAsync<MRHeaderDto>("spTransMaterialRequestCancel", para
                    , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<TransStockDTO>> GetInventoryStockAsync(long mrHeaderId)
        {
            IEnumerable<TransStockDTO> stockList;
            DynamicParameters para = new DynamicParameters();

            para.Add("MRHeaderId", mrHeaderId);

            stockList = await DbConnection.QueryAsync<TransStockDTO>("spTransMaterialRequestGetInv", para
                    , commandType: CommandType.StoredProcedure);
            return stockList;
        }
    }
}
