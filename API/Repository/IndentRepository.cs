using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace API.Repository
{
    public class IndentRepository : DbConnCartonRepositoryBase, IIndentRepository
    {
        public IndentRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        public async Task<IEnumerable<AdhocIndentHeaderDto>> GetIntentHeaderAsync(IndentSearchDto searchDto)
        {
            IEnumerable<AdhocIndentHeaderDto> IntentHdList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CategoryId", searchDto.CategoryId);
            para.Add("AssignedTo", searchDto.AssignedTo);
            para.Add("Type", searchDto.Type);

            IntentHdList = await DbConnection.QueryAsync<AdhocIndentHeaderDto>("spTransIndentHeader", para
                    , commandType: CommandType.StoredProcedure);
            return IntentHdList;
        }

        public async Task<IEnumerable<AdhocIndentDetailsDto>> GetIntentDetailsAsync(long indentHeaderId)
        {
            IEnumerable<AdhocIndentDetailsDto> IntentDtList;
            DynamicParameters para = new DynamicParameters();

            para.Add("IndentHeaderId", indentHeaderId);

            IntentDtList = await DbConnection.QueryAsync<AdhocIndentDetailsDto>("spTransIndentDetails", para
                    , commandType: CommandType.StoredProcedure);

            return IntentDtList;
        }

        public async Task<IEnumerable<AdhocIndentDetailsDto>> GetIntentDetailsByIdsAsync(IndentIdListDto indent)
        {
            IEnumerable<AdhocIndentDetailsDto> IntentDtList;
            DynamicParameters para = new DynamicParameters();

            para.Add("IndentHeaderId", indent.IndentHeaderId);

            IntentDtList = await DbConnection.QueryAsync<AdhocIndentDetailsDto>("spTransIndentDetailsByIds", para
                    , commandType: CommandType.StoredProcedure);

            return IntentDtList;
        }

        public async Task<int> SaveAdhocIndentAsync(AdhocIndetSaveDto indentSaveDto)
        {
            DataTable IndentDetails = new DataTable();
            DataTable IndentSODt = new DataTable();

            DynamicParameters para = new DynamicParameters();

            IndentDetails.Columns.Add("ArticleId", typeof(long));
            IndentDetails.Columns.Add("MRDetailsId", typeof(long));            
            IndentDetails.Columns.Add("ColorId", typeof(long));
            IndentDetails.Columns.Add("SizeId", typeof(long));
            IndentDetails.Columns.Add("UOMId", typeof(int));
            IndentDetails.Columns.Add("TotQty", typeof(int));

            IndentSODt.Columns.Add("SODelId", typeof(long));

            para.Add("MRHeaderId", indentSaveDto.indentHeader.MRHeaderId);
            para.Add("UserId", indentSaveDto.indentHeader.CreateUserId);
            para.Add("TypeId", indentSaveDto.indentHeader.Type);
            para.Add("Type", Enum.GetName(typeof(IndentType), indentSaveDto.indentHeader.Type));

            foreach (var item in indentSaveDto.indentDetails)
            {
                IndentDetails.Rows.Add(item.ArticleId
                       , item.MRDetailsId
                       , item.ColorId
                       , item.SizeId
                       , item.UOMId
                       , item.OpenQty);

            }

            foreach (var item in indentSaveDto.IndentSODetails)
            {
                IndentSODt.Rows.Add(item.SODelId);

            }

            para.Add("@IndentSODT", IndentSODt.AsTableValuedParameter("IndentDetailsSOType"));
            para.Add("@IndentDT", IndentDetails.AsTableValuedParameter("IndentDetailsType"));
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransIndentSave", para
                , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<int> ChangeIntentAssignedToAsync(IndentAssignToDto indentHd)
        {
            DataTable IndentDetails = new DataTable();

            DynamicParameters para = new DynamicParameters();

            IndentDetails.Columns.Add("AssignTo", typeof(int));
            IndentDetails.Columns.Add("IndentHeaderId", typeof(long));
            IndentDetails.Columns.Add("UserId", typeof(int));

            foreach (var item in indentHd.IndentHeader)
            {
                IndentDetails.Rows.Add(indentHd.AssignTo, item.IndentHeaderId , indentHd.UserId);
            }

            para.Add("@IndentAssignDT", IndentDetails.AsTableValuedParameter("IndentAssignToType"));
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransIndentChangeAssignTo", para
                , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }
    }
}
