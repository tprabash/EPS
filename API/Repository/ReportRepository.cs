using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Entities.Admin;
using API.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ReportRepository : DbConnCartonRepositoryBase, IReportRepository
    {    
        // private readonly IApplicationCartonDbContext _contex;

        public ReportRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        //public ReportRepository(IApplicationCartonDbContext contex, IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        //{
        //    _contex = contex;
        //}

        public async Task<int> SaveReportAsync(MstrReport report)
        {            
            DynamicParameters para = new DynamicParameters();
           
            para.Add("AutoId", report.AutoId);
            para.Add("Module", report.Module);
            para.Add("ReportName", report.ReportName);
            para.Add("SSRSReportName", report.SSRSReportName);
            para.Add("FromDate", report.FromDate);
            para.Add("ToDate", report.ToDate);
            para.Add("Purpose", report.Purpose);
            para.Add("UserId", report.CreateUserID);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);             

            var result = await DbConnection.ExecuteAsync("spMstrReportSave", para , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<ReportsReturnDto>> GetReportListAsync(ReportSearchDto dashDto)
        {
            IEnumerable<ReportsReturnDto> ReportList;
            DynamicParameters para = new DynamicParameters();

            para.Add("Module", dashDto.Module);
            para.Add("ModuleId", dashDto.ModuleId);

            ReportList = await DbConnection.QueryAsync<ReportsReturnDto>("spTransGetModuleWiseReports" , para
                    , commandType: CommandType.StoredProcedure);
            return ReportList;
        }
    }
}