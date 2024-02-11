using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.MTrack;
using API.DTOs;
using API.Interfaces;
using Dapper;

namespace API.Repository
{
    public class MTrackMasterRepository : DbConnMTrackRepositoryBase, IMTrackMasterRepository
    {

        private readonly IApplicationMTrackDbContext _context;
        public MTrackMasterRepository(IApplicationMTrackDbContext context, IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
            _context = context;
        }

        public async Task<IEnumerable<PermitMenuDto>> GetAuthMenuListAsyncMtrack(UserDto userDto)
        {
            IEnumerable<PermitMenuDto> menuList = Enumerable.Empty<PermitMenuDto>();
            DynamicParameters para = new DynamicParameters();

            para.Add("AgentId", userDto.UserId);

            if (userDto.ModuleId == 4)
            {
                return menuList = await DbConnection.QueryAsync<PermitMenuDto>("spMenuListAuthorize", para
                     , commandType: CommandType.StoredProcedure);
            }
            return menuList;
        }

        #region Get MachineBreaks
        public async Task<MachineCommonDto> GetFactoryWiseMachineBreakAsync(string location)
        {
            MachineCommonDto machine = new MachineCommonDto();
            DynamicParameters para = new DynamicParameters();

            para.Add("Location", location);

            using (var multi = await DbConnection.QueryMultipleAsync("spTransMachineBreakPendingList", para, commandType: CommandType.StoredProcedure))
            {
                machine.MachineBreakList = multi.Read<MachineBreakDto>();
                machine.MachineRepairList = multi.Read<MachineRepairDto>();
            }
            return machine;
        }
        #endregion Get MachineBreaks

        #region Update MachineBreaks
        public async Task<int> UpdateMachineBreakAsync(MachineBreakUpdateDto machine)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId", machine.AutoId);
            para.Add("ActionCode", machine.ActionCode);
            para.Add("RepaireId", machine.RepaireId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await DbConnection.ExecuteAsync("spTransMachineBreakUpdateDt", para
                , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }
        #endregion Update MachineBreaks

        #region Get Sample Data
        public async Task<IEnumerable<SampleDetailsGetDto>> GetSampleDetailsAsync(SampleDto sample)
        {
            DataTable SampleDT = new DataTable();
            IEnumerable<SampleDetailsGetDto> SampleList;

            DynamicParameters para = new DynamicParameters();

            SampleDT.Columns.Add("F01", typeof(string));
            SampleDT.Columns.Add("F02", typeof(long));
            SampleDT.Columns.Add("F03", typeof(long));
            SampleDT.Columns.Add("F04", typeof(int));
            SampleDT.Columns.Add("F05", typeof(string));
            SampleDT.Columns.Add("F06", typeof(string));
            SampleDT.Columns.Add("F07", typeof(bool));
            SampleDT.Columns.Add("F08", typeof(int));
            SampleDT.Columns.Add("F09", typeof(int));
            SampleDT.Columns.Add("F10", typeof(string));
            SampleDT.Columns.Add("F11", typeof(string));
            SampleDT.Columns.Add("F12", typeof(string));
            SampleDT.Columns.Add("F13", typeof(string));
            SampleDT.Columns.Add("F14", typeof(int));
            SampleDT.Columns.Add("F15", typeof(string));
            SampleDT.Columns.Add("F16", typeof(long));
            SampleDT.Columns.Add("F17", typeof(long));
            SampleDT.Columns.Add("F18", typeof(DateTime));
            SampleDT.Columns.Add("F19", typeof(DateTime));
            SampleDT.Columns.Add("F20", typeof(int));

            SampleDT.Rows.Add(
                sample.F01 != null ? sample.F01 : null
                , sample.F02 != null ? sample.F02 : 0
                , sample.F03 != null ? sample.F03 : 0
                , sample.F04 != null ? sample.F04 : 0
                , sample.F05 != null ? sample.F05 : null
                , sample.F06 != null ? sample.F06 : null
                , sample.F07 != null ? sample.F07 : null
                , sample.F08 != null ? sample.F08 : 0
                , sample.F09 != null ? sample.F09 : 0
                , sample.F10 != null ? sample.F10 : null
                , sample.F11 != null ? sample.F11 : null
                , sample.F12 != null ? sample.F12 : null
                , sample.F13 != null ? sample.F13 : null
                , sample.F14 != null ? sample.F14 : 0
                , sample.F15 != null ? sample.F15 : null
                , sample.F16 != null ? sample.F16 : 0
                , sample.F17 != null ? sample.F17 : 0
                , sample.F18 != null ? sample.F18 : null
                , sample.F19 != null ? sample.F19 : null
                , sample.F20 != null ? sample.F20 : 0
            );

            para.Add("UDT", SampleDT.AsTableValuedParameter("UDT_20"));

            SampleList = await DbConnection.QueryAsync<SampleDetailsGetDto>("sp_Production_SUD", para
                , commandType: CommandType.StoredProcedure);

            return SampleList;
        }
        #endregion Get Sample Data
    }
}