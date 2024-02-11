using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.DTOs.ptrack;
using API.Entities;
using API.Entities.Ptrack;
using API.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace API.Repository
{
    public class PTrackMasterRepository : DbConnPTrackRepositoryBase, IPTrackMasterRepository
    {
        private readonly IApplicationPTrackDbContext _context;
        public PTrackMasterRepository(IApplicationPTrackDbContext context, IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReportListDto>> GetReportMenuListAsync()
        {   
            IEnumerable<ReportListDto> coloList;
            // DynamicParameters para = new DynamicParameters();

            // para.Add("ArticleId" , articleId);
            //para.Add("LocationId", articleDto.LocationId);

            coloList = await DbConnection.QueryAsync<ReportListDto>("spMPlusReportGetDetails" , null
                    , commandType: CommandType.StoredProcedure);
            
            return coloList;
        } 

        public async Task<PurchaseOrdersDto> GetPurchaseOrdersAsync()
        {   
            PurchaseOrdersDto ordersList = new PurchaseOrdersDto();

            using (var multi = await DbConnection.QueryMultipleAsync("spPurchaseOrdersToXML", null, commandType: CommandType.StoredProcedure))
            {
                ordersList.PurchaseOrder = multi.Read<PurchaseOrderDto>();
                ordersList.LineItem = multi.Read<LineItemDto>();
            }
            return ordersList;
        } 

        // public async Task<PurchaseOrders> GetPurchaseOrderXMLAsync()
        // {  
        //     PurchaseOrders orders = new PurchaseOrders();

        //     // using (var multi = DbConnection.QueryMultiple(sql, new {InvoiceID = 1}))
        //     // using (var multi = await DbConnection.QueryMultipleAsync("spPurchaseOrdersToXML", null, commandType: CommandType.StoredProcedure))
        //     // {
        //     //     orders.PurchaseOrder = multi.Read<PurchaseOrder>();
        //     //     orders.LineItem = multi.Read<LineItem>();
        //     // }
        //     return orders;
        // } 

        public async Task<IEnumerable<PermitMenuDto>> GetAuthMenuListAsyncPtrack (UserDto userDto)
        {
            IEnumerable<PermitMenuDto> menuList = Enumerable.Empty<PermitMenuDto>();
            DynamicParameters para = new DynamicParameters();

            para.Add("AgentId" , userDto.UserId);

            if (userDto.ModuleId == 2) {
               return menuList = await DbConnection.QueryAsync<PermitMenuDto>("spMenuListAuthorize" , para
                    , commandType: CommandType.StoredProcedure);
            }
                 
            return menuList;

        }  

      #region "WorkStudy"
        public async Task<ReturnDto> WorkStudySavedata(List<SaveWorkStudyDto> wsDt) 
        {
           DataTable WorkStudyDT = new DataTable();

            DynamicParameters para = new DynamicParameters();

            WorkStudyDT.Columns.Add("F01", typeof(string));
            WorkStudyDT.Columns.Add("F02", typeof(string));
            WorkStudyDT.Columns.Add("F03", typeof(string));
            WorkStudyDT.Columns.Add("F04", typeof(string));
            WorkStudyDT.Columns.Add("F05", typeof(string));
            WorkStudyDT.Columns.Add("F06", typeof(decimal));
            WorkStudyDT.Columns.Add("F07", typeof(decimal));
            WorkStudyDT.Columns.Add("F08", typeof(decimal));     
            WorkStudyDT.Columns.Add("F09", typeof(int));
            WorkStudyDT.Columns.Add("F10", typeof(int));
            WorkStudyDT.Columns.Add("F11", typeof(int)); 
            WorkStudyDT.Columns.Add("F12", typeof(Int64));
            WorkStudyDT.Columns.Add("F13", typeof(Int64));
            WorkStudyDT.Columns.Add("F14", typeof(Int64));    
            WorkStudyDT.Columns.Add("F15", typeof(string));
            WorkStudyDT.Columns.Add("F16", typeof(string));
            WorkStudyDT.Columns.Add("F17", typeof(string));
            WorkStudyDT.Columns.Add("F18", typeof(int));     
            WorkStudyDT.Columns.Add("F19", typeof(Int64));   
            WorkStudyDT.Columns.Add("F20", typeof(int));   
             WorkStudyDT.Columns.Add("F21", typeof(decimal));   
            WorkStudyDT.Columns.Add("F22", typeof(decimal));       

           
              foreach (var item in wsDt)
            {
              var Test =item.Action;
              var FactoryId = item.FactoryId;
              var AgentId = item. AgentId;
               
              if (item.SOperationGroup != null)
                {                  
                      WorkStudyDT.Rows.Add(
                        item.SOperationGroup.Code,
                        0,
                        0,
                        item.SOperationGroup.Description,
                        item.SOperationGroup.bActive,
                        0,
                        0,
                        0,
                        Test,
                        item.SOperationGroup.OpGrId,
                        0,
                        0,
                        0,
                        0, 0, 0, 0, 0, 0,
                        0
                    );
                }
              if (item.SSection != null)
                {                  
                      WorkStudyDT.Rows.Add(
                        item.SSection.Code,
                        0,
                        0,
                        item.SSection.Description,
                        item.SSection.bActive,
                        0,
                        0,
                        0,
                        Test,
                        item.SSection.SecId,
                        0,
                        0,
                        0,
                        0, 0, 0, 0, 0, 0,
                        0
                    );
                }
              if (item.SMachinetype != null)
                {                  
                      WorkStudyDT.Rows.Add(
                        0,
                        0,
                        0,
                        item.SMachinetype.Description,
                        item.SMachinetype.bActive,
                        0,
                        0,
                        0,
                        Test,
                        item.SMachinetype.MachineTypeId,
                        0,
                        0,
                        0,
                        0, 0, 0, 0, 0, 0,
                        0
                    );
                }
              if (item.SOperation != null)
                {                  
                      WorkStudyDT.Rows.Add(
                      item.SOperation.Code,
                        0,
                        0,
                        item.SOperation.Description,
                        item.SOperation.bActive,
                        item.SOperation.SMV,
                        0,
                        0,
                        Test,
                        item.SOperation.OpIdx,
                        item.SOperation.MachineTypeIdx,
                        item.SOperation.HourlyTarget,
                        item.SOperation.HrsTargetedPercent,
                        item.SOperation.OpGIdx,
                          0, 0, 0, 0, 0,
                        0
                    );
                }
              if (item.SFtywiseOperations != null)
                               {                  
                        WorkStudyDT.Rows.Add(
                        0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          Test,
                          item.SFtywiseOperations.ftyWOpId,
                          0,
                          item.SFtywiseOperations.ftycodeid,
                          0,
                          0,
                          0,
                            0, 0, 0, 0,
                          0
                      );
                  }
              if (item.SFtywiseSections != null)
                  {                  
                        WorkStudyDT.Rows.Add(
                        0,
                          0,
                          0,
                          0,
                            item.SFtywiseSections.bActive,
                          0,
                          0,
                          0,
                          Test,
                          item.SFtywiseSections.FtyWiseSecId,
                          item.SFtywiseSections.SecId,
                          item.SFtywiseSections.ftycodeid,
                          0,
                          item.SFtywiseSections.SectionIdx,
                          0,
                            0, 0, 0, 0,
                          0
                      );
                  }
              if (item.SFtywiseOperationsAndSectionss != null)
                  {                  
                        WorkStudyDT.Rows.Add(
                        0,
                          0,
                          0,
                          0,
                          item.SFtywiseOperationsAndSectionss.bActive,
                          0,
                          0,
                          0,
                          Test,
                          item.SFtywiseOperationsAndSectionss.ftyWOpSecId,
                          item.SFtywiseOperationsAndSectionss.OpId,
                          item.SFtywiseOperationsAndSectionss.ftycodeid,
                          0,
                          item.SFtywiseOperationsAndSectionss.FtyWiseSecId,
                          0,
                          0, 0, 0, 0,
                          0
                      );
                  }                
              if (item.SBreakdownHeader != null)
                  {    

                        WorkStudyDT.Rows.Add(
                          0,
                          0,
                          0,
                          0,
                          item.SBreakdownHeader.bActive, 
                          item.SBreakdownHeader.iEfficiency,
                        //0,
                          0,
                          0,
                          Test,
                          item.SBreakdownHeader.BrDoH_id,
                        // 0,
                          0,
                          item.SBreakdownHeader.styleID,
                        // 0,
                          0,
                          0,
                          0,
                          0, 'H', 0, 
                          AgentId,
                          FactoryId
                      );
                  }
              if (item.SBreakdownDetails != null)
                {                  
                      WorkStudyDT.Rows.Add(
                        0,
                        0,
                        0,
                        0,
                        0,
                      0,
                        0,
                        0,
                        Test,
                        0,
                        0,
                        0,
                        item.SBreakdownDetails.OpId,
                        0,                       
                        0,
                        0,
                        'D',
                        item.SBreakdownDetails.Sequence, 
                        AgentId,
                        FactoryId,
                        item.SBreakdownDetails.TotSMV
                    );
                }
              if (item.SFtyLayoutHeader != null)
                {    
                        WorkStudyDT.Rows.Add(
                          0,
                          0,
                          0,
                          0,
                          item.SFtyLayoutHeader.bActive, 
                          0,
                        //0,
                          0,
                          0,
                          Test,
                          item.SFtyLayoutHeader.FtyLayId,
                        // 0,
                          0,
                          item.SFtyLayoutHeader.styleID,
                        // 0,
                          item.SFtyLayoutHeader.BrDoH_id,
                          0,
                          0,
                          0, 'H', 0, 
                          AgentId,
                          FactoryId
                      );
                  }
              if (item.SFtyLayoutDetails != null)
                {                  
                      WorkStudyDT.Rows.Add(
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        item.SFtyLayoutDetails.ftySMV,
                        0,
                        Test,
                        0,
                        item.SFtyLayoutDetails.OpId,
                        item.SFtyLayoutDetails.ftySequence,
                        0,
                        item.SFtyLayoutDetails.ftyHourlyTarget,                       
                        item.SFtyLayoutDetails.FtyLayDId,
                        0,
                        'D',
                        item.SFtyLayoutDetails.ftyWOpSecId, 
                        AgentId,
                        FactoryId,
                        item.SFtyLayoutDetails.ManPower,
                        item.SFtyLayoutDetails.MachineQty
                    );
                }                
              if (item.SStylewiseSMV != null)
                {                  
                      WorkStudyDT.Rows.Add(
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        item.SStylewiseSMV.sMV_Value,
                        0,
                        Test,
                        item.SStylewiseSMV.styleId,
                        0,
                        0,
                        item.SStylewiseSMV.link_Cat,
                       0,                       
                        0,
                        0,
                        0,
                        0, 
                        AgentId,
                        FactoryId,
                        0,
                        0
                    );
                } 
              if (item.sDepartmentWiseSection != null)
                {
                    WorkStudyDT.Rows.Add(
                      0,
                      0,
                      item.sDepartmentWiseSection.bActive,
                      0,
                      0,
                      0,
                      0,
                      0,
                      Test,
                      item.sDepartmentWiseSection.DeptSecId,
                      item.sDepartmentWiseSection.DeptId,
                      0,
                      0,
                      0, 0, 0, 0,
                      item.sDepartmentWiseSection.SecId
                      ,0
,                     0,
                      0,0
                  );
                }
              if (item.Sfolderandattachment != null)
                {                  
                      WorkStudyDT.Rows.Add(
                        item.Sfolderandattachment.Code,
                        0,
                        0,
                        item.Sfolderandattachment.Description,
                        item.Sfolderandattachment.bActive,
                        0,
                        0,
                        0,
                        Test,
                        item.Sfolderandattachment.AutoId,
                        0,
                        0,
                        0,
                        0, 0, 0, 0, 0, 0,
                        0
                    );
                }
              if (item.SFoot != null)
                {                  
                      WorkStudyDT.Rows.Add(
                        item.SFoot.Code,
                        0,
                        0,
                        item.SFoot.Description,
                        item.SFoot.bActive,
                        0,
                        0,
                        0,
                        Test,
                        item.SFoot.AutoId,
                        0,
                        0,
                        0,
                        0, 0, 0, 0, 0, 0,
                        0
                    );
                }
              if (item.SBrand != null)
                {                  
                      WorkStudyDT.Rows.Add(
                        item.SBrand.Code,
                        0,
                        0,
                        item.SBrand.Description,
                        item.SBrand.bActive,
                        0,
                        0,
                        0,
                        Test,
                        item.SBrand.AutoId,
                        0,
                        0,
                        0,
                        item.SBrand.ToolId, 0, 0, 0, 0, 0,
                        0
                    );
                }     
              if (item.SType != null)
                {                  
                      WorkStudyDT.Rows.Add(
                        item.SType.Code,
                        0,
                        0,
                        item.SType.Description,
                        item.SType.bActive,
                        0,
                        0,
                        0,
                        Test,
                        item.SType.AutoId,
                        0,
                        0,
                        0,
                        item.SType.ToolId, 0, 0, 0, 0, 0,
                        0
                    );
                }
              if (item.SSize != null)
                {                  
                      WorkStudyDT.Rows.Add(
                        item.SSize.Code,
                        0,
                        0,
                        item.SSize.Description,
                        item.SSize.bActive,
                        0,
                        0,
                        0,
                        Test,
                        item.SSize.AutoId,
                        0,
                        0,
                        0,
                        item.SSize.ToolId, 0, 0, 0, 0, 0,
                        0
                    );
                } 
              if (item.SBreakdownTechnicalDetails != null)
                {                  
                      WorkStudyDT.Rows.Add(
                        0,
                        0,
                        0,
                        item.SBreakdownTechnicalDetails.InterLining,
                        0,
                        0,
                        0,
                        0,
                        Test,
                       item.SBreakdownTechnicalDetails.BrDoHD_id,
                        item.SBreakdownTechnicalDetails.BrDoH_id,
                        item.SBreakdownTechnicalDetails.FAAid,
                        item.SBreakdownTechnicalDetails.Footid,item.SBreakdownTechnicalDetails.NBrandid,
                        item.SBreakdownTechnicalDetails.NTypeid, item.SBreakdownTechnicalDetails.NSizeid, 
                        item.SBreakdownTechnicalDetails.TBrandid, item.SBreakdownTechnicalDetails.TTypeid,
                        item.SBreakdownTechnicalDetails.TTopSizeid, 0,
                        item.SBreakdownTechnicalDetails.TBottomSizeid
                    );
                }                                             
            }
            para.Add("UDT", WorkStudyDT.AsTableValuedParameter("UDT_WorkStudy"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("IE.sp_Master_CRUD", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<WorkStudyDto>> GetWorkStudyData(WorkStudyDto wsdt)
        {
              DataTable WorkStudyDT = new DataTable();
              IEnumerable<WorkStudyDto> WorkStudyList;
  
            DynamicParameters para = new DynamicParameters();

            WorkStudyDT.Columns.Add("F01", typeof(string));
            WorkStudyDT.Columns.Add("F02", typeof(string));
            WorkStudyDT.Columns.Add("F03", typeof(string));
            WorkStudyDT.Columns.Add("F04", typeof(string));
            WorkStudyDT.Columns.Add("F05", typeof(string));
            WorkStudyDT.Columns.Add("F06", typeof(decimal));
            WorkStudyDT.Columns.Add("F07", typeof(decimal));
            WorkStudyDT.Columns.Add("F08", typeof(decimal));     
            WorkStudyDT.Columns.Add("F09", typeof(int));
            WorkStudyDT.Columns.Add("F10", typeof(int));
            WorkStudyDT.Columns.Add("F11", typeof(int)); 
            WorkStudyDT.Columns.Add("F12", typeof(Int64));
            WorkStudyDT.Columns.Add("F13", typeof(Int64));
            WorkStudyDT.Columns.Add("F14", typeof(Int64));    
            WorkStudyDT.Columns.Add("F15", typeof(string));
            WorkStudyDT.Columns.Add("F16", typeof(string));
            WorkStudyDT.Columns.Add("F17", typeof(string));
            WorkStudyDT.Columns.Add("F18", typeof(int));     
            WorkStudyDT.Columns.Add("F19", typeof(Int64));   
            WorkStudyDT.Columns.Add("F20", typeof(int));      
            WorkStudyDT.Columns.Add("F21", typeof(decimal));   
            WorkStudyDT.Columns.Add("F22", typeof(decimal));                                                                         

              WorkStudyDT.Rows.Add(
                        0,
                        0,
                        0, 
                        wsdt.F04,
                        0, 0, 0,0,
                        wsdt.F09,
                        wsdt.F10,
                        0, 0, wsdt.F13, wsdt.F14, 0, 0, wsdt.F17, 0, 0,wsdt.F20
              );
            
                    para.Add("UDT", WorkStudyDT.AsTableValuedParameter("UDT_WorkStudy"));

            WorkStudyList = await DbConnection.QueryAsync<WorkStudyDto>("IE.sp_Master_CRUD", para
                , commandType: CommandType.StoredProcedure);

            return WorkStudyList;
        }
   #endregion "WorkStudy"

      #region "CPD"
        public async Task<IEnumerable<CPDDto>> GetCPDData(CPDDto wsdt)
        {
          DataTable CPDDT = new DataTable();
          IEnumerable<CPDDto> WorkStudyList;

          DynamicParameters para = new DynamicParameters();

          CPDDT.Columns.Add("F01", typeof(string));
          CPDDT.Columns.Add("F02", typeof(string));
          CPDDT.Columns.Add("F03", typeof(string));
          CPDDT.Columns.Add("F04", typeof(string));
          CPDDT.Columns.Add("F05", typeof(string));
          CPDDT.Columns.Add("F06", typeof(decimal));
          CPDDT.Columns.Add("F07", typeof(decimal));
          CPDDT.Columns.Add("F08", typeof(decimal));     
          CPDDT.Columns.Add("F09", typeof(int));
          CPDDT.Columns.Add("F10", typeof(int));
          CPDDT.Columns.Add("F11", typeof(int)); 
          CPDDT.Columns.Add("F12", typeof(Int64));
          CPDDT.Columns.Add("F13", typeof(Int64));
          CPDDT.Columns.Add("F14", typeof(Int64));    
          CPDDT.Columns.Add("F15", typeof(string));
          CPDDT.Columns.Add("F16", typeof(string));
          CPDDT.Columns.Add("F17", typeof(string));
          CPDDT.Columns.Add("F18", typeof(int));     
          CPDDT.Columns.Add("F19", typeof(Int64));   
          CPDDT.Columns.Add("F20", typeof(int));  
          CPDDT.Columns.Add("F21", typeof(DateTime));   
          CPDDT.Columns.Add("F22", typeof(decimal));                                                                            

          CPDDT.Rows.Add(
                        0,
                        0,
                        0, 
                        0,
                        0, 
                        0, 
                        0,
                        0,
                        wsdt.F09,
                        0,
                        0, 
                        wsdt.F12, 
                        wsdt.F13, 
                        wsdt.F14,  
                        0,
                        0,
                        0,
                        0,
                        0,
                        wsdt.F20
                        );
            
            para.Add("UDT", CPDDT.AsTableValuedParameter("UDT_SectionwiseProduction"));

            WorkStudyList = await DbConnection.QueryAsync<CPDDto>("sp_SectionProduction", para
                , commandType: CommandType.StoredProcedure);

            return WorkStudyList;
        }

        public async Task<ReturnDto> SaveCPDData(List<SaveWorkStudyDto> wsDt)
        {
          DataTable CPDDT = new DataTable();

          DynamicParameters para = new DynamicParameters();

          CPDDT.Columns.Add("F01", typeof(string));
          CPDDT.Columns.Add("F02", typeof(string));
          CPDDT.Columns.Add("F03", typeof(string));
          CPDDT.Columns.Add("F04", typeof(string));
          CPDDT.Columns.Add("F05", typeof(string));
          CPDDT.Columns.Add("F06", typeof(decimal));
          CPDDT.Columns.Add("F07", typeof(decimal));
          CPDDT.Columns.Add("F08", typeof(decimal));     
          CPDDT.Columns.Add("F09", typeof(int));
          CPDDT.Columns.Add("F10", typeof(int));
          CPDDT.Columns.Add("F11", typeof(int)); 
          CPDDT.Columns.Add("F12", typeof(Int64));
          CPDDT.Columns.Add("F13", typeof(Int64));
          CPDDT.Columns.Add("F14", typeof(Int64));    
          CPDDT.Columns.Add("F15", typeof(string));
          CPDDT.Columns.Add("F16", typeof(string));
          CPDDT.Columns.Add("F17", typeof(string));
          CPDDT.Columns.Add("F18", typeof(int));     
          CPDDT.Columns.Add("F19", typeof(Int64));   
          CPDDT.Columns.Add("F20", typeof(int));       
          CPDDT.Columns.Add("F21", typeof(DateTime));   
          CPDDT.Columns.Add("F22", typeof(decimal));   
          
            foreach (var item in wsDt)
            {
              var Test =item.Action;
              var FactoryId = item.FactoryId;
              var AgentId = item. AgentId;
           
              
              if (item.Swp != null)
                {                  
                      CPDDT.Rows.Add(
                        0,
                        0,
                        0,
                        item.Swp.ProductionDate,
                        0,
                        0,
                        0,
                        0,
                        Test,
                        item.Swp.BundleIdx,
                        item.Swp.ftywiseTimeBeltId,
                        0,
                        item.Swp.SectionHierarchyId,
                        0, 
                        0,
                        0,
                        0,
                        0,
                        0,
                        0, item.Swp.ProductionDate
                        );
                }
            }

          
          para.Add("UDT", CPDDT.AsTableValuedParameter("UDT_SectionwiseProduction"));

          var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("sp_SectionProduction", para
                        , commandType: CommandType.StoredProcedure);

          return result;
        }

      #endregion "CPD"

      #region "Transport"
          public async Task<IEnumerable<TransportDto>> GetTransportData(TransportDto wsdt)
            {
              DataTable TransportDT = new DataTable();
              IEnumerable<TransportDto> TransportList;
              DynamicParameters para = new DynamicParameters();

                TransportDT.Columns.Add("F01", typeof(string));
                TransportDT.Columns.Add("F02", typeof(string));
                TransportDT.Columns.Add("F03", typeof(string));
                TransportDT.Columns.Add("F04", typeof(string));
                TransportDT.Columns.Add("F05", typeof(string));
                TransportDT.Columns.Add("F06", typeof(decimal));
                TransportDT.Columns.Add("F07", typeof(decimal));
                TransportDT.Columns.Add("F08", typeof(decimal));     
                TransportDT.Columns.Add("F09", typeof(int));
                TransportDT.Columns.Add("F10", typeof(int));
                TransportDT.Columns.Add("F11", typeof(int)); 
                TransportDT.Columns.Add("F12", typeof(Int64));
                TransportDT.Columns.Add("F13", typeof(Int64));
                TransportDT.Columns.Add("F14", typeof(Int64));    
                TransportDT.Columns.Add("F15", typeof(string));
                TransportDT.Columns.Add("F16", typeof(string));
                TransportDT.Columns.Add("F17", typeof(string));
                TransportDT.Columns.Add("F18", typeof(int));     
                TransportDT.Columns.Add("F19", typeof(Int64));   
                TransportDT.Columns.Add("F20", typeof(int));
                TransportDT.Columns.Add("F21", typeof(DateTime)); 
                TransportDT.Columns.Add("F22", typeof(DateTime)); 
                TransportDT.Columns.Add("F23", typeof(DateTime)); 
                TransportDT.Columns.Add("F24", typeof(DateTime));                                                                              

                  TransportDT.Rows.Add(
                            wsdt.F01,
                            0,
                            0, 
                            0,
                            0, 
                            0, 
                            0,
                            0,
                            wsdt.F09,
                            wsdt.F10,
                            0, 
                            wsdt.F12, 
                            wsdt.F13, 
                            wsdt.F14,  
                            0,
                            0,
                            0,
                            0,
                            0,
                            wsdt.F20
                  );
                
                  para.Add("UDT", TransportDT.AsTableValuedParameter("UDT_Transpot"));

                  TransportList = await DbConnection.QueryAsync<TransportDto>("tms.sp_Master_CRUD", para
                  , commandType: CommandType.StoredProcedure);

                  return TransportList;
            }

          public async Task<ReturnDto> SaveTransportData(List<SaveTransportDto> wsDt)
            {
              DataTable TMDT = new DataTable();
              DynamicParameters para = new DynamicParameters();

                TMDT.Columns.Add("F01", typeof(string));
                TMDT.Columns.Add("F02", typeof(string));
                TMDT.Columns.Add("F03", typeof(string));
                TMDT.Columns.Add("F04", typeof(string));
                TMDT.Columns.Add("F05", typeof(string));
                TMDT.Columns.Add("F06", typeof(decimal));
                TMDT.Columns.Add("F07", typeof(decimal));
                TMDT.Columns.Add("F08", typeof(decimal));     
                TMDT.Columns.Add("F09", typeof(int));
                TMDT.Columns.Add("F10", typeof(int));
                TMDT.Columns.Add("F11", typeof(int)); 
                TMDT.Columns.Add("F12", typeof(Int64));
                TMDT.Columns.Add("F13", typeof(Int64));
                TMDT.Columns.Add("F14", typeof(Int64));    
                TMDT.Columns.Add("F15", typeof(string));
                TMDT.Columns.Add("F16", typeof(string));
                TMDT.Columns.Add("F17", typeof(string));
                TMDT.Columns.Add("F18", typeof(int));     
                TMDT.Columns.Add("F19", typeof(Int64));   
                TMDT.Columns.Add("F20", typeof(int));
                TMDT.Columns.Add("F21", typeof(DateTime)); 
                TMDT.Columns.Add("F22", typeof(DateTime)); 
                TMDT.Columns.Add("F23", typeof(string)); 
                TMDT.Columns.Add("F24", typeof(string));        

              
                foreach (var item in wsDt)
                {
                  var Test =item.Action;
                  var FactoryId = item.FactoryId;
                  var AgentId = item. AgentId;
                  
                  if (item.sVehicletype != null)
                    {                  
                          TMDT.Rows.Add(
                            0,
                            0,
                            0,
                            item.sVehicletype.Details,
                            item.sVehicletype.bActive,
                            0,
                            0,
                            0,
                            Test,
                            item.sVehicletype.idVT,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0
                        );
                    }

                  if (item.sVehiclecategory != null)
                    {                  
                          TMDT.Rows.Add(
                            0,
                            0,
                            0,
                            item.sVehiclecategory.Details,
                            item.sVehiclecategory.bActive,
                            0,
                            0,
                            0,
                            Test,
                            item.sVehiclecategory.idVCat,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0
                        );
                    }

                  if (item.sBookingtype != null)
                    {                  
                          TMDT.Rows.Add(
                            0,
                            0,
                            0,
                            item.sBookingtype.Details,
                            item.sBookingtype.bActive,
                            0,
                            0,
                            0,
                            Test,
                            item.sBookingtype.idBType,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0
                        );
                    }

                  if (item.sTransporttype != null)
                    {                  
                          TMDT.Rows.Add(
                            0,
                            0,
                            0,
                            item.sTransporttype.Details,
                            item.sTransporttype.bActive,
                            0,
                            0,
                            0,
                            Test,
                            item.sTransporttype.idTType,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0
                        );
                    }

                  if (item.sPaymenttype != null)
                  {                  
                        TMDT.Rows.Add(
                          0,
                          0,
                          0,
                          item.sPaymenttype.Details,
                          item.sPaymenttype.bActive,
                          0,
                          0,
                          0,
                          Test,
                          item.sPaymenttype.idPayType,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0
                      );
                  }

                  if (item.sRoute != null)
                  {                  
                        TMDT.Rows.Add(
                          item.sRoute.cShortCode,
                          0,
                          0,
                          item.sRoute.Details,
                          item.sRoute.bActive,
                          0,
                          0,
                          0,
                          Test,
                          item.sRoute.idRoute,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0
                      );
                  }

                  if (item.sRoutefactoryallocation  != null)
                  {                  
                        TMDT.Rows.Add(
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          Test,
                          item.sRoutefactoryallocation.idRoute,
                          0,
                          item.sRoutefactoryallocation.AutoId,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0
                      );
                  } 

                  if (item.sVehicleOthers  != null)
                  {                  
                      TMDT.Rows.Add(
                          0,
                          0,
                          0,
                          item.sVehicleOthers.OtherDetails,
                          item.sVehicleOthers.bActive,
                          0,
                          0,
                          0,
                          Test,
                          item.sVehicleOthers.idOther,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0
                      );
                  } 

                  if (item.sTranspoter  != null)
                  {                  
                      TMDT.Rows.Add(
                          item.sTranspoter.SupplierName,
                            item.sTranspoter.OwnerName,
                          0,
                          item.sTranspoter.ThirdPartyName,
                          0,
                          0,
                          0,
                          0,
                          Test,
                          item.sTranspoter.idTrans,
                          item.sTranspoter.BankCode,
                          item.sTranspoter.BranchCode,
                          0,
                          0,
                          item.sTranspoter.BankName, 
                          item.sTranspoter.BranchName, 
                          item.sTranspoter.BankACNo, 
                          0,
                          0,
                          0
                      );
                  } 

                  if (item.sTranspoterFactoryAllocation  != null)
                  {                  
                        TMDT.Rows.Add(
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          Test,
                          item.sTranspoterFactoryAllocation.idTrans,
                          0,
                          item.sTranspoterFactoryAllocation.AutoId,
                          0,
                          0,
                          0, 
                          0, 
                          0, 
                          0,
                          0,
                          0
                      );
                  } 

                  if (item.sVehicleRegister  != null)
                  {                  
                        TMDT.Rows.Add(
                          item.sVehicleRegister.VehicleNo,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          Test,
                          item.sVehicleRegister.idVR,
                          item.sVehicleRegister.TransporterId,
                          item.sVehicleRegister.idVT,
                          0,
                          FactoryId,
                          0, 
                          0, 
                          0, 
                          item.sVehicleRegister.idVCat,
                          0,
                          0,
                          item.sVehicleRegister.RevenueLicense_ExpDate,
                          item.sVehicleRegister.Insurance_ExpDate
                      );
                  }

                  if (item.sRateMatrix  != null)
                  {                  
                      TMDT.Rows.Add(
                          0,
                          0,
                          0,
                          0,
                          0,
                          item.sRateMatrix.distance,
                          item.sRateMatrix.Amount,
                          0,
                          Test,
                          item.sRateMatrix.idRMat,
                          item.sRateMatrix.idVT,
                          item.sRateMatrix.idVCat,
                          item.sRateMatrix.idRouteFrom,
                          item.sRateMatrix.idRouteTo,
                          0, 
                          0, 
                          0, 
                          0, 
                          item.sRateMatrix.RouteType,
                          AgentId
                      );
                  }  

                  if (item.sApprovalMatrix  != null)
                  {                  
                      TMDT.Rows.Add(
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          0,
                          Test,
                          item.sApprovalMatrix.idAMat,
                          item.sApprovalMatrix.idBType,
                          item.sApprovalMatrix.idTType,
                          item.sApprovalMatrix.RequetedLocationid,
                          0,
                          0, 
                          0, 
                          0, 
                          0, 
                          0,
                          item.sApprovalMatrix.ApprovedAgentsId
                      );
                  }
                    
                  if (item.sBookingRequest  != null)
                  {                  
                        TMDT.Rows.Add(
                          item.sBookingRequest.RefNo,
                          item.sBookingRequest.Requesteduser,
                          0,
                          item.sBookingRequest.Remarks,
                          'H',
                          item.sBookingRequest.transdays,
                          0,
                          0,
                          Test,
                          item.sBookingRequest.idBR,
                          item.sBookingRequest.idBType,
                          item.sBookingRequest.idTType,
                          item.sBookingRequest.idVCat,
                          0,
                          0, 
                          0, 
                          0, 
                          0, 
                          AgentId,
                          FactoryId,
                          item.sBookingRequest.RequestedDate
                      );
                  }

                  if (item.sBookingRoute  != null)
                  {                  
                        TMDT.Rows.Add(
                          0,
                          0,
                          0,
                          0,
                          'D',
                          0,
                          0,
                          0,
                          Test,
                          item.sBookingRoute.idBR,
                          item.sBookingRoute.idRouteFrom,
                          item.sBookingRoute.idRouteTo,
                          0,
                          0,
                          item.sBookingRoute.StartDateTime, 
                          item.sBookingRoute.EndDateTime, 
                          0, 
                          0,
                          0,
                          0
                      );
                  }                                                                              
                }

                para.Add("UDT", TMDT.AsTableValuedParameter("UDT_Transpot"));

                var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("tms.sp_Master_CRUD", para
                , commandType: CommandType.StoredProcedure);

                return result;
            }
      
      #endregion "Transport"

    #region "PaymentInvoice"

        public async Task<IEnumerable<PaymentInvoiceDto>> GetPaymentInvoiceData(PaymentInvoiceDto pidt)
        {
            DataTable PaymentInvoiceDT = new DataTable();
            IEnumerable<PaymentInvoiceDto> PaymentInvoiceList;
            
            DynamicParameters para = new DynamicParameters();

            PaymentInvoiceDT.Columns.Add("F01", typeof(int));
            PaymentInvoiceDT.Columns.Add("F02", typeof(int));
            PaymentInvoiceDT.Columns.Add("F03", typeof(int));
            PaymentInvoiceDT.Columns.Add("F04", typeof(int));
            PaymentInvoiceDT.Columns.Add("F05", typeof(int));
            PaymentInvoiceDT.Columns.Add("F06", typeof(int));
            PaymentInvoiceDT.Columns.Add("F07", typeof(int));
            PaymentInvoiceDT.Columns.Add("F08", typeof(int));
            PaymentInvoiceDT.Columns.Add("F09", typeof(int));
            PaymentInvoiceDT.Columns.Add("F10", typeof(int));
            PaymentInvoiceDT.Columns.Add("F11", typeof(int)); 
            PaymentInvoiceDT.Columns.Add("F12", typeof(int));
            PaymentInvoiceDT.Columns.Add("F13", typeof(int));
            PaymentInvoiceDT.Columns.Add("F14", typeof(string));    
            PaymentInvoiceDT.Columns.Add("F15", typeof(string));
            PaymentInvoiceDT.Columns.Add("F16", typeof(string));
            PaymentInvoiceDT.Columns.Add("F17", typeof(string));
            PaymentInvoiceDT.Columns.Add("F18", typeof(string));     
            PaymentInvoiceDT.Columns.Add("F19", typeof(string));   
            PaymentInvoiceDT.Columns.Add("F20", typeof(string));
            PaymentInvoiceDT.Columns.Add("F21", typeof(string)); 
            PaymentInvoiceDT.Columns.Add("F22", typeof(decimal)); 
            PaymentInvoiceDT.Columns.Add("F23", typeof(decimal)); 
            PaymentInvoiceDT.Columns.Add("F24", typeof(decimal));                                                                              
            PaymentInvoiceDT.Columns.Add("F25", typeof(decimal));
            PaymentInvoiceDT.Columns.Add("F26", typeof(decimal));     
            PaymentInvoiceDT.Columns.Add("F27", typeof(decimal));   
            PaymentInvoiceDT.Columns.Add("F28", typeof(decimal));
            PaymentInvoiceDT.Columns.Add("F29", typeof(DateTime)); 
            PaymentInvoiceDT.Columns.Add("F30", typeof(DateTime)); 
            PaymentInvoiceDT.Columns.Add("F31", typeof(DateTime)); 
            PaymentInvoiceDT.Columns.Add("F32", typeof(bool)); 

            PaymentInvoiceDT.Rows.Add(
                        pidt.F01,
                        pidt.F02,
                        3,4,5,6,7,8,
                        pidt.F09,
                        pidt.F10,
                        11,12, 
                        pidt.F13, 
                        pidt.F14,  
                        15,16,17, 
                        pidt.F18,
                        19,20,21,22,23,24,25,26,27,28
                        //,1900-00-00,1900-00-00,1900-00-00,1
              );
            
              para.Add("UDT", PaymentInvoiceDT.AsTableValuedParameter("UDT_PaymentInvoice"));

              PaymentInvoiceList = await DbConnection.QueryAsync<PaymentInvoiceDto>("sp_PaymentInvoice", para
                                    , commandType: CommandType.StoredProcedure);

            return PaymentInvoiceList;
        }

        public async Task<ReturnDto> SavePaymentInvoiceData(List<SavePaymentInvoiceDto> piDt)
        {
            DataTable PaymentInvoiceDT = new DataTable();

            DynamicParameters para = new DynamicParameters();

            PaymentInvoiceDT.Columns.Add("F01", typeof(int));
            PaymentInvoiceDT.Columns.Add("F02", typeof(int));
            PaymentInvoiceDT.Columns.Add("F03", typeof(int));
            PaymentInvoiceDT.Columns.Add("F04", typeof(int));
            PaymentInvoiceDT.Columns.Add("F05", typeof(int));
            PaymentInvoiceDT.Columns.Add("F06", typeof(int));
            PaymentInvoiceDT.Columns.Add("F07", typeof(int));
            PaymentInvoiceDT.Columns.Add("F08", typeof(int));     
            PaymentInvoiceDT.Columns.Add("F09", typeof(int));
            PaymentInvoiceDT.Columns.Add("F10", typeof(int));
            PaymentInvoiceDT.Columns.Add("F11", typeof(int)); 
            PaymentInvoiceDT.Columns.Add("F12", typeof(int));
            PaymentInvoiceDT.Columns.Add("F13", typeof(int));
            PaymentInvoiceDT.Columns.Add("F14", typeof(string));    
            PaymentInvoiceDT.Columns.Add("F15", typeof(string));
            PaymentInvoiceDT.Columns.Add("F16", typeof(string));
            PaymentInvoiceDT.Columns.Add("F17", typeof(string));
            PaymentInvoiceDT.Columns.Add("F18", typeof(string));     
            PaymentInvoiceDT.Columns.Add("F19", typeof(string));   
            PaymentInvoiceDT.Columns.Add("F20", typeof(string));
            PaymentInvoiceDT.Columns.Add("F21", typeof(string)); 
            PaymentInvoiceDT.Columns.Add("F22", typeof(decimal)); 
            PaymentInvoiceDT.Columns.Add("F23", typeof(decimal)); 
            PaymentInvoiceDT.Columns.Add("F24", typeof(decimal));                                                                               
            PaymentInvoiceDT.Columns.Add("F25", typeof(decimal));
            PaymentInvoiceDT.Columns.Add("F26", typeof(decimal));     
            PaymentInvoiceDT.Columns.Add("F27", typeof(decimal));   
            PaymentInvoiceDT.Columns.Add("F28", typeof(decimal));
            PaymentInvoiceDT.Columns.Add("F29", typeof(DateTime)); 
            PaymentInvoiceDT.Columns.Add("F30", typeof(DateTime)); 
            PaymentInvoiceDT.Columns.Add("F31", typeof(DateTime)); 
            PaymentInvoiceDT.Columns.Add("F32", typeof(bool));  

            foreach (var item in piDt)
            { var ActiviId =item.Action;
              var FactoryId = item.FactoryId;
              var AgentId = item. AgentId;
              var ModuleId = item.ModuleId;
              var  nowDate=   item.nowDate;
               
              if (item.sPaymentInvoiceHeader != null)
                {                  
                  PaymentInvoiceDT.Rows.Add(
                  item.sPaymentInvoiceHeader.InvoiceTypeIdx, //1
                  item.sPaymentInvoiceHeader.SupplierIdx, //2
                  item.sPaymentInvoiceHeader.CurrencyId,  //3
                  item.sPaymentInvoiceHeader.PaymentTermsId, //4
                  item.sPaymentInvoiceHeader.PurchaseTypeId,  //5
                  item.sPaymentInvoiceHeader.TaxTypeId,  //6
                  7,8,
                  ActiviId, //9
                  item.sPaymentInvoiceHeader.PIHIdx, //10
                  AgentId,  //11
                  ModuleId, //12 
                  FactoryId,  //13
                  14, 
                  item.sPaymentInvoiceHeader.ReferenceNo,  //15
                  item.sPaymentInvoiceHeader.Remarks,  //16
                  'H', //17
                  18,19,20,21,                         
                  item.sPaymentInvoiceHeader.ExchangeRate,//22
                  item.sPaymentInvoiceHeader.GRNamount,//23
                  item.sPaymentInvoiceHeader.AdditionalChargeamount,//24
                  item.sPaymentInvoiceHeader.Invoiceamount,//25
                  26,27,28,
                  item.sPaymentInvoiceHeader.InvoiceDate,//29
                  item.sPaymentInvoiceHeader.GLDate,//30
                  item.sPaymentInvoiceHeader.DueDate, //31
                  1 //32 
                  );
                }

              if (item.sPaymentInvoiceDetails != null)
                {                  
                  PaymentInvoiceDT.Rows.Add(
                  item.sPaymentInvoiceDetails.PIDIdx, //1
                  item.sPaymentInvoiceDetails.GRNDIdx, //2
                  item.sPaymentInvoiceDetails.ArticleColrSizeIdx,  //3
                  item.sPaymentInvoiceDetails.UOMIdx, //4
                  5,6,7,8, //8
                  ActiviId, //9
                  item.sPaymentInvoiceDetails.PIHIdx, //10
                  AgentId,  //11
                  ModuleId, //12 
                  FactoryId,  //13
                  14,15,16,
                  'D', //17
                  18,19,20,21,                         
                  item.sPaymentInvoiceDetails.GRNQty,//22
                  item.sPaymentInvoiceDetails.InvoiceQty,//23
                  item.sPaymentInvoiceDetails.InvoiceUnitAmount,//24
                  item.sPaymentInvoiceDetails.InvoiceAmount,//25
                  item.sPaymentInvoiceDetails.FOCQty, //26
                  27,
                  item.sPaymentInvoiceDetails.ComInvoiceamount,//28
                  nowDate,//29
                  nowDate,//30
                  nowDate, //31
                  0 //32
                  );
                }

              if (item.sPaymentInvoiceAdditionalDetails != null)
                {                  
                  PaymentInvoiceDT.Rows.Add(
                  item.sPaymentInvoiceAdditionalDetails.ADIdx, //1
                  2,
                  item.sPaymentInvoiceAdditionalDetails.BasisIdx,  //3
                  4,5,6,7,
                  item.sPaymentInvoiceAdditionalDetails.PIADIdx, //8
                  ActiviId, //9
                  item.sPaymentInvoiceAdditionalDetails.PIHIdx, //10
                  AgentId,  //11
                  ModuleId, //12 
                  FactoryId,  //13
                  14,15,16,
                  'A', //17
                  18,19,20,21,22,
                  item.sPaymentInvoiceAdditionalDetails.AddValue,//23
                  item.sPaymentInvoiceAdditionalDetails.RunningTotal,//24
                  25,26,27,28,
                  nowDate,//29
                  nowDate,//30
                  nowDate, //31
                  0 //32
                  );
                }                                                                             
            }
         
                para.Add("UDT", PaymentInvoiceDT.AsTableValuedParameter("UDT_PaymentInvoice"));

                var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("sp_PaymentInvoice", para
                            , commandType: CommandType.StoredProcedure);

            return result;
        }
    #endregion "PaymentInvoice"  


      #region "FastReact"
        public async Task<IEnumerable<FROrderDto>> getFROrdersAsync(DateTime myDate) 
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("TrnDate", myDate);

            var result = await DbConnection.QueryAsync<FROrderDto>("spFRImportGetMismatchOrder", para
                , commandType: CommandType.StoredProcedure);           

            return result;
        }

        public async Task<IEnumerable<FRwfxRtnDto>> GetFRWFXDetailsAsync(FRwfxDto frDt) 
        {
            IEnumerable<FRwfxRtnDto> FRwfxlst;
            DynamicParameters para = new DynamicParameters();

            DataTable SPODt = new DataTable();
            SPODt.Columns.Add("SPO", typeof(string));

              if (frDt.Spolist != null){
                foreach (var item in frDt.Spolist)
                {
                  SPODt.Rows.Add(item.spo);
                }
              }

              para.Add("Action", frDt.Action);
              para.Add("Season", frDt.Season);
              para.Add("Style", frDt.Style);
              para.Add("SPO", SPODt.AsTableValuedParameter("SPOListType"));

             FRwfxlst = await DbConnection.QueryAsync<FRwfxRtnDto>("spFRImportGetWFXDetails", para
                , commandType: CommandType.StoredProcedure);           

            return FRwfxlst;
        }

        public async Task<ReturnDto> saveFRAssocationOrderAsync(SaveFROrderDto frPODto)
        {
            DynamicParameters para = new DynamicParameters();
            DataTable FRAssocationDT = new DataTable();

            para.Add("OrderNo", frPODto.OrderNo);
            para.Add("ProformaInvoiceNum2", frPODto.ProformaInvoiceNum2);
            para.Add("ColorCodes", frPODto.ColorCodes);
            para.Add("BuyerPONumID", frPODto.BuyerPONumID);
            para.Add("FRStyle", frPODto.FRStyle);
            para.Add("Season", frPODto.Season);
            para.Add("MStyle", frPODto.MStyle);
            para.Add("ExtraOrderQty", frPODto.ExtraOrderQty);

            FRAssocationDT.Columns.Add("OrderCode", typeof(string));
            FRAssocationDT.Columns.Add("ProformaInvoiceNum2", typeof(string));
            FRAssocationDT.Columns.Add("BuyerPONumID", typeof(string));
            FRAssocationDT.Columns.Add("ColorCodes", typeof(string));
            FRAssocationDT.Columns.Add("FROrderQty", typeof(int));
            FRAssocationDT.Columns.Add("TotProdQty", typeof(int));
            FRAssocationDT.Columns.Add("BalQty", typeof(int));
            FRAssocationDT.Columns.Add("Sequence", typeof(int));
            FRAssocationDT.Columns.Add("POAssDetailIdx", typeof(long));
            FRAssocationDT.Columns.Add("SPO", typeof(string));

            foreach (var item in frPODto.FROrderDetails)
            {
                FRAssocationDT.Rows.Add(item.Ordercode
                    , item.ProformaInvoiceNum2
                    , item.BuyerPONumID
                    , item.ColorCodes
                    , item.FROrderQty
                    , item.TotProdQty
                    , item.BalQty
                    , item.Sequence
                    , item.POAssDetailIdx
                    , item.Spo);
            }

            para.Add("FRAssociationDT", FRAssocationDT.AsTableValuedParameter("FROrderAssocationDT"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spFRImportOrderAssociationSave", para
            , commandType: CommandType.StoredProcedure);

            return result;

        }

        public async Task<IEnumerable<FRwfxRtnDto>> getFRNewOrdersAsync(string myOrder) 
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("OrderNo", myOrder);

            var result = await DbConnection.QueryAsync<FRwfxRtnDto>("spFRIImportCheckOrder", para
                , commandType: CommandType.StoredProcedure);           

            return result;
        }

      #endregion "FastReact"

      #region "Dashboard"

        public async Task<IEnumerable<dashBoardTwoDto>> GetDashBoardTwoDataListAsync(dashBoardTwoSearchDto dashboardDt)
        {
            
          DynamicParameters para = new DynamicParameters();
          IEnumerable<dashBoardTwoDto> dashboardList;

          para.Add("xAction" , dashboardDt.action);
          para.Add("xFromDate" , dashboardDt.fDate);
          para.Add("ToDate" , dashboardDt.tDate);
          para.Add("xlocationId" , dashboardDt.locationId);

          dashboardList = await DbConnection.QueryAsync<dashBoardTwoDto>("spDashboard_TM02", para
                      , commandType: CommandType.StoredProcedure);

          return dashboardList;
        }

      #endregion "Dashboard"

      #region EmployeeDt
      public async Task<IEnumerable<MstrEmployee>> GetEmployeDtAsync(string location)
        {
          DynamicParameters para = new DynamicParameters();

          para.Add("Location",location);
           
          return await DbConnection.QueryAsync<MstrEmployee>("spTransGetAllEmployeeDt" , para
          , commandType: CommandType.StoredProcedure);            
        }
      #endregion EmployeeDt

      #region EmpOperationDt
      public async Task<IEnumerable<EmployeeRemainingOpsDto>> GetEmployeeRemainingAsync(int employeeId)
        {
          DynamicParameters para = new DynamicParameters();

          para.Add("EmployeeId" , employeeId);
           
          return await DbConnection.QueryAsync<EmployeeRemainingOpsDto>("spTransGetOperationDt" , para
          , commandType: CommandType.StoredProcedure);            
        }
      #endregion EmpOperationDt

      #region Operation Assigned To Employee Get
      public async Task<IEnumerable<EmpOperationDtDto>> GetEmployeeOpDtAsync(int employeeId)
        {
          DynamicParameters para = new DynamicParameters();

          para.Add("EmployeeId" , employeeId);
           
          return await DbConnection.QueryAsync<EmpOperationDtDto>("spTransGetEmpWiseOpeDt" , para
          , commandType: CommandType.StoredProcedure);            
        }
      #endregion Operation Assigned To Employee Get

      #region Operation Assigned To Employee Save
      public async Task<int> SaveEmployeeOperationAsync(TransOperationsAndSectionsAssignToEmp operation)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , operation.AutoId);
            para.Add("FactoryId" , operation.FactoryId);
            para.Add("OperationId" , operation.OperationId);
            para.Add("EmployeeId" , operation.EmployeeId);
            para.Add("SMV" , operation.SMV);
            para.Add("Target" , operation.Target);
            para.Add("UserId", operation.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spTransSaveEmployeeOperationDt", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
      #endregion Operation Assigned To Employee Save

      #region Operation Assigned To Employee Deactive
      public async Task<int> DeactiveOperationEmpAsync(TransOperationsAndSectionsAssignToEmp operation)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , operation.AutoId);
            para.Add("bActive" , operation.IsActive);
            para.Add("UserId", operation.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spTransOperationEmpDeactivate", para
                , commandType: CommandType.StoredProcedure);            
            return para.Get<int>("Result");
        }
      #endregion Operation Assigned To Employee Deactive
      #region Get Factory Wise Departments
      public async Task<IEnumerable<FtyDepartmentDto>> GetFactoryWiseDepartmentsAsync(int locationId)
        {
          DynamicParameters para = new DynamicParameters();

          para.Add("FactoryId" , locationId);
           
          return await DbConnection.QueryAsync<FtyDepartmentDto>("spTransGetFactoryWiseDepartments" , para
          , commandType: CommandType.StoredProcedure);            
        }
      #endregion Get Factory Wise Departments
      #region Get Factory , Department Wise Section
      public async Task<IEnumerable<SectionDetailsDto>> GetDepartmentWiseSectionAsync(SectionDetailsDto section)
        {   
            IEnumerable<SectionDetailsDto> sectionList;
            DynamicParameters para = new DynamicParameters();

            para.Add("FactoryId" , section.FactoryId);
            para.Add("DepartmentId" , section.DepartmentId);
            
            sectionList = await DbConnection.QueryAsync<SectionDetailsDto>("spTransGetFtyDepartmentSections" , para
                    , commandType: CommandType.StoredProcedure);
            
            return sectionList;
        } 
      #endregion Get Factory , Department Wise Section
      #region Get Factory , Department ,Section Wise Sub-Section
        public async Task<IEnumerable<SubSectionDto>> GetSectionWiseSubSectionAsync(SubSectionDto subsection)
        {   
            IEnumerable<SubSectionDto> subsectionList;
            DynamicParameters para = new DynamicParameters();

            para.Add("FactoryId" , subsection.FactoryId);
            para.Add("DepartmentId" , subsection.DepartmentId);
            para.Add("SectionId" , subsection.SectionId);
            
            subsectionList = await DbConnection.QueryAsync<SubSectionDto>("spTransGetFtyDepartmentSectionsWiseSubSections" , para
                    , commandType: CommandType.StoredProcedure);
            
            return subsectionList;
        } 
      #endregion Get Factory , Department ,Section Wise Sub-Section

      #region Get Factory , Department ,Section ,Sub-Section Wise Lines
        public async Task<IEnumerable<SectionLineDto>> GetSubSectionWiseLineAsync(SectionLineDto line)
        {   
            IEnumerable<SectionLineDto> lineList;
            DynamicParameters para = new DynamicParameters();

            para.Add("FactoryId" , line.FactoryId);
            para.Add("DepartmentId" , line.DepartmentId);
            para.Add("SectionId" , line.SectionId);
            para.Add("SubSection" , line.SubSectionId);
            
            lineList = await DbConnection.QueryAsync<SectionLineDto>("spTransGetFtyDepartmentSectionsSubSectionsWiseLine" , para
                    , commandType: CommandType.StoredProcedure);
            
            return lineList;
        } 
      #endregion Get Factory , Department ,Section ,Sub-Section Wise Lines
      
      #region Get Lost Time Reason
      public async Task<IEnumerable<LostTimeReasonDto>> GetLostTimeReasonAsync(int moduleId)
        {
          DynamicParameters para = new DynamicParameters();

          para.Add("ModuleId" , moduleId);
           
          return await DbConnection.QueryAsync<LostTimeReasonDto>("spTransGetLostTimeReason" , para
          , commandType: CommandType.StoredProcedure);            
        }
      #endregion Get Lost Time Reason

      #region Get Lost Time
        public async Task<IEnumerable<LostTimeDto>> GetLostTimeAsync(LostTimeSearchDto lost)
        {   
            IEnumerable<LostTimeDto> lostTimeList;
            DynamicParameters para = new DynamicParameters();

            para.Add("FactoryId" , lost.FactoryId);
            para.Add("Date" , lost.Date);
            para.Add("DepartmentId" , lost.DepartmentId);
            para.Add("SectionId" , lost.SectionId);
            para.Add("SectionGroupId" , lost.SectionGroupId);
            para.Add("LineId" , lost.LineId);
            
            lostTimeList = await DbConnection.QueryAsync<LostTimeDto>("spTransGetLostTime" , para
                    , commandType: CommandType.StoredProcedure);
            
            return lostTimeList;
        } 
      #endregion Get Lost Time

      #region Save Lost Time
        public async Task<int> SaveLostTimeAsync(LostTimeSaveDto lostS)
        {   
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , lostS.AutoId);
            para.Add("FactoryId" , lostS.FactoryId);
            para.Add("Date" , lostS.Date);
            para.Add("DepartmentId" , lostS.DepartmentId);
            para.Add("SectionId" , lostS.SectionId);
            para.Add("SectionGroupId" , lostS.SectionGroupId);
            para.Add("LineId" , lostS.LineId);
            para.Add("ReasonId" , lostS.ReasonId);
            para.Add("Minuts" , lostS.Minuts);
            para.Add("UserId", lostS.UserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 
            
            var result = await DbConnection.ExecuteAsync("spTransLostTimeSave", para
                , commandType: CommandType.StoredProcedure);            
            return para.Get<int>("Result");
        } 
      #endregion Save Lost Time
    }
}