using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{

    public class SalesRepository : DbConnCartonRepositoryBase, ISalesRepository
    {

        public SalesRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        //public SalesRepository(IApplicationCartonDbContext context, IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        //{
        //    _context = context;
        //}

        public async Task<TransSalesOrderHd> GetSalesOrderRefAsync()
        {
            TransSalesOrderHd SOHeader;
            SOHeader = await DbConnection.QueryFirstAsync<TransSalesOrderHd>("spTransSalesOrderGetCustRef", null
                , commandType: CommandType.StoredProcedure);

            return SOHeader;
        }

        public async Task<IEnumerable<SalesOrderRetDto>> GetSalesOrderAsync(string SORefNo)
        {
            IEnumerable<SalesOrderRetDto> salOrderList;
            DynamicParameters para = new DynamicParameters();

            para.Add("SORefNo", SORefNo);

            salOrderList = await DbConnection.QueryAsync<SalesOrderRetDto>("spTransSalesOrderGetSODetails", para
                    , commandType: CommandType.StoredProcedure);

            return salOrderList;
        }

        public async Task<IEnumerable<SalesOrderDtListDto>> GetSalesOrderDetailsAsync(string customerPO)
        {
            IEnumerable<SalesOrderDtListDto> salesItemList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerPO", customerPO);

            salesItemList = await DbConnection.QueryAsync<SalesOrderDtListDto>("spTransSalesOrderGetSODtList", para
                    , commandType: CommandType.StoredProcedure);

            return salesItemList;
        }

        public async Task<IEnumerable<SalesItemDto>> GetPendSalesOrderItemAsync(int SOHeaderId)
        {
            IEnumerable<SalesItemDto> salesItemList;
            DynamicParameters para = new DynamicParameters();

            para.Add("SOHeaderId", SOHeaderId);
            // para.Add("Action" , "Approve");

            salesItemList = await DbConnection.QueryAsync<SalesItemDto>("spTransSalesOrderPendItems", para
                    , commandType: CommandType.StoredProcedure);

            return salesItemList;
        }

        public async Task<ReturnDto> SaveSalesOrderAsync(List<SalesOrderDeliveryDto> salesOrder)
        {
            DataTable SOHeader = new DataTable();
            DataTable SOItem = new DataTable();
            DataTable SODelivery = new DataTable();
            DynamicParameters para = new DynamicParameters();

            SOHeader.Columns.Add("autoId", typeof(long));
            SOHeader.Columns.Add("UserId", typeof(int));
            SOHeader.Columns.Add("customerLocId", typeof(int));
            SOHeader.Columns.Add("customerId", typeof(int));
            SOHeader.Columns.Add("customerRef", typeof(string));
            SOHeader.Columns.Add("delDate", typeof(string));
            SOHeader.Columns.Add("orderRef", typeof(string));
            SOHeader.Columns.Add("customerUserId", typeof(int));
            SOHeader.Columns.Add("salesCategoryId", typeof(int));
            SOHeader.Columns.Add("salesAgentId", typeof(int));
            SOHeader.Columns.Add("currencyId", typeof(int));
            SOHeader.Columns.Add("countryId", typeof(int));
            SOHeader.Columns.Add("paymentTermId", typeof(int));
            SOHeader.Columns.Add("customerDiviId", typeof(int));
            SOHeader.Columns.Add("isChargeable", typeof(bool));
            SOHeader.Columns.Add("exchdate", typeof(string));
            // SOHeader.Columns.Add("articleId", typeof(int));

            SOItem.Columns.Add("autoId", typeof(long));
            SOItem.Columns.Add("articleId", typeof(long));
            SOItem.Columns.Add("colorId", typeof(int));
            SOItem.Columns.Add("sizeId", typeof(int));
            SOItem.Columns.Add("qty", typeof(int));
            SOItem.Columns.Add("costingId", typeof(long));
            SOItem.Columns.Add("isIntendCreated", typeof(bool));
            SOItem.Columns.Add("price", typeof(decimal));
            SOItem.Columns.Add("brandCodeId", typeof(int));

            SODelivery.Columns.Add("autoId", typeof(long));
            SODelivery.Columns.Add("deliveryDate", typeof(string));
            SODelivery.Columns.Add("deliveryRef", typeof(string));
            SODelivery.Columns.Add("soItemDtId", typeof(long));
            SODelivery.Columns.Add("qty", typeof(int));
            SODelivery.Columns.Add("customerLocId", typeof(int));
            SODelivery.Columns.Add("articleId", typeof(long));
            SODelivery.Columns.Add("colorId", typeof(int));
            SODelivery.Columns.Add("sizeId", typeof(int));

            foreach (var item in salesOrder)
            {
                if (item.SalesOrderHd != null)
                {
                    SOHeader.Rows.Add(
                        item.SalesOrderHd.AutoId
                        , item.SalesOrderHd.CreateUserId
                        , item.SalesOrderHd.CustomerLocId
                        , item.SalesOrderHd.CustomerId
                        , item.SalesOrderHd.CustomerRef.ToUpper().Trim()
                        , item.SalesOrderHd.DelDate
                        , item.SalesOrderHd.OrderRef.ToUpper().Trim()
                        , item.SalesOrderHd.CustomerUserId
                        , item.SalesOrderHd.SalesCategoryId
                        , item.SalesOrderHd.SalesAgentId
                        , item.SalesOrderHd.CusCurrencyId
                        , item.SalesOrderHd.CountryId
                        , item.SalesOrderHd.PaymentTermId
                        , item.SalesOrderHd.CustomerDivId
                        , item.SalesOrderHd.IsChargeable
                        , item.SalesOrderHd.Exchdate);
                }
                else if (item.SalesItemDt != null)
                {
                    foreach (var dt in item.SalesItemDt)
                    {
                        SOItem.Rows.Add(dt.AutoId
                        , dt.ArticleId
                        , dt.ColorId
                        , dt.SizeId
                        , dt.Qty
                        , dt.CostingId
                        , dt.IsIntendCreated
                        , dt.Price
                        , dt.BrandCodeId);
                    }
                }
                else
                {
                    SODelivery.Rows.Add(item.AutoId
                        , item.DeliveryDate
                        , item.DeliveryRef
                        , item.SOItemDtId
                        , item.Qty
                        , item.CustomerLocId
                        , item.ArticleId
                        , item.ColorId
                        , item.SizeId
                    );
                }
            }

            para.Add("SOHeader", SOHeader.AsTableValuedParameter("SOHeaderType"));
            para.Add("SOItem", SOItem.AsTableValuedParameter("SOItemType"));
            para.Add("SODelivery", SODelivery.AsTableValuedParameter("SODeliveryType"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransSalesOrderSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<PendingOrderItemsDto>> GetPendOrderItemsAsync(int custometId)
        {
            IEnumerable<PendingOrderItemsDto> PendItemList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerId", custometId);

            PendItemList = await DbConnection.QueryAsync<PendingOrderItemsDto>("spTransJobCardGetPendingItems", para
                    , commandType: CommandType.StoredProcedure);

            return PendItemList;
        }

        public async Task<IEnumerable<PendingDelivOrderDto>> GetPendDelivOrderAsync(PendingOrderItemsDto items)
        {
            IEnumerable<PendingDelivOrderDto> PendDelivList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CombinId", items.CombinId);
            para.Add("CustomerId", items.CustomerId);
            para.Add("ArticleId", items.ArticleId);
            para.Add("ColorId", items.ColorId);
            para.Add("SizeId", items.SizeId);

            PendDelivList = await DbConnection.QueryAsync<PendingDelivOrderDto>("spTransJobCardGetPendingOrders", para
                    , commandType: CommandType.StoredProcedure);

            return PendDelivList;
        }

        public async Task<RefNumDto> GetRefNumberAsync(string TransType)
        {
            RefNumDto refNum;
            DynamicParameters para = new DynamicParameters();

            para.Add("TransType", TransType);

            refNum = await DbConnection.QuerySingleAsync<RefNumDto>("spTransRefNumberGet", para
                    , commandType: CommandType.StoredProcedure);
            return refNum;
        }

        public async Task<IEnumerable<ReturnJobCardDto>> GetJobCardDetailsAsync(string jobNo)
        {
            IEnumerable<ReturnJobCardDto> jobCardDto;
            DynamicParameters para = new DynamicParameters();

            para.Add("JobNo", jobNo);

            jobCardDto = await DbConnection.QueryAsync<ReturnJobCardDto>("spTransJobCardGetSavedJobs", para
                    , commandType: CommandType.StoredProcedure);
            return jobCardDto;
        }

        public async Task<IEnumerable<PendingOrderItemsDto>> GetJobCardListAsync(string CustomerRef)
        {
            IEnumerable<PendingOrderItemsDto> jobCardDto;
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerRef", CustomerRef);

            jobCardDto = await DbConnection.QueryAsync<PendingOrderItemsDto>("spTransJobCardGetList", para
                    , commandType: CommandType.StoredProcedure);
            return jobCardDto;
        }

        public async Task<IEnumerable<PendingOrderItemsDto>> GetJobCardListsAsync(string jobcardNo)
        {
            IEnumerable<PendingOrderItemsDto> jobCardDto;
            DynamicParameters para = new DynamicParameters();

            para.Add("JobCardNo", jobcardNo);

            jobCardDto = await DbConnection.QueryAsync<PendingOrderItemsDto>("spTransJobCardGetListByOrderRef", para
                    , commandType: CommandType.StoredProcedure);
            return jobCardDto;
        }

        public async Task<ReturnDto> SaveJobCardAsync(List<TransJobDetail> trnsJob)
        {
            DataTable JobHeader = new DataTable();
            DataTable JobDetails = new DataTable();

            DynamicParameters para = new DynamicParameters();

            JobHeader.Columns.Add("JobNo", typeof(string));
            JobHeader.Columns.Add("CustomerId", typeof(int));
            JobHeader.Columns.Add("ArticleId", typeof(int));
            JobHeader.Columns.Add("ColorId", typeof(int));
            JobHeader.Columns.Add("SizeId", typeof(int));
            JobHeader.Columns.Add("CombinId", typeof(int));
            JobHeader.Columns.Add("DelivLocationId", typeof(int));
            JobHeader.Columns.Add("TotQty", typeof(int));
            JobHeader.Columns.Add("PlanQty", typeof(int));
            JobHeader.Columns.Add("LocationId", typeof(int));
            JobHeader.Columns.Add("UserId", typeof(int));
            JobHeader.Columns.Add("PlanDate", typeof(string));

            JobDetails.Columns.Add("JobHeaderId", typeof(int));
            JobDetails.Columns.Add("SOItemDtId", typeof(int));
            JobDetails.Columns.Add("SODelivDtId", typeof(int));
            JobDetails.Columns.Add("OrderQty", typeof(int));
            JobDetails.Columns.Add("JobQty", typeof(int));

            foreach (var item in trnsJob)
            {
                if (item.JobHeader != null)
                {
                    JobHeader.Rows.Add(
                        item.JobHeader.JobNo.Trim()
                        , item.JobHeader.CustomerId
                        , item.JobHeader.ArticleId
                        , item.JobHeader.ColorId
                        , item.JobHeader.SizeId
                        , item.JobHeader.CombinId
                        , item.JobHeader.DelivLocationId
                        , item.JobHeader.TotQty
                        , item.JobHeader.PlanQty
                        , item.JobHeader.LocationId
                        , item.JobHeader.CreateUserId
                        , item.JobHeader.PlanDate);
                }
                else
                {
                    JobDetails.Rows.Add(item.JobHeaderId
                        , item.SOItemDtId
                        , item.SODelivDtId
                        , item.OrderQty
                        , item.JobQty);
                }
            }

            para.Add("JobHeaderDT", JobHeader.AsTableValuedParameter("JobHeaderType"));
            para.Add("JobDetailDT", JobDetails.AsTableValuedParameter("JobDetailType"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransJobCardSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<TransJobHeader>> GetFPOPendingJobsAsync()
        {
            IEnumerable<TransJobHeader> jobList;
            //DynamicParameters para = new DynamicParameters();

            jobList = await DbConnection.QueryAsync<TransJobHeader>("spTransFtyProdOrderGetPendJobs", null
                    , commandType: CommandType.StoredProcedure);
            return jobList;
        }

        public async Task<IEnumerable<FPONoListDto>> GetFPONoListAsync(string customerRef)
        {
            IEnumerable<FPONoListDto> fpoList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerRef", customerRef);

            fpoList = await DbConnection.QueryAsync<FPONoListDto>("spTransFtyProdOrderGetList", para
                    , commandType: CommandType.StoredProcedure);
            return fpoList;
        }


        public async Task<IEnumerable<PendJobDetailsDto>> GetFPOPendingJobDtAsync(int JobId)
        {
            IEnumerable<PendJobDetailsDto> jobList;
            DynamicParameters para = new DynamicParameters();

            para.Add("JobId", JobId);

            jobList = await DbConnection.QueryAsync<PendJobDetailsDto>("spTransFtyProdOrderGetPendJobDt", para
                    , commandType: CommandType.StoredProcedure);
            return jobList;
        }

        public async Task<ReturnDto> SaveFPOAsync(List<FacProdOrderDto> facProdOrderDtos)
        {
            DataTable FPOHeader = new DataTable();
            DataTable FPODetails = new DataTable();

            DynamicParameters para = new DynamicParameters();

            FPOHeader.Columns.Add("AutoId", typeof(long));
            FPOHeader.Columns.Add("JobHeaderId", typeof(long));
            FPOHeader.Columns.Add("FPONo", typeof(string));
            FPOHeader.Columns.Add("StartDate", typeof(string));
            FPOHeader.Columns.Add("EndDate", typeof(string));
            // FPOHeader.Columns.Add("StatusId", typeof(byte));
            FPOHeader.Columns.Add("Remarks", typeof(string));
            FPOHeader.Columns.Add("Qty", typeof(int));
            FPOHeader.Columns.Add("UserId", typeof(int));

            FPODetails.Columns.Add("SODelivDtId", typeof(long));
            FPODetails.Columns.Add("SOItemDtId", typeof(long));
            FPODetails.Columns.Add("Qty", typeof(int));

            foreach (var item in facProdOrderDtos)
            {
                if (item.FtyProductionOrderHd != null)
                {
                    FPOHeader.Rows.Add(item.FtyProductionOrderHd.AutoId
                        , item.FtyProductionOrderHd.JobHeaderId
                        , item.FtyProductionOrderHd.FPONo.Trim()
                        , item.FtyProductionOrderHd.StartDate
                        , item.FtyProductionOrderHd.EndDate
                        // , item.FtyProductionOrderHd.StatusId
                        , item.FtyProductionOrderHd.Remarks.Trim()
                        , item.FtyProductionOrderHd.Qty
                        , item.FtyProductionOrderHd.CreateUserId);
                }
                else
                {
                    FPODetails.Rows.Add(item.SODelivDtId
                        , item.SOItemDtId
                        , item.Qty);
                }
            }

            para.Add("FPOHeaderDT", FPOHeader.AsTableValuedParameter("FPOHeaderType"));
            para.Add("FPODetailDT", FPODetails.AsTableValuedParameter("FPODeailType"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransFtyProdOrderSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<ReturnFPODetailsDto>> GetFPODetailsAsync(string FPONo)
        {
            IEnumerable<ReturnFPODetailsDto> fPODetails;
            DynamicParameters para = new DynamicParameters();

            para.Add("FPONo", FPONo);

            fPODetails = await DbConnection.QueryAsync<ReturnFPODetailsDto>("spTransFtyProdOrderGetDetails", para
                    , commandType: CommandType.StoredProcedure);
            return fPODetails;
        }

        public async Task<int> DeleteFPOAsync(DeleteFPODto fPODto)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("UserId", fPODto.UserId);
            para.Add("FPOId", fPODto.FPOId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spTransFtyProdOrderDelete", para
                , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<int> CancelJobCardAsync(CancelJobcardDto JobHDto)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("UserId", JobHDto.UserId);
            para.Add("JobHeaderId", JobHDto.JobHeaderId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spTransJobCardCancel", para
                , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<FPPOProductionDto> GetFPPOInDetailsAsync(int FPPODId)
        {
            FPPOProductionDto fPPODetails;
            DynamicParameters para = new DynamicParameters();

            para.Add("FPPODId", FPPODId);

            fPPODetails = await DbConnection.QueryFirstOrDefaultAsync<FPPOProductionDto>("spTransProductionInGetDetails", para
                    , commandType: CommandType.StoredProcedure);
            return fPPODetails;
        }

        public async Task<int> SaveFPPOInAsync(TransProductionDetails prodDetails)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("FPPODId", prodDetails.FPPODId);
            para.Add("BalQty", prodDetails.ValidationQty);
            para.Add("InQty", prodDetails.Qty);
            para.Add("ReceiveSiteId", prodDetails.ReceiveSiteId);
            para.Add("DispatchId", prodDetails.DispatchId);
            para.Add("UserId", prodDetails.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spTransProductionInSave", para
                , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<TransProductionTotalDto>> GetTransProductionTotAsync()
        {
            IEnumerable<TransProductionTotalDto> totProd;
            totProd = await DbConnection.QueryAsync<TransProductionTotalDto>("spTransProductionGetTotal", null
                , commandType: CommandType.StoredProcedure);

            return totProd;
        }

        public async Task<FPPOProductionDto> GetFPPOOutDetailsAsync(int FPPODId)
        {
            FPPOProductionDto fPPODetails;
            DynamicParameters para = new DynamicParameters();

            para.Add("FPPODId", FPPODId);

            fPPODetails = await DbConnection.QueryFirstOrDefaultAsync<FPPOProductionDto>("spTransProductionOutGetDetails", para
                    , commandType: CommandType.StoredProcedure);
            return fPPODetails;
        }

        public async Task<int> SaveFPPOOutAsync(TransProductionDetails prodDetails)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("FPPODId", prodDetails.FPPODId);
            para.Add("BalQty", prodDetails.ValidationQty);
            para.Add("OutQty", prodDetails.Qty);
            para.Add("ReceiveSiteId", prodDetails.ReceiveSiteId);
            para.Add("DispatchId", prodDetails.DispatchId);
            para.Add("UserId", prodDetails.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spTransProductionOutSave", para
                , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<int> SaveFPPORejectAsync(List<TransProdDetailsDto> prodDetails)
        {
            DataTable DamageDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            DamageDT.Columns.Add("RejReasonId", typeof(int));

            foreach (var item in prodDetails)
            {
                DamageDT.Rows.Add(item.RejReasonId);
            }

            para.Add("FPPODId", prodDetails[0].FPPODId);
            para.Add("BalQty", prodDetails[0].ValidationQty);
            para.Add("RejectQty", prodDetails[0].Qty);
            para.Add("ReceiveSiteId", prodDetails[0].ReceiveSiteId);
            para.Add("DispatchId", prodDetails[0].DispatchId);
            para.Add("UserId", prodDetails[0].CreateUserId);

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            para.Add("DamageDT", DamageDT.AsTableValuedParameter("DamageType"));

            var result = await DbConnection.ExecuteAsync("spTransProductionRejectSave", para
                , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<PendDispatchDto>> GetPendDispatchDtAsync(PendDispatchDto prod)
        {
            IEnumerable<PendDispatchDto> prodDetails;
            DynamicParameters para = new DynamicParameters();

            para.Add("DispatchSiteId", prod.DispatchSiteId);
            para.Add("CustomerId", prod.CustomerId);

            prodDetails = await DbConnection.QueryAsync<PendDispatchDto>("spTransDispatchGetPendDetails", para
                    , commandType: CommandType.StoredProcedure);
            return prodDetails;
        }


        public async Task<IEnumerable<DispatchNoListDto>> GetDispatchListAsync(string customerRef)
        {
            IEnumerable<DispatchNoListDto> dispatchList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerRef", customerRef);

            dispatchList = await DbConnection.QueryAsync<DispatchNoListDto>("spTransDispatchGetList", para
                    , commandType: CommandType.StoredProcedure);
            return dispatchList;
        }


        public async Task<IEnumerable<DispatchNoListDto>> GetDispatchListsAsync(string dispatchNo)
        {
            IEnumerable<DispatchNoListDto> dispatchLists;
            DynamicParameters para = new DynamicParameters();

            para.Add("RefNo", dispatchNo);
            para.Add("Result", "DN");

            dispatchLists = await DbConnection.QueryAsync<DispatchNoListDto>("spTransDispatchGetLists", para
                    , commandType: CommandType.StoredProcedure);
            return dispatchLists;
        }

        public async Task<IEnumerable<DispatchedDetDto>> GetDispatchDetails(DispatchSearchDto dispatch)
        {
            IEnumerable<DispatchedDetDto> dispDetails;
            DynamicParameters para = new DynamicParameters();

            para.Add("DispatchNo", dispatch.DispatchNo);
            para.Add("DispatchType", dispatch.DispatchType);

            dispDetails = await DbConnection.QueryAsync<DispatchedDetDto>("spTransDispatchGetDetails", para
                    , commandType: CommandType.StoredProcedure);
            return dispDetails;
        }

        public async Task<IEnumerable<DispatchStockRespondDto>> GetDispatchStockAsync(DispatchRequestDto requestDto)
        {
            IEnumerable<DispatchStockRespondDto> prodDetails;
            DynamicParameters para = new DynamicParameters();

            para.Add("FromSiteId", requestDto.FromSiteId);
            para.Add("Category", requestDto.Category);
            para.Add("ProdGroup", requestDto.ProdGroup);
            para.Add("ProdType", requestDto.ProdType);

            prodDetails = await DbConnection.QueryAsync<DispatchStockRespondDto>("spTransDispatchStockDt", para
                    , commandType: CommandType.StoredProcedure);
            return prodDetails;
        }

        public async Task<ReturnDto> SaveDispatchedDtAsync(TransDispatchDto dispatch)
        {
            DataTable DispatchDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            para.Add("DispatchNo", dispatch.DispatchHeader.DispatchNo);
            para.Add("CustomerId", dispatch.DispatchHeader.CustomerId);
            para.Add("CusLocationId", dispatch.DispatchHeader.CusLocationId);
            para.Add("DispatchSiteId", dispatch.DispatchHeader.DispatchSiteId);
            para.Add("Reason", dispatch.DispatchHeader.Reason);
            para.Add("VehicleNo", dispatch.DispatchHeader.VehicleNo);
            para.Add("LoactionId", dispatch.DispatchHeader.LocationId);
            para.Add("UserId", dispatch.DispatchHeader.CreateUserId);
            para.Add("SupplierId", dispatch.DispatchHeader.SupplierId);
            para.Add("ToSiteId", dispatch.DispatchHeader.ToSiteId);
            para.Add("DispatchType", dispatch.DispatchHeader.DispatchType);

            DispatchDT.Columns.Add("SOItemId", typeof(long));
            DispatchDT.Columns.Add("SODelivDtId", typeof(long));
            DispatchDT.Columns.Add("ProducedQty", typeof(int));
            DispatchDT.Columns.Add("DispatchedQty", typeof(int));
            DispatchDT.Columns.Add("BalDispatchQty", typeof(int));
            DispatchDT.Columns.Add("GRNHeaderId", typeof(long));
            DispatchDT.Columns.Add("GRNDetailsId", typeof(long));
            DispatchDT.Columns.Add("StockId", typeof(long));
            DispatchDT.Columns.Add("Price", typeof(decimal));

            foreach (var item in dispatch.DispatchDetails)
            {
                DispatchDT.Rows.Add(item.SOItemId == null ? 0 : item.SOItemId,
                    item.SODelivDtId == null ? 0 : item.SODelivDtId,
                    item.ProducedQty,
                    item.DispatchedQty,
                    item.BalDispatchQty,
                    item.GRNHeaderId == null ? 0 : item.GRNHeaderId,
                    item.GRNDetailsId == null ? 0 : item.GRNDetailsId,
                    item.StockId == null ? 0 : item.StockId,
                    item.Price == null ? 0 : item.Price
                    );

            }

            // para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            para.Add("DispatchDT", DispatchDT.AsTableValuedParameter("DispatchDetType"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransDispatchSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }


        public async Task<int> CancelDispatchDtAsync(TransDispatchHeader dispHd)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("DispatchHdId", dispHd.AutoId);
            para.Add("UserId", dispHd.CancelUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spTransDispatchCancel", para
                    , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<ReturnDto> SaveCostingAsync(List<SavedCostingDto> costDt)
        {
            DataTable CostHeaderDT = new DataTable();
            DataTable CostDetailDT = new DataTable();
            DataTable CostSpeInsDT = new DataTable();

            DynamicParameters para = new DynamicParameters();

            CostHeaderDT.Columns.Add("CostHeaderId", typeof(long));
            CostHeaderDT.Columns.Add("UserId", typeof(int));
            CostHeaderDT.Columns.Add("RefNo", typeof(string));
            CostHeaderDT.Columns.Add("VersionControl", typeof(int));
            CostHeaderDT.Columns.Add("CustomerId", typeof(int));
            CostHeaderDT.Columns.Add("ArticleId", typeof(long));
            CostHeaderDT.Columns.Add("ColorId", typeof(int));
            CostHeaderDT.Columns.Add("SizeId", typeof(int));
            CostHeaderDT.Columns.Add("Combination", typeof(string));
            CostHeaderDT.Columns.Add("NoOfUps", typeof(int));
            CostHeaderDT.Columns.Add("BrandCodeId", typeof(int));
            CostHeaderDT.Columns.Add("PDHeaderId", typeof(int));
            CostHeaderDT.Columns.Add("BoardLength", typeof(decimal));
            CostHeaderDT.Columns.Add("BoardWidth", typeof(decimal));
            CostHeaderDT.Columns.Add("SQM", typeof(decimal));
            CostHeaderDT.Columns.Add("ReelSize", typeof(int));
            CostHeaderDT.Columns.Add("ActualReal", typeof(int));
            CostHeaderDT.Columns.Add("TrimWaste", typeof(decimal));
            CostHeaderDT.Columns.Add("TotNetWeight", typeof(decimal));
            CostHeaderDT.Columns.Add("TotGrossWeight", typeof(decimal));
            CostHeaderDT.Columns.Add("Tollerence", typeof(decimal));
            CostHeaderDT.Columns.Add("TotalBoxCost", typeof(decimal));
            CostHeaderDT.Columns.Add("Markup", typeof(decimal));
            CostHeaderDT.Columns.Add("Commission", typeof(decimal));
            CostHeaderDT.Columns.Add("MOQCost", typeof(decimal));
            CostHeaderDT.Columns.Add("SellingPrice", typeof(decimal));
            CostHeaderDT.Columns.Add("TotMOQCost", typeof(decimal));
            CostHeaderDT.Columns.Add("ProfitMarkup", typeof(decimal));
            CostHeaderDT.Columns.Add("CommSelPrice", typeof(decimal));
            CostHeaderDT.Columns.Add("CartonTypeId", typeof(int));
            CostHeaderDT.Columns.Add("ReferenceCostId", typeof(long));
            CostHeaderDT.Columns.Add("CostCoppyType", typeof(string));
            CostHeaderDT.Columns.Add("DimensionId", typeof(int));

            CostDetailDT.Columns.Add("CostGroupId", typeof(byte));
            CostDetailDT.Columns.Add("GroupOrder", typeof(int));
            CostDetailDT.Columns.Add("ArticleId", typeof(long));
            CostDetailDT.Columns.Add("ColorId", typeof(int));
            CostDetailDT.Columns.Add("SizeId", typeof(int));
            CostDetailDT.Columns.Add("UnitId", typeof(int));
            CostDetailDT.Columns.Add("GSM", typeof(int));
            CostDetailDT.Columns.Add("FluteId", typeof(int));
            CostDetailDT.Columns.Add("Factor", typeof(decimal));
            CostDetailDT.Columns.Add("Cost", typeof(decimal));
            CostDetailDT.Columns.Add("Base", typeof(string));
            CostDetailDT.Columns.Add("BaseValue", typeof(decimal));
            CostDetailDT.Columns.Add("Westage", typeof(decimal));
            CostDetailDT.Columns.Add("ArtiUOMConvId", typeof(int));
            CostDetailDT.Columns.Add("NetCons", typeof(decimal));
            CostDetailDT.Columns.Add("GrossCons", typeof(decimal));
            CostDetailDT.Columns.Add("CostPcs", typeof(decimal));
            CostDetailDT.Columns.Add("ConsBase", typeof(decimal));
            CostDetailDT.Columns.Add("MultiCon", typeof(int));

            CostSpeInsDT.Columns.Add("SpeInsId", typeof(int));
            CostSpeInsDT.Columns.Add("Value", typeof(string));

            foreach (var item in costDt)
            {
                if (item.CostingHeader != null)
                {
                    CostHeaderDT.Rows.Add(
                        item.CostingHeader.AutoId,
                        item.CostingHeader.CreateUserId,
                        item.CostingHeader.RefNo,
                        item.CostingHeader.VersionControl,
                        item.CostingHeader.CustomerId,
                        item.CostingHeader.ArticleId,
                        item.CostingHeader.ColorId,
                        item.CostingHeader.SizeId,
                        item.CostingHeader.Combination,
                        item.CostingHeader.NoOfUps,
                        item.CostingHeader.BrandCodeId,
                        item.CostingHeader.PDHeaderId,
                        item.CostingHeader.BoardLength,
                        item.CostingHeader.BoardWidth,
                        item.CostingHeader.SQM,
                        item.CostingHeader.ReelSize,
                        item.CostingHeader.ActualReal,
                        item.CostingHeader.TrimWaste,
                        item.CostingHeader.TotNetWeight,
                        item.CostingHeader.TotGrossWeight,
                        item.CostingHeader.Tollerence,
                        item.CostingHeader.TotalBoxCost,
                        item.CostingHeader.Markup,
                        item.CostingHeader.Commission,
                        item.CostingHeader.MOQCost,
                        item.CostingHeader.SellingPrice,
                        item.CostingHeader.TotMOQCost,
                        item.CostingHeader.ProfitMarkup,
                        item.CostingHeader.CommSelPrice,
                        item.CostingHeader.CartonTypeId,
                        item.CostingHeader.ReferenceCostId,
                        item.CostingHeader.CostCoppyType,
                        item.CostingHeader.DimensionId
                    );
                }
                else if (item.CostingDetails != null)
                {
                    foreach (var det in item.CostingDetails)
                    {
                        CostDetailDT.Rows.Add(det.CostGroupId,
                            det.GroupOrder,
                            det.ArticleId,
                            det.ColorId,
                            det.SizeId,
                            det.UnitId,
                            det.GSM,
                            det.FluteId,
                            det.Factor,
                            det.Cost,
                            det.Base,
                            det.BaseValue,
                            det.Wastage,
                            det.ArtiUOMConvId,
                            det.NetCon,
                            det.GrossCon,
                            det.CostPcs,
                            det.ConsBase,
                            det.MultiCon);
                    }
                }
                else if (item.CostingSpecial != null)
                {
                    foreach (var spe in item.CostingSpecial)
                    {
                        CostSpeInsDT.Rows.Add(
                            spe.SpeInstId,
                            spe.Value
                        );
                    }
                }
            }

            para.Add("CostHeaderDT", CostHeaderDT.AsTableValuedParameter("CostHeaderType"));
            para.Add("CostDetailDT", CostDetailDT.AsTableValuedParameter("CostDetailsType"));
            para.Add("CostSpecialInsDT", CostSpeInsDT.AsTableValuedParameter("CostSpecialInsType"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransCostingSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }


        public async Task<CostingSheetDto> GetCostingDetailsAsync(CostHeaderDto costHearder)
        {
            CostingSheetDto costSheet = new CostingSheetDto();
            DynamicParameters para = new DynamicParameters();

            para.Add("CostHeaderId", costHearder.AutoId);
            para.Add("Option", costHearder.Status);

            // using (var multi = DbConnection.QueryMultiple(sql, new {InvoiceID = 1}))
            using (var multi = await DbConnection.QueryMultipleAsync("spTransCostingGetDetails", para, commandType: CommandType.StoredProcedure))
            {
                costSheet.costHeader = multi.Read<CostingHeaderDto>();
                costSheet.costDetails = multi.Read<CostingDetailsDto>();
                costSheet.costSpecials = multi.Read<CostingSpecialDto>();
            }
            return costSheet;
        }

        public async Task<IEnumerable<CostHeaderDto>> GetCostHeaderAsync(CostHeaderDto costHead)
        {
            IEnumerable<CostHeaderDto> costHeaderList;
            DynamicParameters para = new DynamicParameters();

            para.Add("ArticleColorSizeId", costHead.ArtColorSizeId);
            para.Add("BrandCodeId", costHead.BrandCodeId);
            para.Add("CustomerId", costHead.CustomerId);

            costHeaderList = await DbConnection.QueryAsync<CostHeaderDto>("spTransCostingGetHeader", para
                    , commandType: CommandType.StoredProcedure);

            return costHeaderList;
        }

        public async Task<IEnumerable<CostHeaderDto>> GetCostHeaderListAsync(long CustomerId)
        {
            IEnumerable<CostHeaderDto> costHeaderList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerId", CustomerId);

            costHeaderList = await DbConnection.QueryAsync<CostHeaderDto>("spTransCostingGetHdList", para
                    , commandType: CommandType.StoredProcedure);
            return costHeaderList;
        }

        public async Task<IEnumerable<CostHeaderDto>> GetCostHeaderByRefListAsync(string RefNo)
        {
            IEnumerable<CostHeaderDto> costHeaderList;
            DynamicParameters para = new DynamicParameters();

            para.Add("RefNo", RefNo);

            costHeaderList = await DbConnection.QueryAsync<CostHeaderDto>("spTransCostingGetByCosId", para
                    , commandType: CommandType.StoredProcedure);
            return costHeaderList;
        }

        public async Task<int> AttachCostSheetSOAsync(TransSalesOrderItemDt soItemDt)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("SalesItemId", soItemDt.AutoId);
            para.Add("CostId", soItemDt.CostingId);
            para.Add("UserId", soItemDt.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spTransSalesOrderAttachCS", para
                    , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<int> RemoveCostSheetSOAsync(TransSalesOrderItemDt soItemDt)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("SalesItemId", soItemDt.AutoId);
            para.Add("UserId", soItemDt.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spTransSalesOrderRemoveCS", para
                    , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<ApproveUsersGetDto> GetApprovalRouteDetailsAsync(ApproveUserDto appUser)
        {
            ApproveUsersGetDto approveList = new ApproveUsersGetDto();
            DynamicParameters para = new DynamicParameters();

            para.Add("UserId", appUser.UserId);
            para.Add("Module", appUser.Module);

            using (var multi = await DbConnection.QueryMultipleAsync("spTransApprovalRoutingDetails", para, commandType: CommandType.StoredProcedure))
            {
                approveList.approveUsers = multi.Read<ApproveUserDto>();
                approveList.userDetails = multi.Read<ApproveUserDto>();
            }

            return approveList;
        }

        public async Task<int> SaveApproveCenterAsync(TransApprovalCenter appCenter)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId", appCenter.AutoId);
            para.Add("Module", appCenter.ModuleName);
            para.Add("AssignUser", appCenter.AssigneUser);
            para.Add("RequestedBy", appCenter.RequestedBy);
            para.Add("RefId", appCenter.RefId);
            para.Add("RefNo", appCenter.RefNo);
            para.Add("Remarks", appCenter.Remarks);
            para.Add("Status", appCenter.Status);
            para.Add("Details", appCenter.Details);
            para.Add("IsFinal", appCenter.IsFinal);
            para.Add("UserId", appCenter.RequestedBy);

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await DbConnection.ExecuteAsync("spTransApproveCenterSave", para
                    , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<ApproveCenterDto>> GetApproveCenterDetailsAsync(int userId)
        {
            IEnumerable<ApproveCenterDto> approveList;
            DynamicParameters para = new DynamicParameters();

            para.Add("UserId", userId);

            approveList = await DbConnection.QueryAsync<ApproveCenterDto>("spTransApproveCenterGetDetails", para
                    , commandType: CommandType.StoredProcedure);

            return approveList;
        }

        public async Task<int> SaveSalesOrderUploadAsync(TransSalesOrderFileUpload upload)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("UserId", upload.UploadUserID);
            para.Add("SOHeaderId", upload.SOHeaderId);
            para.Add("FileName", upload.FileName);
            para.Add("FileSize", upload.FileSize);
            para.Add("DocFile", upload.DocFile);
            para.Add("AutoId", upload.AutoId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spTransSalesOrderSaveFile", para
                    , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        // public async Task<IEnumerable<TransFtyProductionOrder>> GetMINPendFPODetailsAsync()
        // {   
        //     IEnumerable<TransFtyProductionOrder> FPOList;
        //     // DynamicParameters para = new DynamicParameters();

        //     FPOList = await DbConnection.QueryAsync<TransFtyProductionOrder>("spTransMINGetPendFPO" , null
        //             , commandType: CommandType.StoredProcedure);

        //     return FPOList;
        // }

        public async Task<IEnumerable<MINDetailsDto>> GetMINDetailsAsync(long MINHeaderId)
        {
            IEnumerable<MINDetailsDto> MINList;
            DynamicParameters para = new DynamicParameters();

            para.Add("MINHeaderId", MINHeaderId);

            MINList = await DbConnection.QueryAsync<MINDetailsDto>("spTransMINGetDetails", para
                    , commandType: CommandType.StoredProcedure);

            return MINList;
        }
        public async Task<logDto> GetCostLogDetailsAsync(long CostingId)
        {
            logDto logD;
            DynamicParameters para = new DynamicParameters();

            para.Add("CostHeaderId", CostingId);

            logD = await DbConnection.QuerySingleAsync<logDto>("spTransCostingGetLogDetails", para
                    , commandType: CommandType.StoredProcedure);
            return logD;
        }
        public async Task<int> CancelSalesOrderAsync(TransSalesOrderHd salesOrderHd)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("SalesOrderHdId", salesOrderHd.AutoId);
            para.Add("UserId", salesOrderHd.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spTransSalesOrderCancel", para
                    , commandType: CommandType.StoredProcedure);
            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<CostingAttachSODto>> GetCostAttachedSOListAsync(long CostingId)
        {
            IEnumerable<CostingAttachSODto> costAttSOList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CostingId", CostingId);

            costAttSOList = await DbConnection.QueryAsync<CostingAttachSODto>("spTransCostingGetSOList", para
                    , commandType: CommandType.StoredProcedure);

            return costAttSOList;
        }

        public async Task<IEnumerable<SalesOrderStatusDto>> GetSalesOrderStatusAsync(string CustPORef)
        {
            IEnumerable<SalesOrderStatusDto> salesOrderStatusList;
            DynamicParameters para = new DynamicParameters();

            para.Add("PORef", CustPORef);

            salesOrderStatusList = await DbConnection.QueryAsync<SalesOrderStatusDto>("spTransSalesOrderStatus", para
                    , commandType: CommandType.StoredProcedure);

            return salesOrderStatusList;
        }

        public async Task<int> updateSalesOrderPriceAsync(TransSalesOrderItemDt soItemDt)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("SalesItemId", soItemDt.AutoId);
            para.Add("Price", soItemDt.Price);
            para.Add("UserId", soItemDt.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spTransSalesOrderPriceUpdate", para
                    , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<int> saveSalesOrderDefaultAsync(TransSalesOrderDefault salesDefault)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerId", salesDefault.CustomerId);
            para.Add("CusCurrencyId", salesDefault.CusCurrencyId);
            para.Add("CustomerLocId", salesDefault.CustomerLocId);
            para.Add("SalesCategoryId", salesDefault.SalesCategoryId);
            para.Add("CustomerUserId", salesDefault.CustomerUserId);
            para.Add("SalesAgentId", salesDefault.SalesAgentId);
            para.Add("CustomerDivId", salesDefault.CustomerDivId);
            para.Add("PaymentTermId", salesDefault.PaymentTermId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spTrnsSaveSalesOrderDefault", para
                    , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }
        public async Task<IEnumerable<SalesOrderRetDto>> GetSalesOrderDefaultAsync(int CustomerId)
        {
            IEnumerable<SalesOrderRetDto> salOrderHDDefaultValue;
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerId", CustomerId);

            salOrderHDDefaultValue = await DbConnection.QueryAsync<SalesOrderRetDto>("spSalesOrderHDDefaultDetails", para
                    , commandType: CommandType.StoredProcedure);

            return salOrderHDDefaultValue;
        }
        public async Task<IEnumerable<TransSalesOrderDefault>> GetSalesDefaultAsync(int CustomerId)
        {
            IEnumerable<TransSalesOrderDefault> result;
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerId", CustomerId);

            result = await DbConnection.QueryAsync<TransSalesOrderDefault>("spSalesOrderHDDefaultDetails", para
            , commandType: CommandType.StoredProcedure);

            return result;
        }

        #region CCS-Dashboard

        public async Task<IEnumerable<DashboarDetailsDto>> GetDashboardOneDetailsAsync(DashboardSearchDto dashDto)
        {
            IEnumerable<DashboarDetailsDto> Dashboardetails;
            DynamicParameters para = new DynamicParameters();

            para.Add("ActionId", dashDto.ActionId);
            para.Add("Transdate", dashDto.Transdate);
            para.Add("Todate", dashDto.Todate);

            Dashboardetails = await DbConnection.QueryAsync<DashboarDetailsDto>("spDashboard_01_DetailList", para
                    , commandType: CommandType.StoredProcedure);

            return Dashboardetails;
        }

        #endregion

        #region DispatchSOQtyCheck
        public async Task<IEnumerable<DispatchSODto>> GetPossibilityAsync(long SOHeaderId)
        {
            IEnumerable<DispatchSODto> result;
            DynamicParameters para = new DynamicParameters();

            para.Add("SOHeaderId", SOHeaderId);

            result = await DbConnection.QueryAsync<DispatchSODto>("spTransCheckSOToDispatchQty", para
                    , commandType: CommandType.StoredProcedure);

            return result;
        }
        #endregion
        #region TransfairableDelivery
        public async Task<IEnumerable<TransfairableDeliveryDto>> GetTransferableDeliveriesAsync(TransfairableDeliveryDto prod)
        {
            IEnumerable<TransfairableDeliveryDto> transDetails;
            DynamicParameters para = new DynamicParameters();

            para.Add("SoHeaderId", prod.SoHeaderId);
            para.Add("DispatchSite", prod.DispatchSiteId);

            transDetails = await DbConnection.QueryAsync<TransfairableDeliveryDto>("spGetTransferablePOS", para
                    , commandType: CommandType.StoredProcedure);
            return transDetails;
        }
        #endregion

        public async Task<IEnumerable<TransfairablePoRefDto>> GetTransfairablePoRefAsync(string RefNo)
        {
            IEnumerable<TransfairablePoRefDto> result;
            DynamicParameters para = new DynamicParameters();

            para.Add("RefNo", RefNo);

            result = await DbConnection.QueryAsync<TransfairablePoRefDto>("spTransGetAllTransfairablePos", para
                    , commandType: CommandType.StoredProcedure);
            return result;
        }

        #region TransfairableAlterDelivery
        public async Task<IEnumerable<TransfAlternateDeliveryDto>> TransfairableAlterDeliveryAsync(long FPPOID)
        {
            IEnumerable<TransfAlternateDeliveryDto> transDetails;
            DynamicParameters para = new DynamicParameters();

            para.Add("FPPODID", FPPOID);

            transDetails = await DbConnection.QueryAsync<TransfAlternateDeliveryDto>("spGetTransferToPOS", para
                    , commandType: CommandType.StoredProcedure);
            return transDetails;
        }
        #endregion
        #region SaveTranfer
        public async Task<ReturnDto> SaveTranferAsync(SavedTranferDto trnsDto)
        {
            DataTable TranferDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            ///////////----------- TRANFER HEADER ------------------------
            para.Add("TRANSHeaderId", trnsDto.TranferHeader.idTransH);
            para.Add("TRNSNo", trnsDto.TranferHeader.TransNo);
            para.Add("SiteId", trnsDto.TranferHeader.idSite);
            para.Add("UserId", trnsDto.TranferHeader.CreateUserId);
            para.Add("ModuleId", trnsDto.TranferHeader.ModuleId);
            para.Add("LocationId", trnsDto.TranferHeader.LocationId);
            para.Add("SoHeaderId", trnsDto.TranferHeader.SoHeaderId);

            /////// ---------- TRANFER DETAILS ------------------
            TranferDT.Columns.Add("idFPPOD_From", typeof(long));
            TranferDT.Columns.Add("SOHeaderId_From", typeof(long));
            TranferDT.Columns.Add("iQty", typeof(int));
            TranferDT.Columns.Add("idFPPOD_To", typeof(long));
            TranferDT.Columns.Add("SOHeaderId_To", typeof(long));

            foreach (var item in trnsDto.TranferDetails)
            {
                TranferDT.Rows.Add(item.idFPPOD_From,
                                item.SOHeaderId_From,
                                item.iQty,
                                item.idFPPOD_To,
                                item.SOHeaderId_To);
            }
            para.Add("TranferDT", TranferDT.AsTableValuedParameter("TransferType"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransTransferSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }
        #endregion
        #region TransferList
        public async Task<IEnumerable<TransferListDto>> TransferListAsync(int CustomerId)
        {
            IEnumerable<TransferListDto> transList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerId", CustomerId);

            transList = await DbConnection.QueryAsync<TransferListDto>("spTransGetAllPoTransferList", para
                    , commandType: CommandType.StoredProcedure);
            return transList;
        }
        #endregion

        #region GetTranferDetails
        public async Task<TranferGetDto> GetTranferDetailsAsync(long TransferHdId)
        {
            TranferGetDto trnsferDt = new TranferGetDto();
            DynamicParameters para = new DynamicParameters();

            para.Add("TransferHdId", TransferHdId);

            using (var multi = await DbConnection.QueryMultipleAsync("spTransTransferGet", para, commandType: CommandType.StoredProcedure))
            {
                trnsferDt.TranferHeader = multi.Read<TransTransferHeader>();
                trnsferDt.TranferDetails = multi.Read<TranferGetDetailsDto>();
            }
            return trnsferDt;
        }
        #endregion


        #region "Block Booking"

        public async Task<IEnumerable<BlockBookingDto>> GetBlockBookingData(BlockBookingDto wsdt)
        {
            DataTable BlockBookingDT = new DataTable();
            IEnumerable<BlockBookingDto> BlockBookingList;

            DynamicParameters para = new DynamicParameters();

            BlockBookingDT.Columns.Add("F01", typeof(int));
            BlockBookingDT.Columns.Add("F02", typeof(int));
            BlockBookingDT.Columns.Add("F03", typeof(int));
            BlockBookingDT.Columns.Add("F04", typeof(int));
            BlockBookingDT.Columns.Add("F05", typeof(int));
            BlockBookingDT.Columns.Add("F06", typeof(int));
            BlockBookingDT.Columns.Add("F07", typeof(int));
            BlockBookingDT.Columns.Add("F08", typeof(int));
            BlockBookingDT.Columns.Add("F09", typeof(int));
            BlockBookingDT.Columns.Add("F10", typeof(int));
            BlockBookingDT.Columns.Add("F11", typeof(int));
            BlockBookingDT.Columns.Add("F12", typeof(int));
            BlockBookingDT.Columns.Add("F13", typeof(int));
            BlockBookingDT.Columns.Add("F14", typeof(int));
            BlockBookingDT.Columns.Add("F15", typeof(string));
            BlockBookingDT.Columns.Add("F16", typeof(string));
            BlockBookingDT.Columns.Add("F17", typeof(string));
            BlockBookingDT.Columns.Add("F18", typeof(string));
            BlockBookingDT.Columns.Add("F19", typeof(string));
            BlockBookingDT.Columns.Add("F20", typeof(decimal));
            BlockBookingDT.Columns.Add("F21", typeof(decimal));
            BlockBookingDT.Columns.Add("F22", typeof(decimal));
            BlockBookingDT.Columns.Add("F23", typeof(bool));
            BlockBookingDT.Columns.Add("F24", typeof(DateTime));
            BlockBookingDT.Columns.Add("F25", typeof(DateTime));
            BlockBookingDT.Columns.Add("F26", typeof(DateTime));

            BlockBookingDT.Rows.Add(
                 wsdt.F01, wsdt.F02, wsdt.F03, wsdt.F04,
                 wsdt.F05, wsdt.F06, wsdt.F07, wsdt.F08,
                 wsdt.F09, wsdt.F10, wsdt.F11, wsdt.F12,
                 wsdt.F13, wsdt.F14, wsdt.F15, wsdt.F16,
                 wsdt.F17, wsdt.F18, wsdt.F19, wsdt.F20,
                 wsdt.F21, wsdt.F22, wsdt.F23, wsdt.F24,
                 wsdt.F25, wsdt.F26
            );

            para.Add("UDT", BlockBookingDT.AsTableValuedParameter("UDT_BlockBooking"));

            BlockBookingList = await DbConnection.QueryAsync<BlockBookingDto>("sp_BlockBooking", para
              , commandType: CommandType.StoredProcedure);

            return BlockBookingList;
        }

        public async Task<ReturnDto> SaveBlockBookingData(List<SaveBlockBookingDto> wsDt)
        {
            DataTable TMDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            TMDT.Columns.Add("F01", typeof(int));
            TMDT.Columns.Add("F02", typeof(int));
            TMDT.Columns.Add("F03", typeof(int));
            TMDT.Columns.Add("F04", typeof(int));
            TMDT.Columns.Add("F05", typeof(int));
            TMDT.Columns.Add("F06", typeof(int));
            TMDT.Columns.Add("F07", typeof(int));
            TMDT.Columns.Add("F08", typeof(int));
            TMDT.Columns.Add("F09", typeof(int));
            TMDT.Columns.Add("F10", typeof(int));
            TMDT.Columns.Add("F11", typeof(int));
            TMDT.Columns.Add("F12", typeof(int));
            TMDT.Columns.Add("F13", typeof(int));
            TMDT.Columns.Add("F14", typeof(int));
            TMDT.Columns.Add("F15", typeof(string));
            TMDT.Columns.Add("F16", typeof(string));
            TMDT.Columns.Add("F17", typeof(string));
            TMDT.Columns.Add("F18", typeof(string));
            TMDT.Columns.Add("F19", typeof(string));
            TMDT.Columns.Add("F20", typeof(decimal));
            TMDT.Columns.Add("F21", typeof(decimal));
            TMDT.Columns.Add("F22", typeof(decimal));
            TMDT.Columns.Add("F23", typeof(bool));
            TMDT.Columns.Add("F24", typeof(DateTime));
            TMDT.Columns.Add("F25", typeof(DateTime));
            TMDT.Columns.Add("F26", typeof(DateTime));

            foreach (var item in wsDt)

            {

                var ActivityCode = item.ActiveCode;
                var ModuleId = item.ModuleId;
                var LocationId = item.LocationId;

                if (item.sBBHeader != null)
                {
                    TMDT.Rows.Add(
                     ActivityCode,
                     ModuleId,
                     LocationId,
                     item.sBBHeader.CreateUserId,
                     item.sBBHeader.idBBH,
                     item.sBBHeader.idBuyer,
                     item.sBBHeader.idCategory,
                     item.sBBHeader.idCurrency,
                     item.sBBHeader.idBuyerDepartment,
                     item.sBBHeader.IdMerchandierMaster,
                     item.sBBHeader.iStatusId,
                     item.sBBHeader.BBType,
                     0,
                     item.sBBHeader.BookingNo,
                     0,
                     0,
                     0,
                     0,
                     0,
                     0,
                     0,
                     0,
                     1,
                     item.sBBHeader.BookDate

                   );
                }

                if (item.sBBDetails != null)
                {
                    TMDT.Rows.Add(
                     ActivityCode,
                     ModuleId,
                     LocationId,
                     0,
                     item.sBBDetails.idBBH,
                     item.sBBDetails.idBBD,
                     0,
                     item.sBBDetails.idProductGroup,
                     item.sBBDetails.idSubCategory,
                     item.sBBDetails.idArticle,
                     item.sBBDetails.idSubCategoryGroup,
                     item.sBBDetails.idSeason,
                     item.sBBDetails.iYear,
                     0,
                     item.sBBDetails.cMonth,
                     0,
                     0,
                     0,
                     0,
                     item.sBBDetails.Qty,
                     item.sBBDetails.Price,
                     item.sBBDetails.Value,
                     0,
                     item.sBBDetails.StartDate,
                     item.sBBDetails.EndDate,
                     item.sBBDetails.BuyerDelDate
                   );
                }

            }

            para.Add("UDT", TMDT.AsTableValuedParameter("UDT_BlockBooking"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("sp_BlockBooking", para,
            commandType: CommandType.StoredProcedure);

            return result;

        }
        #endregion "Block Booking"

        #region "Order Creation"

        public async Task<IEnumerable<OrderCreationDto>> GetOCData(OrderCreationDto wsdt)
        {
            DataTable OrderCreationDt = new DataTable();
            IEnumerable<OrderCreationDto> OrderCreationList;

            DynamicParameters para = new DynamicParameters();

            OrderCreationDt.Columns.Add("F01", typeof(long));
            OrderCreationDt.Columns.Add("F02", typeof(long));
            OrderCreationDt.Columns.Add("F03", typeof(long));
            OrderCreationDt.Columns.Add("F04", typeof(long));
            OrderCreationDt.Columns.Add("F05", typeof(long));
            OrderCreationDt.Columns.Add("F06", typeof(long));
            OrderCreationDt.Columns.Add("F07", typeof(long));
            OrderCreationDt.Columns.Add("F08", typeof(long));
            OrderCreationDt.Columns.Add("F09", typeof(long));
            OrderCreationDt.Columns.Add("F10", typeof(long));
            OrderCreationDt.Columns.Add("F11", typeof(long));
            OrderCreationDt.Columns.Add("F12", typeof(long));
            OrderCreationDt.Columns.Add("F13", typeof(long));
            OrderCreationDt.Columns.Add("F14", typeof(long));
            OrderCreationDt.Columns.Add("F15", typeof(long));
            OrderCreationDt.Columns.Add("F16", typeof(long));
            OrderCreationDt.Columns.Add("F17", typeof(long));
            OrderCreationDt.Columns.Add("F18", typeof(long));
            OrderCreationDt.Columns.Add("F19", typeof(long));
            OrderCreationDt.Columns.Add("F20", typeof(long));
            OrderCreationDt.Columns.Add("F21", typeof(long));
            OrderCreationDt.Columns.Add("F22", typeof(long));
            OrderCreationDt.Columns.Add("F23", typeof(long));
            OrderCreationDt.Columns.Add("F24", typeof(long));
            OrderCreationDt.Columns.Add("F25", typeof(int));
            OrderCreationDt.Columns.Add("F26", typeof(int));
            OrderCreationDt.Columns.Add("F27", typeof(string));
            OrderCreationDt.Columns.Add("F28", typeof(string));
            OrderCreationDt.Columns.Add("F29", typeof(string));
            OrderCreationDt.Columns.Add("F30", typeof(string));
            OrderCreationDt.Columns.Add("F31", typeof(string));
            OrderCreationDt.Columns.Add("F32", typeof(string));
            OrderCreationDt.Columns.Add("F33", typeof(string));
            OrderCreationDt.Columns.Add("F34", typeof(string));
            OrderCreationDt.Columns.Add("F35", typeof(string));
            OrderCreationDt.Columns.Add("F36", typeof(string));
            OrderCreationDt.Columns.Add("F37", typeof(string));
            OrderCreationDt.Columns.Add("F38", typeof(string));
            OrderCreationDt.Columns.Add("F39", typeof(string));
            OrderCreationDt.Columns.Add("F40", typeof(string));
            OrderCreationDt.Columns.Add("F41", typeof(string));
            OrderCreationDt.Columns.Add("F42", typeof(string));
            OrderCreationDt.Columns.Add("F43", typeof(string));
            OrderCreationDt.Columns.Add("F44", typeof(string));
            OrderCreationDt.Columns.Add("F45", typeof(string));
            OrderCreationDt.Columns.Add("F46", typeof(string));
            OrderCreationDt.Columns.Add("F47", typeof(string));
            OrderCreationDt.Columns.Add("F48", typeof(string));
            OrderCreationDt.Columns.Add("F49", typeof(string));
            OrderCreationDt.Columns.Add("F50", typeof(string));
            OrderCreationDt.Columns.Add("F51", typeof(string));
            OrderCreationDt.Columns.Add("F52", typeof(string));
            OrderCreationDt.Columns.Add("F53", typeof(decimal));
            OrderCreationDt.Columns.Add("F54", typeof(decimal));
            OrderCreationDt.Columns.Add("F55", typeof(decimal));
            OrderCreationDt.Columns.Add("F56", typeof(DateTime));
            OrderCreationDt.Columns.Add("F57", typeof(DateTime));
            OrderCreationDt.Columns.Add("F58", typeof(DateTime));
            OrderCreationDt.Columns.Add("F59", typeof(DateTime));

            OrderCreationDt.Rows.Add(
                      wsdt.F01, wsdt.F02, wsdt.F03, wsdt.F04,
                      wsdt.F05, wsdt.F06, wsdt.F07, wsdt.F08,
                      wsdt.F09, wsdt.F10, wsdt.F11, wsdt.F12,
                      wsdt.F13, wsdt.F14, wsdt.F15, wsdt.F16,
                      wsdt.F17, wsdt.F18, wsdt.F19, wsdt.F20,
                      wsdt.F21, wsdt.F22, wsdt.F23, wsdt.F24,
                      wsdt.F25, wsdt.F26, wsdt.F27, wsdt.F28,
                      wsdt.F29, wsdt.F30, wsdt.F31, wsdt.F32,
                      wsdt.F33, wsdt.F34, wsdt.F35, wsdt.F36,
                      wsdt.F37, wsdt.F38, wsdt.F39, wsdt.F40,
                      wsdt.F41, wsdt.F42, wsdt.F43, wsdt.F44,
                      wsdt.F45, wsdt.F46, wsdt.F47, wsdt.F48,
                      wsdt.F49, wsdt.F50, wsdt.F51, wsdt.F52,
                      wsdt.F53, wsdt.F54, wsdt.F55, wsdt.F56,
                      wsdt.F57, wsdt.F58, wsdt.F59
            );

            para.Add("UDT", OrderCreationDt.AsTableValuedParameter("UDT_OC"));

            OrderCreationList = await DbConnection.QueryAsync<OrderCreationDto>("SP_OrderCreation", para
                , commandType: CommandType.StoredProcedure);

            return OrderCreationList;
        }

        public async Task<ReturnDto> SaveOCData(List<SaveOrderCreationDto> ocdto)

        {
            DataTable OrderCreationDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            OrderCreationDT.Columns.Add("ActivityNo", typeof(Int64));
            OrderCreationDT.Columns.Add("ModuleNo", typeof(Int64));
            OrderCreationDT.Columns.Add("CompanyNo", typeof(Int64));
            OrderCreationDT.Columns.Add("LocationNo", typeof(Int64));
            OrderCreationDT.Columns.Add("AgentNo", typeof(Int64));
            OrderCreationDT.Columns.Add("bActive", typeof(Int64));
            OrderCreationDT.Columns.Add("F01", typeof(Int64));
            OrderCreationDT.Columns.Add("F02", typeof(Int64));
            OrderCreationDT.Columns.Add("F03", typeof(Int64));
            OrderCreationDT.Columns.Add("F04", typeof(Int64));
            OrderCreationDT.Columns.Add("F05", typeof(Int64));
            OrderCreationDT.Columns.Add("F06", typeof(Int64));
            OrderCreationDT.Columns.Add("F07", typeof(Int64));
            OrderCreationDT.Columns.Add("F08", typeof(Int64));
            OrderCreationDT.Columns.Add("F09", typeof(Int64));
            OrderCreationDT.Columns.Add("F10", typeof(Int64));
            OrderCreationDT.Columns.Add("F11", typeof(Int64));
            OrderCreationDT.Columns.Add("F12", typeof(Int64));
            OrderCreationDT.Columns.Add("F13", typeof(Int64));
            OrderCreationDT.Columns.Add("F14", typeof(decimal));
            OrderCreationDT.Columns.Add("F15", typeof(decimal));
            OrderCreationDT.Columns.Add("F16", typeof(string));
            OrderCreationDT.Columns.Add("F17", typeof(string));
            OrderCreationDT.Columns.Add("F18", typeof(string));
            OrderCreationDT.Columns.Add("F19", typeof(string));
            OrderCreationDT.Columns.Add("F20", typeof(string));
            OrderCreationDT.Columns.Add("F21", typeof(string));
            OrderCreationDT.Columns.Add("F22", typeof(string));
            OrderCreationDT.Columns.Add("F23", typeof(DateTime));
            OrderCreationDT.Columns.Add("F24", typeof(DateTime));
            OrderCreationDT.Columns.Add("F25", typeof(DateTime));


            foreach (var item in ocdto)

            {

                var ActivityNo = item.ActivityNo;
                var ModuleNo = item.ModuleNo;
                var CompanyNo = item.CompanyNo;
                var LocationNo = item.LocationNo;
                var AgentNo = item.AgentNo;
                var bActive = item.bActive;

                if (item.sSalesOrderHeader != null)
                {
                    OrderCreationDT.Rows.Add(
                    ActivityNo,
                    ModuleNo,
                    CompanyNo,
                    LocationNo,
                    AgentNo,
                    bActive,
                    0,//1
                    0,//2
                    0,//3
                    item.sSalesOrderHeader.AutoId,//4
                    item.sSalesOrderHeader.OCHIdx,//5
                    0,//6
                    0,//7
                    0,//8
                    0,//9
                    0,//10
                    0,//11
                    0,//12
                    0,//13
                    0,//14
                    0,//15 
                    0,//16
                    0,//17
                    item.sSalesOrderHeader.PoNo,//18
                    0,//19
                    0,//20
                    0,//21
                    "H",//22
                    item.sSalesOrderHeader.BuyerDelDate,//23
                    item.sSalesOrderHeader.BuyerDelDate,
                    item.sSalesOrderHeader.BuyerDelDate
                  );
                }

                if (item.sSalesOrderDeatails != null)
                {
                    OrderCreationDT.Rows.Add(
                    ActivityNo,
                    ModuleNo,
                    CompanyNo,
                    LocationNo,
                    AgentNo,
                    bActive,
                    0,//1
                    0,//2
                    0,//3
                    0,//4
                    0,//5
                    item.sSalesOrderDeatails.AutoId,//6
                    item.sSalesOrderDeatails.SOHId,//7
                    item.sSalesOrderDeatails.MISPId,//8
                    item.sSalesOrderDeatails.MSId,//9
                    item.sSalesOrderDeatails.MPId,//10
                    item.sSalesOrderDeatails.OrderQty,//11
                    item.sSalesOrderDeatails.ProductionOutQty,//12
                    item.sSalesOrderDeatails.DamageQty,//13
                    item.sSalesOrderDeatails.Price,//14
                    0,//15 
                    0,//16
                    0,//17
                    0,//18
                    0,//19
                    0,//20
                    0,//21
                    "D"
                  );
                }

            }

            para.Add("UDT", OrderCreationDT.AsTableValuedParameter("udt_OrderCreationData"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("sp_OrderCreationData", para,
            commandType: CommandType.StoredProcedure);

            return result;
        }

        #endregion "Order Creation"

        #region "Recipe"
        public async Task<IEnumerable<RecipeDto>> GetRecipeData(RecipeDto rcpdto)

        {
            DataTable RecipeDT = new DataTable();
            IEnumerable<RecipeDto> RecipeList;

            DynamicParameters para = new DynamicParameters();

            RecipeDT.Columns.Add("F01", typeof(long));
            RecipeDT.Columns.Add("F02", typeof(long));
            RecipeDT.Columns.Add("F03", typeof(long));
            RecipeDT.Columns.Add("F04", typeof(long));
            RecipeDT.Columns.Add("F05", typeof(long));
            RecipeDT.Columns.Add("F06", typeof(long));
            RecipeDT.Columns.Add("F07", typeof(long));
            RecipeDT.Columns.Add("F08", typeof(long));
            RecipeDT.Columns.Add("F09", typeof(long));
            RecipeDT.Columns.Add("F10", typeof(long));
            RecipeDT.Columns.Add("F11", typeof(long));
            RecipeDT.Columns.Add("F12", typeof(long));
            RecipeDT.Columns.Add("F13", typeof(long));
            RecipeDT.Columns.Add("F14", typeof(long));
            RecipeDT.Columns.Add("F15", typeof(long));
            RecipeDT.Columns.Add("F16", typeof(long));
            RecipeDT.Columns.Add("F17", typeof(long));
            RecipeDT.Columns.Add("F18", typeof(long));
            RecipeDT.Columns.Add("F19", typeof(long));
            RecipeDT.Columns.Add("F20", typeof(long));
            RecipeDT.Columns.Add("F21", typeof(long));
            RecipeDT.Columns.Add("F22", typeof(long));
            RecipeDT.Columns.Add("F23", typeof(long));
            RecipeDT.Columns.Add("F24", typeof(string));
            RecipeDT.Columns.Add("F25", typeof(string));
            RecipeDT.Columns.Add("F26", typeof(string));
            RecipeDT.Columns.Add("F27", typeof(string));
            RecipeDT.Columns.Add("F28", typeof(string));
            RecipeDT.Columns.Add("F29", typeof(string));
            RecipeDT.Columns.Add("F30", typeof(string));
            RecipeDT.Columns.Add("F31", typeof(string));
            RecipeDT.Columns.Add("F32", typeof(string));
            RecipeDT.Columns.Add("F33", typeof(string));
            RecipeDT.Columns.Add("F34", typeof(string));
            RecipeDT.Columns.Add("F35", typeof(string));
            RecipeDT.Columns.Add("F36", typeof(string));
            RecipeDT.Columns.Add("F37", typeof(string));
            RecipeDT.Columns.Add("F38", typeof(string));
            RecipeDT.Columns.Add("F39", typeof(decimal));
            RecipeDT.Columns.Add("F40", typeof(decimal));
            RecipeDT.Columns.Add("F41", typeof(decimal));
            RecipeDT.Columns.Add("F42", typeof(decimal));
            RecipeDT.Columns.Add("F43", typeof(decimal));
            RecipeDT.Columns.Add("F44", typeof(decimal));
            RecipeDT.Columns.Add("F45", typeof(decimal));
            RecipeDT.Columns.Add("F46", typeof(decimal));
            RecipeDT.Columns.Add("F47", typeof(DateTime));
            RecipeDT.Columns.Add("F48", typeof(DateTime));

            RecipeDT.Rows.Add(
            rcpdto.F01, rcpdto.F02, rcpdto.F03, rcpdto.F04, rcpdto.F05, rcpdto.F06, rcpdto.F07, rcpdto.F08, rcpdto.F09, rcpdto.F10,
            rcpdto.F11, rcpdto.F12, rcpdto.F13, rcpdto.F14, rcpdto.F15, rcpdto.F16, rcpdto.F17, rcpdto.F18, rcpdto.F19, rcpdto.F20,
            rcpdto.F21, rcpdto.F22, rcpdto.F23, rcpdto.F24, rcpdto.F25, rcpdto.F26, rcpdto.F27, rcpdto.F28, rcpdto.F29, rcpdto.F30,
            rcpdto.F31, rcpdto.F32, rcpdto.F33, rcpdto.F34, rcpdto.F35, rcpdto.F36, rcpdto.F37, rcpdto.F38, rcpdto.F39, rcpdto.F40,
            rcpdto.F41, rcpdto.F42, rcpdto.F43, rcpdto.F44, rcpdto.F45, rcpdto.F46, rcpdto.F47, rcpdto.F48
            );
            para.Add("UDT", RecipeDT.AsTableValuedParameter("UDT_Recipe"));

            RecipeList = await DbConnection.QueryAsync<RecipeDto>("SP_Recipe", para
            , commandType: CommandType.StoredProcedure);

            return RecipeList;
        }

        public async Task<ReturnDto> SaveRecipeData(List<SaveRecipeDto> rcpdto)

        {
            DataTable RecipeDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            RecipeDT.Columns.Add("F01", typeof(long));
            RecipeDT.Columns.Add("F02", typeof(long));
            RecipeDT.Columns.Add("F03", typeof(long));
            RecipeDT.Columns.Add("F04", typeof(long));
            RecipeDT.Columns.Add("F05", typeof(long));
            RecipeDT.Columns.Add("F06", typeof(long));
            RecipeDT.Columns.Add("F07", typeof(long));
            RecipeDT.Columns.Add("F08", typeof(long));
            RecipeDT.Columns.Add("F09", typeof(long));
            RecipeDT.Columns.Add("F10", typeof(long));
            RecipeDT.Columns.Add("F11", typeof(long));
            RecipeDT.Columns.Add("F12", typeof(long));
            RecipeDT.Columns.Add("F13", typeof(long));
            RecipeDT.Columns.Add("F14", typeof(long));
            RecipeDT.Columns.Add("F15", typeof(long));
            RecipeDT.Columns.Add("F16", typeof(long));
            RecipeDT.Columns.Add("F17", typeof(long));
            RecipeDT.Columns.Add("F18", typeof(long));
            RecipeDT.Columns.Add("F19", typeof(long));
            RecipeDT.Columns.Add("F20", typeof(long));
            RecipeDT.Columns.Add("F21", typeof(long));
            RecipeDT.Columns.Add("F22", typeof(long));
            RecipeDT.Columns.Add("F23", typeof(long));
            RecipeDT.Columns.Add("F24", typeof(string));
            RecipeDT.Columns.Add("F25", typeof(string));
            RecipeDT.Columns.Add("F26", typeof(string));
            RecipeDT.Columns.Add("F27", typeof(string));
            RecipeDT.Columns.Add("F28", typeof(string));
            RecipeDT.Columns.Add("F29", typeof(string));
            RecipeDT.Columns.Add("F30", typeof(string));
            RecipeDT.Columns.Add("F31", typeof(string));
            RecipeDT.Columns.Add("F32", typeof(string));
            RecipeDT.Columns.Add("F33", typeof(string));
            RecipeDT.Columns.Add("F34", typeof(string));
            RecipeDT.Columns.Add("F35", typeof(string));
            RecipeDT.Columns.Add("F36", typeof(string));
            RecipeDT.Columns.Add("F37", typeof(string));
            RecipeDT.Columns.Add("F38", typeof(string));
            RecipeDT.Columns.Add("F39", typeof(decimal));
            RecipeDT.Columns.Add("F40", typeof(decimal));
            RecipeDT.Columns.Add("F41", typeof(decimal));
            RecipeDT.Columns.Add("F42", typeof(decimal));
            RecipeDT.Columns.Add("F43", typeof(decimal));
            RecipeDT.Columns.Add("F44", typeof(decimal));
            RecipeDT.Columns.Add("F45", typeof(decimal));
            RecipeDT.Columns.Add("F46", typeof(decimal));
            RecipeDT.Columns.Add("F47", typeof(DateTime));
            RecipeDT.Columns.Add("F48", typeof(DateTime));

            foreach (var item in rcpdto)
            {
                var ActivityCode = item.Action;
                var ModuleId = item.ModuleId;
                var LocationId = item.LocationId;
                var UserId = item.UserId;
                var AddNewFlag = item.AddNewFlag;
                var RDidx = item.RDidx;
                var MoveStepFlag = item.MoveStepFlag;

                if (item.sRecipeHeader != null)
                {
                    RecipeDT.Rows.Add(
                        ActivityCode,//1
                        ModuleId,//2
                        LocationId,//3
                        UserId,//4
                        item.sRecipeHeader.AutoId,//5
                        0,//6
                        item.sRecipeHeader.SOHId,//7
                        0,//8
                        0, 0, 0,//9-11
                        item.sRecipeHeader.RecipeRevisionNo,//12
                        item.sRecipeHeader.SalesordertypeId,//13
                        item.sRecipeHeader.RecipeTypeId,//14
                        item.sRecipeHeader.MachineTypeId,//15
                        item.sRecipeHeader.MachineMasterId,//16
                        item.sRecipeHeader.pcsperBath,//17
                        0,//18
                        item.sRecipeHeader.CopyRHId,//19
                        0, 0,//20-21
                        1,//22
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,//23-33
                        item.sRecipeHeader.TopComment,//34
                        item.sRecipeHeader.EndComment,//35
                        item.sRecipeHeader.ActualWashType,//36
                        0, 0,//37 - 38
                        item.sRecipeHeader.pcsweight,//39
                        item.sRecipeHeader.Loadweight,//40
                        0, 0, 0, 0, 0, 0 //41-48
                );
                }
                if (item.sRecipeDetails != null)
                {
                    RecipeDT.Rows.Add(
                        ActivityCode,//1
                        ModuleId,//2
                        LocationId,//3
                        UserId,//4
                        item.sRecipeDetails.RHId,//5
                        item.sRecipeDetails.AutoId,//6
                        AddNewFlag,//7
                        item.sRecipeDetails.ArticleId,//8
                        RDidx,//9
                        item.sRecipeDetails.MainStep,//10
                        item.sRecipeDetails.SubStep,//11
                        item.sRecipeDetails.ParentId,//12
                        item.sRecipeDetails.ProcessId,//13
                        MoveStepFlag,//14
                        item.sRecipeDetails.UseById,//15
                        item.sRecipeDetails.UOMId,//16
                        item.sRecipeDetails.ProcessTime,//17
                        item.sRecipeDetails.Temperature,//18
                        0, 0, 0,//19-21
                        2,//22
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,//23-33
                        item.sRecipeDetails.Description,//34
                        item.sRecipeDetails.MiddleRemaks,//35
                        item.sRecipeDetails.StepRemaks,//36
                        item.sRecipeDetails.AddRouteCard,//37
                        0,//38
                        item.sRecipeDetails.WaterLevel,//39
                        item.sRecipeDetails.WaterRatio,//40
                        item.sRecipeDetails.ChemicalRatio,//41
                        item.sRecipeDetails.RPM,//42
                        item.sRecipeDetails.UseQty,//43
                        item.sRecipeDetails.PH, 0, 0 //44-48
                    );
                }
                if (item.sRecipeColorDetails != null)
                {
                    RecipeDT.Rows.Add(
                        ActivityCode,//1
                        ModuleId,//2
                        LocationId,//3
                        UserId,//4
                        item.sRecipeColorDetails.RHId,//5
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,//6-17
                        item.sRecipeColorDetails.ColorId,//18
                        0, 0, 0, 2,//22
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0//19-48
                    );
                }

            }

            para.Add("UDT", RecipeDT.AsTableValuedParameter("UDT_Recipe"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("SP_Recipe", para,
            commandType: CommandType.StoredProcedure);

            return result;
        }

        #endregion "Recipe"
        #region " Washing Costing"
        public async Task<IEnumerable<CostSheetDto>> GetCostingData(CostSheetDto costdto)

        {
            DataTable CostingDT = new DataTable();
            IEnumerable<CostSheetDto> CostingList;

            DynamicParameters para = new DynamicParameters();

            CostingDT.Columns.Add("F01", typeof(long));
            CostingDT.Columns.Add("F02", typeof(long));
            CostingDT.Columns.Add("F03", typeof(long));
            CostingDT.Columns.Add("F04", typeof(long));
            CostingDT.Columns.Add("F05", typeof(long));
            CostingDT.Columns.Add("F06", typeof(long));
            CostingDT.Columns.Add("F07", typeof(long));
            CostingDT.Columns.Add("F08", typeof(long));
            CostingDT.Columns.Add("F09", typeof(long));
            CostingDT.Columns.Add("F10", typeof(long));
            CostingDT.Columns.Add("F11", typeof(long));
            CostingDT.Columns.Add("F12", typeof(long));
            CostingDT.Columns.Add("F13", typeof(long));
            CostingDT.Columns.Add("F14", typeof(long));
            CostingDT.Columns.Add("F15", typeof(long));
            CostingDT.Columns.Add("F16", typeof(long));
            CostingDT.Columns.Add("F17", typeof(long));
            CostingDT.Columns.Add("F18", typeof(long));
            CostingDT.Columns.Add("F19", typeof(long));
            CostingDT.Columns.Add("F20", typeof(long));
            CostingDT.Columns.Add("F21", typeof(long));
            CostingDT.Columns.Add("F22", typeof(long));
            CostingDT.Columns.Add("F23", typeof(long));
            CostingDT.Columns.Add("F24", typeof(string));
            CostingDT.Columns.Add("F25", typeof(string));
            CostingDT.Columns.Add("F26", typeof(string));
            CostingDT.Columns.Add("F27", typeof(string));
            CostingDT.Columns.Add("F28", typeof(string));
            CostingDT.Columns.Add("F29", typeof(string));
            CostingDT.Columns.Add("F30", typeof(string));
            CostingDT.Columns.Add("F31", typeof(string));
            CostingDT.Columns.Add("F32", typeof(string));
            CostingDT.Columns.Add("F33", typeof(string));
            CostingDT.Columns.Add("F34", typeof(string));
            CostingDT.Columns.Add("F35", typeof(string));
            CostingDT.Columns.Add("F36", typeof(string));
            CostingDT.Columns.Add("F37", typeof(string));
            CostingDT.Columns.Add("F38", typeof(string));
            CostingDT.Columns.Add("F39", typeof(decimal));
            CostingDT.Columns.Add("F40", typeof(decimal));
            CostingDT.Columns.Add("F41", typeof(decimal));
            CostingDT.Columns.Add("F42", typeof(decimal));
            CostingDT.Columns.Add("F43", typeof(decimal));
            CostingDT.Columns.Add("F44", typeof(decimal));
            CostingDT.Columns.Add("F45", typeof(decimal));
            CostingDT.Columns.Add("F46", typeof(decimal));
            CostingDT.Columns.Add("F47", typeof(DateTime));
            CostingDT.Columns.Add("F48", typeof(DateTime));

            CostingDT.Rows.Add(
            costdto.F01, costdto.F02, costdto.F03, costdto.F04, costdto.F05, costdto.F06, costdto.F07, costdto.F08, costdto.F09, costdto.F10,
            costdto.F11, costdto.F12, costdto.F13, costdto.F14, costdto.F15, costdto.F16, costdto.F17, costdto.F18, costdto.F19, costdto.F20,
            costdto.F21, costdto.F22, costdto.F23, costdto.F24, costdto.F25, costdto.F26, costdto.F27, costdto.F28, costdto.F29, costdto.F30,
            costdto.F31, costdto.F32, costdto.F33, costdto.F34, costdto.F35, costdto.F36, costdto.F37, costdto.F38, costdto.F39, costdto.F40,
            costdto.F41, costdto.F42, costdto.F43, costdto.F44, costdto.F45, costdto.F46, costdto.F47, costdto.F48
            );
            para.Add("UDT", CostingDT.AsTableValuedParameter("UDT_Costing"));

            CostingList = await DbConnection.QueryAsync<CostSheetDto>("SP_Costing", para
            , commandType: CommandType.StoredProcedure);

            return CostingList;
        }

        public async Task<ReturnDto> SaveCostingData(List<SaveCostSheetDto> costdto)

        {
            DataTable CostingDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            CostingDT.Columns.Add("F01", typeof(long));
            CostingDT.Columns.Add("F02", typeof(long));
            CostingDT.Columns.Add("F03", typeof(long));
            CostingDT.Columns.Add("F04", typeof(long));
            CostingDT.Columns.Add("F05", typeof(long));
            CostingDT.Columns.Add("F06", typeof(long));
            CostingDT.Columns.Add("F07", typeof(long));
            CostingDT.Columns.Add("F08", typeof(long));
            CostingDT.Columns.Add("F09", typeof(long));
            CostingDT.Columns.Add("F10", typeof(long));
            CostingDT.Columns.Add("F11", typeof(long));
            CostingDT.Columns.Add("F12", typeof(long));
            CostingDT.Columns.Add("F13", typeof(long));
            CostingDT.Columns.Add("F14", typeof(long));
            CostingDT.Columns.Add("F15", typeof(long));
            CostingDT.Columns.Add("F16", typeof(long));
            CostingDT.Columns.Add("F17", typeof(long));
            CostingDT.Columns.Add("F18", typeof(long));
            CostingDT.Columns.Add("F19", typeof(long));
            CostingDT.Columns.Add("F20", typeof(long));
            CostingDT.Columns.Add("F21", typeof(long));
            CostingDT.Columns.Add("F22", typeof(long));
            CostingDT.Columns.Add("F23", typeof(long));
            CostingDT.Columns.Add("F24", typeof(string));
            CostingDT.Columns.Add("F25", typeof(string));
            CostingDT.Columns.Add("F26", typeof(string));
            CostingDT.Columns.Add("F27", typeof(string));
            CostingDT.Columns.Add("F28", typeof(string));
            CostingDT.Columns.Add("F29", typeof(string));
            CostingDT.Columns.Add("F30", typeof(string));
            CostingDT.Columns.Add("F31", typeof(string));
            CostingDT.Columns.Add("F32", typeof(string));
            CostingDT.Columns.Add("F33", typeof(string));
            CostingDT.Columns.Add("F34", typeof(string));
            CostingDT.Columns.Add("F35", typeof(string));
            CostingDT.Columns.Add("F36", typeof(string));
            CostingDT.Columns.Add("F37", typeof(string));
            CostingDT.Columns.Add("F38", typeof(string));
            CostingDT.Columns.Add("F39", typeof(decimal));
            CostingDT.Columns.Add("F40", typeof(decimal));
            CostingDT.Columns.Add("F41", typeof(decimal));
            CostingDT.Columns.Add("F42", typeof(decimal));
            CostingDT.Columns.Add("F43", typeof(decimal));
            CostingDT.Columns.Add("F44", typeof(decimal));
            CostingDT.Columns.Add("F45", typeof(decimal));
            CostingDT.Columns.Add("F46", typeof(decimal));
            CostingDT.Columns.Add("F47", typeof(DateTime));
            CostingDT.Columns.Add("F48", typeof(DateTime));

            foreach (var item in costdto)
            {
                var ActivityCode = item.Action;
                var ModuleId = item.ModuleId;
                var LocationId = item.LocationId;
                var UserId = item.UserId;

                if (item.sCostSheetHeader != null)
                {
                    CostingDT.Rows.Add(
                        ActivityCode,//1
                        ModuleId,//2
                        LocationId,//3
                        UserId,//4
                        item.sCostSheetHeader.AutoId,//5
                        0,//6
                        item.sCostSheetHeader.RHId,//7
                        item.sCostSheetHeader.SOId,//8
                        item.sCostSheetHeader.ArticleId,//9
                        0,//10
                        item.sCostSheetHeader.BrandId,//11
                        item.sCostSheetHeader.Status,//12
                        item.sCostSheetHeader.RevisionNo,//13
                        0,//14
                        0,//15
                        0,//16
                        0,//17
                        0,//18
                        0,//19
                        0, 0,//20-21
                        1,//22
                        0,//23
                        item.sCostSheetHeader.CostRefNo,//24
                        item.sCostSheetHeader.Combination,//25
                        item.sCostSheetHeader.bActive,//26
                         0, 0, 0, 0, 0, 0, 0,//27-33
                        item.sCostSheetHeader.Remarks,//34
                        0,//35
                        0,//36
                        0, 0,//37 - 38
                        0,//39
                        0,//40
                        0, 0, 0, 0, 0, 0,//41-46
                        item.sCostSheetHeader.CostDate,//47 
                        item.sCostSheetHeader.TransDate//48 
                );
                }
                if (item.sCostSheetDetails != null)
                {
                    CostingDT.Rows.Add(
                        ActivityCode,//1
                        ModuleId,//2
                        LocationId,//3
                        UserId,//4
                        item.sCostSheetDetails.CHId,//5
                        item.sCostSheetDetails.AutoId,//6
                        0,//7
                        item.sCostSheetDetails.ArticleId,//8
                        0,//9
                        0,//10
                        item.sCostSheetDetails.ColorId,//11
                        item.sCostSheetDetails.StyleColorId,//12
                        item.sCostSheetDetails.SizeId,//13
                        item.sCostSheetDetails.StyleSizeId,//14
                        item.sCostSheetDetails.UOMId,//15
                        item.sCostSheetDetails.FluteTypeId,//16
                        item.sCostSheetDetails.BaseId,//17
                        item.sCostSheetDetails.SuppilerId,//18
                        0, 0, 0,//19-21
                        2,//22
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,//23-33
                        0,//34
                        0,//35
                        0,//36
                        0,//37
                        0,//38
                        item.sCostSheetDetails.Factor,//39
                        item.sCostSheetDetails.Wastage,//40
                        item.sCostSheetDetails.ConsQty,//41
                        item.sCostSheetDetails.Cost,//42
                        item.sCostSheetDetails.Value,//43
                        0, 0, 0 //44-48
                    );
                }


            }

            para.Add("UDT", CostingDT.AsTableValuedParameter("UDT_Costing"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("SP_Costing", para,
            commandType: CommandType.StoredProcedure);

            return result;
        }

        #endregion "Washing Costing"

        #region "ProductionIssueTo"
        public async Task<IEnumerable<ProductionDto>> GetProductionIssueToData(ProductionDto productdto)
        {
            DataTable ProductDT = new DataTable();
            IEnumerable<ProductionDto> ProductionList;

            DynamicParameters para = new DynamicParameters();

            ProductDT.Columns.Add("F01", typeof(long));
            ProductDT.Columns.Add("F02", typeof(long));
            ProductDT.Columns.Add("F03", typeof(long));
            ProductDT.Columns.Add("F04", typeof(long));
            ProductDT.Columns.Add("F05", typeof(long));
            ProductDT.Columns.Add("F06", typeof(long));
            ProductDT.Columns.Add("F07", typeof(long));
            ProductDT.Columns.Add("F08", typeof(long));
            ProductDT.Columns.Add("F09", typeof(long));
            ProductDT.Columns.Add("F10", typeof(long));
            ProductDT.Columns.Add("F11", typeof(long));
            ProductDT.Columns.Add("F12", typeof(long));
            ProductDT.Columns.Add("F13", typeof(long));
            ProductDT.Columns.Add("F14", typeof(long));
            ProductDT.Columns.Add("F15", typeof(long));
            ProductDT.Columns.Add("F16", typeof(string));
            ProductDT.Columns.Add("F17", typeof(string));
            ProductDT.Columns.Add("F18", typeof(string));
            ProductDT.Columns.Add("F19", typeof(string));
            ProductDT.Columns.Add("F20", typeof(string));
            ProductDT.Columns.Add("F21", typeof(string));
            ProductDT.Columns.Add("F22", typeof(string));
            ProductDT.Columns.Add("F23", typeof(string));
            ProductDT.Columns.Add("F24", typeof(string));
            ProductDT.Columns.Add("F25", typeof(string));
            ProductDT.Columns.Add("F26", typeof(string));
            ProductDT.Columns.Add("F27", typeof(DateTime));

            ProductDT.Rows.Add(
            productdto.F01, productdto.F02, productdto.F03, productdto.F04, productdto.F05, productdto.F06, productdto.F07, productdto.F08, productdto.F09, productdto.F10,
            productdto.F11, productdto.F12, productdto.F13, productdto.F14, productdto.F15, productdto.F16, productdto.F17, productdto.F18, productdto.F19, productdto.F20,
            productdto.F21, productdto.F22, productdto.F23, productdto.F24, productdto.F25, productdto.F26, productdto.F27
            );
            para.Add("UDT", ProductDT.AsTableValuedParameter("UDT_Prduction"));

            ProductionList = await DbConnection.QueryAsync<ProductionDto>("SP_IssueToProduction", para
            , commandType: CommandType.StoredProcedure);

            return ProductionList;
        }
        public async Task<ReturnDto> SaveProductionIssueToData(SaveProductionDto productdto)
        {
            DataTable ProductDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            ProductDT.Columns.Add("F01", typeof(long));
            ProductDT.Columns.Add("F02", typeof(long));
            ProductDT.Columns.Add("F03", typeof(long));
            ProductDT.Columns.Add("F04", typeof(long));
            ProductDT.Columns.Add("F05", typeof(long));
            ProductDT.Columns.Add("F06", typeof(long));
            ProductDT.Columns.Add("F07", typeof(long));
            ProductDT.Columns.Add("F08", typeof(long));
            ProductDT.Columns.Add("F09", typeof(long));
            ProductDT.Columns.Add("F10", typeof(long));
            ProductDT.Columns.Add("F11", typeof(long));
            ProductDT.Columns.Add("F12", typeof(long));
            ProductDT.Columns.Add("F13", typeof(long));
            ProductDT.Columns.Add("F14", typeof(long));
            ProductDT.Columns.Add("F15", typeof(long));
            ProductDT.Columns.Add("F16", typeof(string));
            ProductDT.Columns.Add("F17", typeof(string));
            ProductDT.Columns.Add("F18", typeof(string));
            ProductDT.Columns.Add("F19", typeof(string));
            ProductDT.Columns.Add("F20", typeof(string));
            ProductDT.Columns.Add("F21", typeof(string));
            ProductDT.Columns.Add("F22", typeof(string));
            ProductDT.Columns.Add("F23", typeof(string));
            ProductDT.Columns.Add("F24", typeof(string));
            ProductDT.Columns.Add("F25", typeof(string));
            ProductDT.Columns.Add("F26", typeof(string));
            ProductDT.Columns.Add("F27", typeof(DateTime));


            var ActivityCode = productdto.Action;
            var ModuleId = productdto.ModuleId;
            var LocationId = productdto.LocationId;
            var UserId = productdto.UserId;
            var RouteCardId = productdto.RouteCardId;
            if (productdto.sSIssueToProdcutionHeader != null)
            {
                ProductDT.Rows.Add(
                    ActivityCode,//1
                    ModuleId,//2
                    LocationId,//3
                    UserId,//4
                    productdto.sSIssueToProdcutionHeader.ITPHId,//5
                    RouteCardId,//6
                    0,//7
                    0,//8
                    productdto.sSIssueToProdcutionHeader.ArticleId,//9
                    productdto.sSIssueToProdcutionHeader.RHId,//10
                    productdto.sSIssueToProdcutionHeader.CustomerId,//11
                    0,//12
                    0,//13
                    0,//14
                    0,//15
                    productdto.sSIssueToProdcutionHeader.DocNo,//16
                    productdto.sSIssueToProdcutionHeader.Remarks,//17
                    productdto.sSIssueToProdcutionHeader.IssuedEpf,//18
                    productdto.sSIssueToProdcutionHeader.ReceivedEpf,//19
                    productdto.sSIssueToProdcutionHeader.LotWeight,//20 
                    0,//21
                    0,//22
                    0,//23
                    "H",//24
                    0,//25
                    0,//26
                    productdto.sSIssueToProdcutionHeader.TransDate.Date //27
                );
            }
            if (productdto.sIssueToProdcutionDetails != null)
            {
                foreach (var item in productdto.sIssueToProdcutionDetails)
                {
                    ProductDT.Rows.Add(
                        ActivityCode,//1
                        0,//2
                        0,//3
                        0,//4
                        0,//5
                        item.SOHIdx,//6
                        item.SODIdx,//7
                        item.ArticleColorSizeId,//8
                        0,//9
                        item.ReceiveSiteId,//10
                        0,//11
                        item.OrderQty,//12
                        item.IssuedQty,//13
                        item.IssueQty,//14
                        0,//15
                        0,//16
                        0,//17
                        0,//18
                        0,//19
                        0,//20
                        0,//21
                        0,//22
                        0,//23
                        'D',//24
                        0,//25
                        0//26
                         //27
                    );
                }
            }


            para.Add("UDT", ProductDT.AsTableValuedParameter("UDT_Prduction"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("SP_IssueToProduction", para,
            commandType: CommandType.StoredProcedure);

            return result;
        }

        #endregion "ProductionIssueTo"

        #region "ProductionUpdate"
        public async Task<IEnumerable<ProductionDto>> GetProductionUpdateData(ProductionDto productdto)
        {
            DataTable ProductDT = new DataTable();
            IEnumerable<ProductionDto> ProductionList;

            DynamicParameters para = new DynamicParameters();

            ProductDT.Columns.Add("F01", typeof(long));
            ProductDT.Columns.Add("F02", typeof(long));
            ProductDT.Columns.Add("F03", typeof(long));
            ProductDT.Columns.Add("F04", typeof(long));
            ProductDT.Columns.Add("F05", typeof(long));
            ProductDT.Columns.Add("F06", typeof(long));
            ProductDT.Columns.Add("F07", typeof(long));
            ProductDT.Columns.Add("F08", typeof(long));
            ProductDT.Columns.Add("F09", typeof(long));
            ProductDT.Columns.Add("F10", typeof(long));
            ProductDT.Columns.Add("F11", typeof(long));
            ProductDT.Columns.Add("F12", typeof(long));
            ProductDT.Columns.Add("F13", typeof(long));
            ProductDT.Columns.Add("F14", typeof(long));
            ProductDT.Columns.Add("F15", typeof(long));
            ProductDT.Columns.Add("F16", typeof(string));
            ProductDT.Columns.Add("F17", typeof(string));
            ProductDT.Columns.Add("F18", typeof(string));
            ProductDT.Columns.Add("F19", typeof(string));
            ProductDT.Columns.Add("F20", typeof(string));
            ProductDT.Columns.Add("F21", typeof(string));
            ProductDT.Columns.Add("F22", typeof(string));
            ProductDT.Columns.Add("F23", typeof(string));
            ProductDT.Columns.Add("F24", typeof(string));
            ProductDT.Columns.Add("F25", typeof(string));
            ProductDT.Columns.Add("F26", typeof(string));
            ProductDT.Columns.Add("F27", typeof(DateTime));

            ProductDT.Rows.Add(
            productdto.F01, productdto.F02, productdto.F03, productdto.F04, productdto.F05, productdto.F06, productdto.F07, productdto.F08, productdto.F09, productdto.F10,
            productdto.F11, productdto.F12, productdto.F13, productdto.F14, productdto.F15, productdto.F16, productdto.F17, productdto.F18, productdto.F19, productdto.F20,
            productdto.F21, productdto.F22, productdto.F23, productdto.F24, productdto.F25, productdto.F26, productdto.F27
            );
            para.Add("UDT", ProductDT.AsTableValuedParameter("UDT_Prduction"));

            ProductionList = await DbConnection.QueryAsync<ProductionDto>("SP_Productionout", para
            , commandType: CommandType.StoredProcedure);

            return ProductionList;
        }
        public async Task<ReturnDto> SaveProductionUpdateData(SaveProductionDto productdto)
        {
            DataTable ProductDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            ProductDT.Columns.Add("F01", typeof(long));
            ProductDT.Columns.Add("F02", typeof(long));
            ProductDT.Columns.Add("F03", typeof(long));
            ProductDT.Columns.Add("F04", typeof(long));
            ProductDT.Columns.Add("F05", typeof(long));
            ProductDT.Columns.Add("F06", typeof(long));
            ProductDT.Columns.Add("F07", typeof(long));
            ProductDT.Columns.Add("F08", typeof(long));
            ProductDT.Columns.Add("F09", typeof(long));
            ProductDT.Columns.Add("F10", typeof(long));
            ProductDT.Columns.Add("F11", typeof(long));
            ProductDT.Columns.Add("F12", typeof(long));
            ProductDT.Columns.Add("F13", typeof(long));
            ProductDT.Columns.Add("F14", typeof(long));
            ProductDT.Columns.Add("F15", typeof(long));
            ProductDT.Columns.Add("F16", typeof(string));
            ProductDT.Columns.Add("F17", typeof(string));
            ProductDT.Columns.Add("F18", typeof(string));
            ProductDT.Columns.Add("F19", typeof(string));
            ProductDT.Columns.Add("F20", typeof(string));
            ProductDT.Columns.Add("F21", typeof(string));
            ProductDT.Columns.Add("F22", typeof(string));
            ProductDT.Columns.Add("F23", typeof(string));
            ProductDT.Columns.Add("F24", typeof(string));
            ProductDT.Columns.Add("F25", typeof(string));
            ProductDT.Columns.Add("F26", typeof(string));
            ProductDT.Columns.Add("F27", typeof(DateTime));


            var ActivityCode = productdto.Action;
            var ModuleId = productdto.ModuleId;
            var LocationId = productdto.LocationId;
            var UserId = productdto.UserId;
            if (productdto.sSIssueToProdcutionHeader != null)
            {
                ProductDT.Rows.Add(
                    ActivityCode,//1
                    ModuleId,//2
                    LocationId,//3
                    UserId,//4
                    productdto.sSIssueToProdcutionHeader.ITPHId,//5
                    0,//6
                    0,//7
                    0,//8
                    productdto.sSIssueToProdcutionHeader.ArticleId,//9
                    productdto.sSIssueToProdcutionHeader.RHId,//10
                    productdto.sSIssueToProdcutionHeader.CustomerId,//11
                    0,//12
                    0,//13
                    0,//14
                    0,//15
                    productdto.sSIssueToProdcutionHeader.DocNo,//16
                    productdto.sSIssueToProdcutionHeader.Remarks,//17
                    productdto.sSIssueToProdcutionHeader.IssuedEpf,//18
                    productdto.sSIssueToProdcutionHeader.ReceivedEpf,//19
                    productdto.sSIssueToProdcutionHeader.LotWeight,//20 
                    0,//21
                    0,//22
                    0,//23
                    "H",//24
                    0,//25
                    0,//26
                    productdto.sSIssueToProdcutionHeader.TransDate //27
                );
            }
            if (productdto.sIssueToProdcutionDetails != null)
            {
                foreach (var item in productdto.sIssueToProdcutionDetails)
                {
                    ProductDT.Rows.Add(
                        ActivityCode,//1
                        0,//2
                        0,//3
                        0,//4
                        item.ArticleColorSizeId,//5 PHIDx
                        0,//6 
                        item.SODIdx,//7 FPPOId
                        0,//8
                        0,//9
                        item.SOHIdx,//10 FPPODId
                        item.OrderQty,//11 Entered Qty
                        0,//12 
                        item.IssuedQty,//13 Time
                        UserId,//14 User
                        0,//15
                        0,//16
                        0,//17
                        0,//18
                        0,//19
                        0,//20
                        0,//21
                        0,//22
                        0,//23
                        "D",//24
                        0,//25
                        0//26
                         //27
                    );
                }
            }

            if (productdto.sProductionQCHeader != null)
            {
                ProductDT.Rows.Add(
                    ActivityCode,//1
                    ModuleId,//2
                    LocationId,//3
                    UserId,//4
                   productdto.sSIssueToProdcutionHeader.ITPHId,//5
                    0,//6
                    0,//7
                    0,//8
                    productdto.sSIssueToProdcutionHeader.ArticleId,//9
                    productdto.sSIssueToProdcutionHeader.RHId,//10
                    productdto.sSIssueToProdcutionHeader.CustomerId,//11
                    0,//12
                    0,//13
                    0,//14
                    0,//15
                    productdto.sSIssueToProdcutionHeader.DocNo,//16
                    productdto.sSIssueToProdcutionHeader.Remarks,//17
                    0,//18
                    0,//19
                    0,//20 
                    0,//21
                    0,//22
                    0,//23
                    "H",//24
                    0,//25
                    0,//26
                    productdto.sSIssueToProdcutionHeader.TransDate.Date //27
                );
            }

            if (productdto.sProductionQCDetails != null)
            {
                foreach (var item in productdto.sProductionQCDetails)
                {
                    ProductDT.Rows.Add(
                        ActivityCode,//1
                        0,//2
                        0,//3
                        0,//4
                        item.ArticleColorSizeId,//5 PHIDx
                        0,//6 
                        item.SODIdx,//7 FPPOId
                        0,//8
                        0,//9
                        item.SOHIdx,//10 FPPODId
                        item.OrderQty,//11 Entered Qty
                        0,//12 
                        item.IssuedQty,//13 Time
                        UserId,//14 User
                        0,//15
                        0,//16
                        0,//17
                        0,//18
                        0,//19
                        0,//20
                        0,//21
                        0,//22
                        0,//23
                        "D",//24
                        0,//25
                        0//26
                         //27
                    );
                }
            }

            para.Add("UDT", ProductDT.AsTableValuedParameter("UDT_Prduction"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("SP_Productionout", para,
            commandType: CommandType.StoredProcedure);

            return result;
        }
        public async Task<IEnumerable<ProductionOutDetailsDto>> GetProductionUpdateDetailData(ProductionOutDetailsDto productdto)
        {
            DataTable ProductDT = new DataTable();
            IEnumerable<ProductionOutDetailsDto> ProductionList;

            DynamicParameters para = new DynamicParameters();

            ProductDT.Columns.Add("d1", typeof(long));
            ProductDT.Columns.Add("d2", typeof(long));
            ProductDT.Columns.Add("d3", typeof(long));
            ProductDT.Columns.Add("d4", typeof(long));
            ProductDT.Columns.Add("d5", typeof(long));
            ProductDT.Columns.Add("d6", typeof(long));
            ProductDT.Columns.Add("d7", typeof(long));
            ProductDT.Columns.Add("d8", typeof(long));
            ProductDT.Columns.Add("d9", typeof(long));
            ProductDT.Columns.Add("d10", typeof(long));
            ProductDT.Columns.Add("d11", typeof(long));
            ProductDT.Columns.Add("d12", typeof(long));
            ProductDT.Columns.Add("d13", typeof(long));
            ProductDT.Columns.Add("d14", typeof(long));
            ProductDT.Columns.Add("d15", typeof(long));
            ProductDT.Columns.Add("d16", typeof(long));
            ProductDT.Columns.Add("d17", typeof(long));
            ProductDT.Columns.Add("d18", typeof(long));
            ProductDT.Columns.Add("d19", typeof(long));
            ProductDT.Columns.Add("d20", typeof(long));
            ProductDT.Columns.Add("d21", typeof(long));
            ProductDT.Columns.Add("d22", typeof(long));
            ProductDT.Columns.Add("d23", typeof(long));
            ProductDT.Columns.Add("d24", typeof(long));
            ProductDT.Columns.Add("d25", typeof(long));
            ProductDT.Columns.Add("d26", typeof(long));
            ProductDT.Columns.Add("d27", typeof(long));
            ProductDT.Columns.Add("f03", typeof(long));
            ProductDT.Columns.Add("f04", typeof(long));
            ProductDT.Columns.Add("f05", typeof(long));
            ProductDT.Columns.Add("F16", typeof(string));
            ProductDT.Columns.Add("F32", typeof(string));
            ProductDT.Columns.Add("F33", typeof(string));

            ProductDT.Rows.Add(
            productdto.d1, productdto.d2, productdto.d3, productdto.d4, productdto.d5, productdto.d6, productdto.d7, productdto.d8, productdto.d9, productdto.d10,
            productdto.d11, productdto.d12, productdto.d13, productdto.d14, productdto.d15, productdto.d16, productdto.d17, productdto.d18, productdto.d19, productdto.d20,
            productdto.d21, productdto.d22, productdto.d23, productdto.d24, productdto.d25, productdto.d26, productdto.d27, productdto.f03, productdto.f04, productdto.f05,
            productdto.F16, productdto.F32, productdto.F33
            );
            para.Add("UDT", ProductDT.AsTableValuedParameter("UDT_PrductionDetail"));

            ProductionList = await DbConnection.QueryAsync<ProductionOutDetailsDto>("SP_ProductionoutDetails", para
            , commandType: CommandType.StoredProcedure);

            return ProductionList;
        }
        #endregion "ProductionUpdate"

        #region "ProductionQcOut"
        public async Task<IEnumerable<ProductionDto>> GetProductionQcOutData(ProductionDto productdto)
        {
            DataTable ProductDT = new DataTable();
            IEnumerable<ProductionDto> ProductionList;

            DynamicParameters para = new DynamicParameters();

            ProductDT.Columns.Add("F01", typeof(long));
            ProductDT.Columns.Add("F02", typeof(long));
            ProductDT.Columns.Add("F03", typeof(long));
            ProductDT.Columns.Add("F04", typeof(long));
            ProductDT.Columns.Add("F05", typeof(long));
            ProductDT.Columns.Add("F06", typeof(long));
            ProductDT.Columns.Add("F07", typeof(long));
            ProductDT.Columns.Add("F08", typeof(long));
            ProductDT.Columns.Add("F09", typeof(long));
            ProductDT.Columns.Add("F10", typeof(long));
            ProductDT.Columns.Add("F11", typeof(long));
            ProductDT.Columns.Add("F12", typeof(long));
            ProductDT.Columns.Add("F13", typeof(long));
            ProductDT.Columns.Add("F14", typeof(long));
            ProductDT.Columns.Add("F15", typeof(long));
            ProductDT.Columns.Add("F16", typeof(string));
            ProductDT.Columns.Add("F17", typeof(string));
            ProductDT.Columns.Add("F18", typeof(string));
            ProductDT.Columns.Add("F19", typeof(string));
            ProductDT.Columns.Add("F20", typeof(string));
            ProductDT.Columns.Add("F21", typeof(string));
            ProductDT.Columns.Add("F22", typeof(string));
            ProductDT.Columns.Add("F23", typeof(string));
            ProductDT.Columns.Add("F24", typeof(string));
            ProductDT.Columns.Add("F25", typeof(string));
            ProductDT.Columns.Add("F26", typeof(string));
            ProductDT.Columns.Add("F27", typeof(DateTime));

            ProductDT.Rows.Add(
            productdto.F01, productdto.F02, productdto.F03, productdto.F04, productdto.F05, productdto.F06, productdto.F07, productdto.F08, productdto.F09, productdto.F10,
            productdto.F11, productdto.F12, productdto.F13, productdto.F14, productdto.F15, productdto.F16, productdto.F17, productdto.F18, productdto.F19, productdto.F20,
            productdto.F21, productdto.F22, productdto.F23, productdto.F24, productdto.F25, productdto.F26, productdto.F27
            );
            para.Add("UDT", ProductDT.AsTableValuedParameter("UDT_Prduction"));

            ProductionList = await DbConnection.QueryAsync<ProductionDto>("SP_ProductionQcOut", para
            , commandType: CommandType.StoredProcedure);

            return ProductionList;
        }
        public async Task<ReturnDto> SaveProductionQcOutData(SaveProductionDto productdto)
        {
            DataTable ProductDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            ProductDT.Columns.Add("F01", typeof(long));
            ProductDT.Columns.Add("F02", typeof(long));
            ProductDT.Columns.Add("F03", typeof(long));
            ProductDT.Columns.Add("F04", typeof(long));
            ProductDT.Columns.Add("F05", typeof(long));
            ProductDT.Columns.Add("F06", typeof(long));
            ProductDT.Columns.Add("F07", typeof(long));
            ProductDT.Columns.Add("F08", typeof(long));
            ProductDT.Columns.Add("F09", typeof(long));
            ProductDT.Columns.Add("F10", typeof(long));
            ProductDT.Columns.Add("F11", typeof(long));
            ProductDT.Columns.Add("F12", typeof(long));
            ProductDT.Columns.Add("F13", typeof(long));
            ProductDT.Columns.Add("F14", typeof(long));
            ProductDT.Columns.Add("F15", typeof(long));
            ProductDT.Columns.Add("F16", typeof(string));
            ProductDT.Columns.Add("F17", typeof(string));
            ProductDT.Columns.Add("F18", typeof(string));
            ProductDT.Columns.Add("F19", typeof(string));
            ProductDT.Columns.Add("F20", typeof(string));
            ProductDT.Columns.Add("F21", typeof(string));
            ProductDT.Columns.Add("F22", typeof(string));
            ProductDT.Columns.Add("F23", typeof(string));
            ProductDT.Columns.Add("F24", typeof(string));
            ProductDT.Columns.Add("F25", typeof(string));
            ProductDT.Columns.Add("F26", typeof(string));
            ProductDT.Columns.Add("F27", typeof(DateTime));


            var ActivityCode = productdto.Action;
            var ModuleId = productdto.ModuleId;
            var LocationId = productdto.LocationId;
            var UserId = productdto.UserId;
            var RouteCardId = productdto.RouteCardId;

            if (productdto.sSIssueToProdcutionHeader != null)
            {
                ProductDT.Rows.Add(
                    ActivityCode,//1
                    ModuleId,//2
                    LocationId,//3
                    UserId,//4
                    productdto.sSIssueToProdcutionHeader.ITPHId,//5
                    RouteCardId,//6
                    0,//7
                    0,//8
                    productdto.sSIssueToProdcutionHeader.ArticleId,//9
                    productdto.sSIssueToProdcutionHeader.RHId,//10
                    productdto.sSIssueToProdcutionHeader.CustomerId,//11
                    0,//12
                    0,//13
                    0,//14
                    0,//15
                    productdto.sSIssueToProdcutionHeader.DocNo,//16
                    productdto.sSIssueToProdcutionHeader.Remarks,//17
                    0,//18
                    0,//19
                    0,//20 
                    0,//21
                    0,//22
                    0,//23
                    "H",//24
                    0,//25
                    0,//26
                    productdto.sSIssueToProdcutionHeader.TransDate.Date //27
                );
            }
            if (productdto.sIssueToProdcutionDetails != null)
            {
                foreach (var item in productdto.sIssueToProdcutionDetails)
                {
                    ProductDT.Rows.Add(
                        ActivityCode,//1
                        0,//2
                        0,//3
                        0,//4
                        0,//5
                        item.SOHIdx,//6
                        item.SODIdx,//7
                        item.ArticleColorSizeId,//8 //fppoid
                        0,//9
                        0,//10
                        0,//11
                        item.OrderQty,//12
                        item.IssuedQty,//13
                        item.IssueQty,//14
                        0,//15
                        0,//16
                        0,//17
                        0,//18
                        0,//19
                        0,//20
                        0,//21
                        0,//22
                        0,//23
                        "D",//24
                        0,//25
                        0//26
                         //27

                    );
                }
            }


            para.Add("UDT", ProductDT.AsTableValuedParameter("UDT_Prduction"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("SP_ProductionQcOut", para,
            commandType: CommandType.StoredProcedure);

            return result;
        }
        #endregion "ProductionQcOut"

        #region "ProductionDispatch"
        public async Task<IEnumerable<ProductionDto>> GetProductionDispatchData(ProductionDto productdto)
        {
            DataTable ProductDT = new DataTable();
            IEnumerable<ProductionDto> ProductionList;

            DynamicParameters para = new DynamicParameters();

            ProductDT.Columns.Add("F01", typeof(long));
            ProductDT.Columns.Add("F02", typeof(long));
            ProductDT.Columns.Add("F03", typeof(long));
            ProductDT.Columns.Add("F04", typeof(long));
            ProductDT.Columns.Add("F05", typeof(long));
            ProductDT.Columns.Add("F06", typeof(long));
            ProductDT.Columns.Add("F07", typeof(long));
            ProductDT.Columns.Add("F08", typeof(long));
            ProductDT.Columns.Add("F09", typeof(long));
            ProductDT.Columns.Add("F10", typeof(long));
            ProductDT.Columns.Add("F11", typeof(long));
            ProductDT.Columns.Add("F12", typeof(long));
            ProductDT.Columns.Add("F13", typeof(long));
            ProductDT.Columns.Add("F14", typeof(long));
            ProductDT.Columns.Add("F15", typeof(long));
            ProductDT.Columns.Add("F16", typeof(string));
            ProductDT.Columns.Add("F17", typeof(string));
            ProductDT.Columns.Add("F18", typeof(string));
            ProductDT.Columns.Add("F19", typeof(string));
            ProductDT.Columns.Add("F20", typeof(string));
            ProductDT.Columns.Add("F21", typeof(string));
            ProductDT.Columns.Add("F22", typeof(string));
            ProductDT.Columns.Add("F23", typeof(string));
            ProductDT.Columns.Add("F24", typeof(string));
            ProductDT.Columns.Add("F25", typeof(string));
            ProductDT.Columns.Add("F26", typeof(string));
            ProductDT.Columns.Add("F27", typeof(DateTime));

            ProductDT.Rows.Add(
            productdto.F01, productdto.F02, productdto.F03, productdto.F04, productdto.F05, productdto.F06, productdto.F07, productdto.F08, productdto.F09, productdto.F10,
            productdto.F11, productdto.F12, productdto.F13, productdto.F14, productdto.F15, productdto.F16, productdto.F17, productdto.F18, productdto.F19, productdto.F20,
            productdto.F21, productdto.F22, productdto.F23, productdto.F24, productdto.F25, productdto.F26, productdto.F27
            );
            para.Add("UDT", ProductDT.AsTableValuedParameter("UDT_Prduction"));

            ProductionList = await DbConnection.QueryAsync<ProductionDto>("spTransDispatchGetData", para
            , commandType: CommandType.StoredProcedure);

            return ProductionList;
        }
        public async Task<ReturnDto> SaveProductionDispatchData(SaveDispatchDto productdto)
        {
            DataTable ProductDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            ProductDT.Columns.Add("F01", typeof(long));
            ProductDT.Columns.Add("F02", typeof(long));
            ProductDT.Columns.Add("F03", typeof(long));
            ProductDT.Columns.Add("F04", typeof(long));
            ProductDT.Columns.Add("F05", typeof(long));
            ProductDT.Columns.Add("F06", typeof(long));
            ProductDT.Columns.Add("F07", typeof(long));
            ProductDT.Columns.Add("F08", typeof(long));
            ProductDT.Columns.Add("F09", typeof(long));
            ProductDT.Columns.Add("F10", typeof(long));
            ProductDT.Columns.Add("F11", typeof(long));
            ProductDT.Columns.Add("F12", typeof(long));
            ProductDT.Columns.Add("F13", typeof(long));
            ProductDT.Columns.Add("F14", typeof(long));
            ProductDT.Columns.Add("F15", typeof(long));
            ProductDT.Columns.Add("F16", typeof(string));
            ProductDT.Columns.Add("F17", typeof(string));
            ProductDT.Columns.Add("F18", typeof(string));
            ProductDT.Columns.Add("F19", typeof(string));
            ProductDT.Columns.Add("F20", typeof(string));
            ProductDT.Columns.Add("F21", typeof(string));
            ProductDT.Columns.Add("F22", typeof(string));
            ProductDT.Columns.Add("F23", typeof(string));
            ProductDT.Columns.Add("F24", typeof(string));
            ProductDT.Columns.Add("F25", typeof(string));
            ProductDT.Columns.Add("F26", typeof(string));
            ProductDT.Columns.Add("F27", typeof(DateTime));

            var ActivityCode = productdto.Action;
            var ModuleId = productdto.ModuleId;
            var LocationId = productdto.LocationId;
            var UserId = productdto.UserId;

            if (productdto.sDispatchHeader != null)
            {
                ProductDT.Rows.Add(
                    ActivityCode,//1
                    ModuleId,//2
                    LocationId,//3
                    UserId,//4
                    productdto.sDispatchHeader.CustomerId,//5
                    productdto.sDispatchHeader.CusLocationId,//6
                    productdto.sDispatchHeader.DispatchSiteId,//7
                    productdto.sDispatchHeader.DispatchType,//8
                    0,//9
                    0,//10
                    1,//11
                    0,//12
                    0,//13
                    0,//14
                    0,//15
                    productdto.sDispatchHeader.Reason,//16
                    productdto.sDispatchHeader.VehicleNo,//17
                    0,//18
                    0,//19
                    0,//20 
                    0,//21
                    0,//22
                    0,//23
                    "H",//24
                    0,//25
                    0//26
                     //27
                );
            }
            if (productdto.sDispatchDetails != null)
            {
                foreach (var item in productdto.sDispatchDetails)
                {
                    ProductDT.Rows.Add(
                        ActivityCode,//1
                        0,//2
                        item.SOItemId,//3
                        item.SODelivDtId,//4
                        item.ProducedQty,//5
                        item.DispatchedQty,//6
                        item.BalDispatchQty,//7
                        0,//8 //
                        0,//9
                        0,//10
                        2,//11
                        0,//12
                        0,//13
                        0,//14
                        0,//15
                        0,//16
                        0,//17
                        0,//18
                        0,//19
                        0,//20
                        0,//21
                        0,//22
                        0,//23
                        0,//24
                        0,//25
                        0//26
                         //27
                    );
                }
            }

            if (productdto.sDispatchAdditionalData != null)
            {
                ProductDT.Rows.Add(
                    ActivityCode,//1
                    ModuleId,//2
                    LocationId,//3
                    UserId,//4
                    productdto.sDispatchAdditionalData.GTPassWshCatId,//5
                    productdto.sDispatchAdditionalData.DHId,//6
                    0,//7
                    0,//8
                    0,//9
                    0,//10
                    3,//11
                    0,//12
                    0,//13
                    0,//14
                    0,//15
                    productdto.sDispatchAdditionalData.ManualDspNo,//16
                    productdto.sDispatchAdditionalData.NoOfBags,//17
                    productdto.sDispatchAdditionalData.CountedBy,//18
                    productdto.sDispatchAdditionalData.MemoNo,//19
                    productdto.sDispatchAdditionalData.FWDRName,//20 
                     productdto.sDispatchAdditionalData.Comments,//21
                    0,//22
                    0,//23
                    "H",//24
                    0,//25
                    0//26
                     //27
                );
            }

            para.Add("UDT", ProductDT.AsTableValuedParameter("UDT_Prduction"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransDispatchGetData", para,
            commandType: CommandType.StoredProcedure);

            return result;
        }
        #endregion "ProductionDispatch"

        #region "POAssociation - EPS Order Creation"
        public async Task<IEnumerable<POAssociationDto>> GetPOAssociationData(POAssociationDto poassociationDto)
        {
            DataTable POAssociationDT = new DataTable();
            IEnumerable<POAssociationDto> PoassociationList;

            DynamicParameters para = new DynamicParameters();



            POAssociationDT.Columns.Add("ActivityNo", typeof(Int64));
            POAssociationDT.Columns.Add("ModuleNo", typeof(Int64));
            POAssociationDT.Columns.Add("CompanyNo", typeof(Int64));
            POAssociationDT.Columns.Add("LocationNo", typeof(Int64));
            POAssociationDT.Columns.Add("AgentNo", typeof(Int64));
            POAssociationDT.Columns.Add("bActive", typeof(Int64));
            POAssociationDT.Columns.Add("F01", typeof(long));
            POAssociationDT.Columns.Add("F02", typeof(long));
            POAssociationDT.Columns.Add("F03", typeof(long));
            POAssociationDT.Columns.Add("F04", typeof(long));
            POAssociationDT.Columns.Add("F05", typeof(long));
            POAssociationDT.Columns.Add("F06", typeof(long));
            POAssociationDT.Columns.Add("F07", typeof(long));
            POAssociationDT.Columns.Add("F08", typeof(long));
            POAssociationDT.Columns.Add("F09", typeof(long));
            POAssociationDT.Columns.Add("F10", typeof(long));
            POAssociationDT.Columns.Add("F11", typeof(long));
            POAssociationDT.Columns.Add("F12", typeof(long));
            POAssociationDT.Columns.Add("F13", typeof(long));
            POAssociationDT.Columns.Add("F14", typeof(long));
            POAssociationDT.Columns.Add("F15", typeof(decimal));
            POAssociationDT.Columns.Add("F16", typeof(string));
            POAssociationDT.Columns.Add("F17", typeof(string));
            POAssociationDT.Columns.Add("F18", typeof(string));
            POAssociationDT.Columns.Add("F19", typeof(string));
            POAssociationDT.Columns.Add("F20", typeof(string));
            POAssociationDT.Columns.Add("F21", typeof(string));
            POAssociationDT.Columns.Add("F22", typeof(string));
            POAssociationDT.Columns.Add("F23", typeof(string));
            POAssociationDT.Columns.Add("F24", typeof(string));
            POAssociationDT.Columns.Add("F25", typeof(string));

            POAssociationDT.Rows.Add(
            poassociationDto.ActivityNo, poassociationDto.ModuleNo, poassociationDto.CompanyNo, poassociationDto.LocationNo, poassociationDto.AgentNo, poassociationDto.bActive,
            poassociationDto.F01, poassociationDto.F02, poassociationDto.F03, poassociationDto.F04, poassociationDto.F05, poassociationDto.F06, poassociationDto.F07, poassociationDto.F08, poassociationDto.F09, poassociationDto.F10,
            poassociationDto.F11, poassociationDto.F12, poassociationDto.F13, poassociationDto.F14, poassociationDto.F15, poassociationDto.F16, poassociationDto.F17, poassociationDto.F18, poassociationDto.F19, poassociationDto.F20,
            poassociationDto.F21, poassociationDto.F22, poassociationDto.F23, poassociationDto.F24, poassociationDto.F25
            );
            para.Add("UDT", POAssociationDT.AsTableValuedParameter("udt_OrderCreationData"));

            PoassociationList = await DbConnection.QueryAsync<POAssociationDto>("sp_OrderCreationData", para
            , commandType: CommandType.StoredProcedure);

            return PoassociationList;
        }
        public async Task<ReturnDto> SavePOAssociationDataAsync(SavePOAssociationDto savePOAssociationList)

        {
            DataTable POAssociationDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            POAssociationDT.Columns.Add("ActivityNo", typeof(Int64));
            POAssociationDT.Columns.Add("ModuleNo", typeof(Int64));
            POAssociationDT.Columns.Add("CompanyNo", typeof(Int64));
            POAssociationDT.Columns.Add("LocationNo", typeof(Int64));
            POAssociationDT.Columns.Add("AgentNo", typeof(Int64));
            POAssociationDT.Columns.Add("bActive", typeof(Int64));
            POAssociationDT.Columns.Add("F01", typeof(Int64));
            POAssociationDT.Columns.Add("F02", typeof(Int64));
            POAssociationDT.Columns.Add("F03", typeof(Int64));
            POAssociationDT.Columns.Add("F04", typeof(Int64));
            POAssociationDT.Columns.Add("F05", typeof(Int64));
            POAssociationDT.Columns.Add("F06", typeof(Int64));
            POAssociationDT.Columns.Add("F07", typeof(Int64));
            POAssociationDT.Columns.Add("F08", typeof(Int64));
            POAssociationDT.Columns.Add("F09", typeof(Int64));
            POAssociationDT.Columns.Add("F10", typeof(Int64));
            POAssociationDT.Columns.Add("F11", typeof(Int64));
            POAssociationDT.Columns.Add("F12", typeof(Int64));
            POAssociationDT.Columns.Add("F13", typeof(Int64));
            POAssociationDT.Columns.Add("F14", typeof(decimal));
            POAssociationDT.Columns.Add("F15", typeof(decimal));
            POAssociationDT.Columns.Add("F16", typeof(string));
            POAssociationDT.Columns.Add("F17", typeof(string));
            POAssociationDT.Columns.Add("F18", typeof(string));
            POAssociationDT.Columns.Add("F19", typeof(string));
            POAssociationDT.Columns.Add("F20", typeof(string));
            POAssociationDT.Columns.Add("F21", typeof(string));
            POAssociationDT.Columns.Add("F22", typeof(string));
            POAssociationDT.Columns.Add("F23", typeof(DateTime));
            POAssociationDT.Columns.Add("F24", typeof(DateTime));
            POAssociationDT.Columns.Add("F25", typeof(DateTime));

            var ActivityNo = savePOAssociationList.ActivityNo;
            var ModuleNo = savePOAssociationList.ModuleNo;
            var CompanyNo = savePOAssociationList.CompanyNo;
            var LocationNo = savePOAssociationList.LocationNo;
            var AgentNo = savePOAssociationList.AgentNo;
            var bActive = savePOAssociationList.bActive;

            if (savePOAssociationList.sOCHeader != null)
            {
                POAssociationDT.Rows.Add(
                    ActivityNo,
                    ModuleNo,
                    CompanyNo,
                    LocationNo,
                    AgentNo,
                    bActive,
                    savePOAssociationList.sOCHeader.AutoId,//1
                    savePOAssociationList.sOCHeader.CustomerId,//2
                    savePOAssociationList.sOCHeader.ArticleId,//3
                    0,//4
                    0,//5
                    0,//6
                    0,//7
                    0,//8
                    0,//9
                    0,//10
                    0,//11
                    0,//12
                    0,//13
                    0,//14
                    0,//15 
                    savePOAssociationList.sOCHeader.OCNo,//16
                    savePOAssociationList.sOCHeader.Remaks,//17
                    0,//18
                    0,//19
                    0,//20
                    0,//21
                    0//22

                );

            }

            if (savePOAssociationList.sSalesOrderHeader != null)
            {
                POAssociationDT.Rows.Add(
                    ActivityNo,
                    ModuleNo,
                    CompanyNo,
                    LocationNo,
                    AgentNo,
                    bActive,
                    0,//1
                    0,//2
                    0,//3
                    savePOAssociationList.sSalesOrderHeader.AutoId,//4
                    savePOAssociationList.sSalesOrderHeader.OCHIdx,//5
                    0,//6
                    0,//7
                    0,//8
                    0,//9
                    0,//10
                    0,//11
                    0,//12
                    0,//13
                    0,//14
                    0,//15 
                    0,//16
                    0,//17
                    savePOAssociationList.sSalesOrderHeader.PoNo,//18
                    0,//19
                    0,//20
                    0,//21
                    "H",//22
                    savePOAssociationList.sSalesOrderHeader.BuyerDelDate//23
                );

            }

            if (savePOAssociationList.sSalesOrderDeatails != null)
            {
                POAssociationDT.Rows.Add(
                    ActivityNo,
                    ModuleNo,
                    CompanyNo,
                    LocationNo,
                    AgentNo,
                    bActive,
                    0,//1
                    0,//2
                    0,//3
                    0,//4
                    0,//5
                    savePOAssociationList.sSalesOrderDeatails.AutoId,//6
                    savePOAssociationList.sSalesOrderDeatails.SOHId,//7
                    savePOAssociationList.sSalesOrderDeatails.MISPId,//8
                    savePOAssociationList.sSalesOrderDeatails.MSId,//9
                    savePOAssociationList.sSalesOrderDeatails.MPId,//10
                    savePOAssociationList.sSalesOrderDeatails.OrderQty,//11
                    savePOAssociationList.sSalesOrderDeatails.ProductionOutQty,//12
                    savePOAssociationList.sSalesOrderDeatails.DamageQty,//13
                    savePOAssociationList.sSalesOrderDeatails.Price,//14
                    0,//15 
                    0,//16
                    0,//17
                    0,//18
                    0,//19
                    0,//20
                    0,//21
                    "D",//22
                    0//23
                );

            }

            para.Add("UDT", POAssociationDT.AsTableValuedParameter("udt_OrderCreationData"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("sp_OrderCreationData", para,
            commandType: CommandType.StoredProcedure);

            return result;
        }
        #endregion "POAssociation"

        #region "GRNData"

        public async Task<IEnumerable<GRNDto>> GetGRNData(GRNDto wsdt)
        {
            DataTable MWSMaterDT = new DataTable();
            IEnumerable<GRNDto> MWSMaterList;

            DynamicParameters para = new DynamicParameters();

            MWSMaterDT.Columns.Add("ActivityNo", typeof(Int64));
            MWSMaterDT.Columns.Add("ModuleNo", typeof(Int64));
            MWSMaterDT.Columns.Add("CompanyNo", typeof(Int64));
            MWSMaterDT.Columns.Add("LocationNo", typeof(Int64));
            MWSMaterDT.Columns.Add("AgentNo", typeof(Int64));
            MWSMaterDT.Columns.Add("bActive", typeof(Int64));
            MWSMaterDT.Columns.Add("F01", typeof(Int64));
            MWSMaterDT.Columns.Add("F02", typeof(Int64));
            MWSMaterDT.Columns.Add("F03", typeof(Int64));
            MWSMaterDT.Columns.Add("F04", typeof(Int64));
            MWSMaterDT.Columns.Add("F05", typeof(Int64));
            MWSMaterDT.Columns.Add("F06", typeof(Int64));
            MWSMaterDT.Columns.Add("F07", typeof(Int64));
            MWSMaterDT.Columns.Add("F08", typeof(Int64));
            MWSMaterDT.Columns.Add("F09", typeof(decimal));
            MWSMaterDT.Columns.Add("F10", typeof(decimal));
            MWSMaterDT.Columns.Add("F11", typeof(decimal));
            MWSMaterDT.Columns.Add("F12", typeof(decimal));
            MWSMaterDT.Columns.Add("F13", typeof(decimal));
            MWSMaterDT.Columns.Add("F14", typeof(decimal));
            MWSMaterDT.Columns.Add("F15", typeof(decimal));
            MWSMaterDT.Columns.Add("F16", typeof(string));
            MWSMaterDT.Columns.Add("F17", typeof(string));
            MWSMaterDT.Columns.Add("F18", typeof(string));
            MWSMaterDT.Columns.Add("F19", typeof(string));
            MWSMaterDT.Columns.Add("F20", typeof(string));
            MWSMaterDT.Columns.Add("F21", typeof(string));
            MWSMaterDT.Columns.Add("F22", typeof(string));
            MWSMaterDT.Columns.Add("F23", typeof(DateTime));
            MWSMaterDT.Columns.Add("F24", typeof(DateTime));
            MWSMaterDT.Columns.Add("F25", typeof(DateTime));


            MWSMaterDT.Rows.Add(
                      wsdt.ActivityNo,
                      wsdt.ModuleNo,
                      wsdt.CompanyNo,
                      wsdt.LocationNo,
                      wsdt.AgentNo,
                      wsdt.bActive,
                      wsdt.F01,
                      wsdt.F02,
                      wsdt.F03,
                      wsdt.F04,
                      wsdt.F05,
                      wsdt.F06,
                      wsdt.F07,
                      wsdt.F08,
                      wsdt.F09,
                      wsdt.F10,
                      wsdt.F11,
                      wsdt.F12,
                      wsdt.F13,
                      wsdt.F14,
                      wsdt.F15,
                      wsdt.F16,
                      wsdt.F17,
                      wsdt.F18,
                      wsdt.F19,
                      wsdt.F20,
                      wsdt.F21,
                      wsdt.F22,
                      wsdt.F23,
                      wsdt.F24,
                      wsdt.F25
            );

            para.Add("UDT", MWSMaterDT.AsTableValuedParameter("udt_GRNData"));

            MWSMaterList = await DbConnection.QueryAsync<GRNDto>("sp_GRNData", para
                , commandType: CommandType.StoredProcedure);

            return MWSMaterList;
        }

        public async Task<ReturnDto> SaveGRNDATA(SaveGRNDto wsDt)

        {
            DataTable GRNHeader = new DataTable();
            DynamicParameters para = new DynamicParameters();

            GRNHeader.Columns.Add("ActivityNo", typeof(Int64));
            GRNHeader.Columns.Add("ModuleNo", typeof(Int64));
            GRNHeader.Columns.Add("CompanyNo", typeof(Int64));
            GRNHeader.Columns.Add("LocationNo", typeof(Int64));
            GRNHeader.Columns.Add("AgentNo", typeof(Int64));
            GRNHeader.Columns.Add("bActive", typeof(Int64));
            GRNHeader.Columns.Add("F01", typeof(Int64));
            GRNHeader.Columns.Add("F02", typeof(Int64));
            GRNHeader.Columns.Add("F03", typeof(Int64));
            GRNHeader.Columns.Add("F04", typeof(Int64));
            GRNHeader.Columns.Add("F05", typeof(Int64));
            GRNHeader.Columns.Add("F06", typeof(Int64));
            GRNHeader.Columns.Add("F07", typeof(Int64));
            GRNHeader.Columns.Add("F08", typeof(Int64));
            GRNHeader.Columns.Add("F09", typeof(decimal));
            GRNHeader.Columns.Add("F10", typeof(decimal));
            GRNHeader.Columns.Add("F11", typeof(decimal));
            GRNHeader.Columns.Add("F12", typeof(decimal));
            GRNHeader.Columns.Add("F13", typeof(decimal));
            GRNHeader.Columns.Add("F14", typeof(decimal));
            GRNHeader.Columns.Add("F15", typeof(decimal));
            GRNHeader.Columns.Add("F16", typeof(string));
            GRNHeader.Columns.Add("F17", typeof(string));
            GRNHeader.Columns.Add("F18", typeof(string));
            GRNHeader.Columns.Add("F19", typeof(string));
            GRNHeader.Columns.Add("F20", typeof(string));
            GRNHeader.Columns.Add("F21", typeof(string));
            GRNHeader.Columns.Add("F22", typeof(string));
            GRNHeader.Columns.Add("F23", typeof(DateTime));
            GRNHeader.Columns.Add("F24", typeof(DateTime));
            GRNHeader.Columns.Add("F25", typeof(DateTime));

            var ActivityNo = wsDt.ActivityNo;
            var ModuleNo = wsDt.ModuleNo;
            var CompanyNo = wsDt.CompanyNo;
            var LocationNo = wsDt.LocationNo;
            var AgentNo = wsDt.AgentNo;
            var bActive = wsDt.bActive;

            if (wsDt.sGRNHeader != null)
            {
                GRNHeader.Rows.Add(
                  ActivityNo,
                  ModuleNo,
                  CompanyNo,
                  CompanyNo,
                  AgentNo,
                  bActive,
                  wsDt.sGRNHeader.AutoId,//1
                  wsDt.sGRNHeader.SupplierIdx,//2
                  wsDt.sGRNHeader.GRNTypeId,//3
                  0,//4
                  0,//5
                  0,//6
                  0,//7
                  1,//8
                  0,//9
                  0,//10
                  0,//11
                  0,//12
                  0,//13
                  0,//14
                  0,//15
                  0,//16,
                  wsDt.sGRNHeader.SupplierRef,//17,
                  0,//18,
                  0,//19
                  0,//20
                  0,//21
                  0,//22
                  wsDt.sGRNHeader.DocDate,//23
                  wsDt.sGRNHeader.Transdatetime//24
              );
            }
            if (wsDt.sGRNDetails != null)
            {
                foreach (var wsDtRow in wsDt.sGRNDetails)
                {
                    GRNHeader.Rows.Add(
                        ActivityNo,
                            ModuleNo,
                            CompanyNo,
                            LocationNo,
                            AgentNo,
                            bActive,
                            0,//1
                            0,//2
                            0,//3
                            wsDtRow.ItemMasterIdx,//4
                            0,//5
                            0,//6
                            0,//7
                            2,//8
                            wsDtRow.GRNQty,//9
                            wsDtRow.FreeQty,//10
                            wsDtRow.UnitRate,//11
                            wsDtRow.Value,//12
                            0,//13
                            0,//14
                            0,//15
                            0,//16,
                            0,//17,
                            0,//18,
                            0,//19
                            0,//20
                            0,//21
                            0//22
                     );
                }
            }

            para.Add("UDT", GRNHeader.AsTableValuedParameter("udt_GRNData"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("sp_GRNData", para,
            commandType: CommandType.StoredProcedure);

            return result;
        }
        #endregion "GRNData"


        
         #region "Production-Out"


     public async Task<IEnumerable<ProductionOutDto>> GetProductionOutData(ProductionOutDto poassociationDto)
        {
            DataTable POAssociationDT = new DataTable();
            IEnumerable<ProductionOutDto> PoassociationList;

            DynamicParameters para = new DynamicParameters();

            POAssociationDT.Columns.Add("ActivityNo", typeof(Int64));
            POAssociationDT.Columns.Add("ModuleNo", typeof(Int64));
            POAssociationDT.Columns.Add("CompanyNo", typeof(Int64));
            POAssociationDT.Columns.Add("LocationNo", typeof(Int64));
            POAssociationDT.Columns.Add("AgentNo", typeof(Int64));
            POAssociationDT.Columns.Add("bActive", typeof(Int64));
            POAssociationDT.Columns.Add("F01", typeof(long));
            POAssociationDT.Columns.Add("F02", typeof(long));
            POAssociationDT.Columns.Add("F03", typeof(long));
            POAssociationDT.Columns.Add("F04", typeof(long));
            POAssociationDT.Columns.Add("F05", typeof(long));
            POAssociationDT.Columns.Add("F06", typeof(long));
            POAssociationDT.Columns.Add("F07", typeof(long));
            POAssociationDT.Columns.Add("F08", typeof(long));
            POAssociationDT.Columns.Add("F09", typeof(long));
            POAssociationDT.Columns.Add("F10", typeof(long));
            POAssociationDT.Columns.Add("F11", typeof(decimal));
            POAssociationDT.Columns.Add("F12", typeof(decimal));
            POAssociationDT.Columns.Add("F13", typeof(decimal));
            POAssociationDT.Columns.Add("F14", typeof(decimal));
            POAssociationDT.Columns.Add("F15", typeof(string));
            POAssociationDT.Columns.Add("F16", typeof(string));
            POAssociationDT.Columns.Add("F17", typeof(string));
            POAssociationDT.Columns.Add("F18", typeof(string));
            POAssociationDT.Columns.Add("F19", typeof(string));
            POAssociationDT.Columns.Add("F20", typeof(string));


            POAssociationDT.Rows.Add(
            poassociationDto.ActivityNo, poassociationDto.ModuleNo, poassociationDto.CompanyNo, poassociationDto.LocationNo, poassociationDto.AgentNo, poassociationDto.bActive,
            poassociationDto.F01, poassociationDto.F02, poassociationDto.F03, poassociationDto.F04, poassociationDto.F05, poassociationDto.F06, poassociationDto.F07, poassociationDto.F08, poassociationDto.F09, poassociationDto.F10,
            poassociationDto.F11, poassociationDto.F12, poassociationDto.F13, poassociationDto.F14, poassociationDto.F15, poassociationDto.F16, poassociationDto.F17, poassociationDto.F18, poassociationDto.F19, poassociationDto.F20
        
            );
            para.Add("UDT", POAssociationDT.AsTableValuedParameter("udt_ProductionOutData"));

            PoassociationList = await DbConnection.QueryAsync<ProductionOutDto>("sp_ProductionoutData", para
            , commandType: CommandType.StoredProcedure);

            return PoassociationList;
        }

         public async Task<ReturnDto> SaveProductionOutData(List<SaveProductionOutDto> ocdto)

        {
            DataTable OrderCreationDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            OrderCreationDT.Columns.Add("ActivityNo", typeof(Int64));
            OrderCreationDT.Columns.Add("ModuleNo", typeof(Int64));
            OrderCreationDT.Columns.Add("CompanyNo", typeof(Int64));
            OrderCreationDT.Columns.Add("LocationNo", typeof(Int64));
            OrderCreationDT.Columns.Add("AgentNo", typeof(Int64));
            OrderCreationDT.Columns.Add("bActive", typeof(Int64));
            OrderCreationDT.Columns.Add("F01", typeof(Int64));
            OrderCreationDT.Columns.Add("F02", typeof(Int64));
            OrderCreationDT.Columns.Add("F03", typeof(Int64));
            OrderCreationDT.Columns.Add("F04", typeof(Int64));
            OrderCreationDT.Columns.Add("F05", typeof(Int64));
            OrderCreationDT.Columns.Add("F06", typeof(Int64));
            OrderCreationDT.Columns.Add("F07", typeof(Int64));
            OrderCreationDT.Columns.Add("F08", typeof(Int64));
            OrderCreationDT.Columns.Add("F09", typeof(Int64));
            OrderCreationDT.Columns.Add("F10", typeof(Int64));
            OrderCreationDT.Columns.Add("F11", typeof(decimal));
            OrderCreationDT.Columns.Add("F12", typeof(decimal));
            OrderCreationDT.Columns.Add("F13", typeof(decimal));
            OrderCreationDT.Columns.Add("F14", typeof(decimal));
            OrderCreationDT.Columns.Add("F15", typeof(string));
            OrderCreationDT.Columns.Add("F16", typeof(string));
            OrderCreationDT.Columns.Add("F17", typeof(string));
            OrderCreationDT.Columns.Add("F18", typeof(string));
            OrderCreationDT.Columns.Add("F19", typeof(string));
            OrderCreationDT.Columns.Add("F20", typeof(string));

            foreach (var item in ocdto)

            {

                var ActivityNo = item.ActivityNo;
                var ModuleNo = item.ModuleNo;
                var CompanyNo = item.CompanyNo;
                var LocationNo = item.LocationNo;
                var AgentNo = item.AgentNo;
                var bActive = item.bActive;

                if (item.sProductionoutHeader != null)
                {
                    OrderCreationDT.Rows.Add(
                    ActivityNo,
                    ModuleNo,
                    CompanyNo,
                    LocationNo,
                    AgentNo,
                    bActive,
                    item.sProductionoutHeader.AutoId,//1
                    item.sProductionoutHeader.ArticleId,//2
                    0,//3
                    0,//4
                    0,//5
                    0,//6
                    0,//7
                    0,//8
                    0,//9
                    0,//10
                    0,//11
                    0,//12
                    0,//13
                    0,//14
                    item.sProductionoutHeader.DocNo,//15 
                    0,//16
                    0,//17
                    0,//18
                    0,//19
                    0,//20
                    0,//21
                    "H",//22
                    0,//23
                    0,
                    0
                  );
                }

                if (item.sProductionoutDetails != null)
                {
                    OrderCreationDT.Rows.Add(
                    ActivityNo,
                    ModuleNo,
                    CompanyNo,
                    LocationNo,
                    AgentNo,
                    bActive,
                    item.sProductionoutDetails.POHId,//1
                    0,//2
                    item.sProductionoutDetails.AutoIdx,//3
                    0,//4
                    item.sProductionoutDetails.SODId,//5
                    item.sProductionoutDetails.OrderQty,//6
                    item.sProductionoutDetails.ProductionOutQty,//7
                    item.sProductionoutDetails.DamageQty,//8
                    0,//9
                    0,//10
                    0,//11
                    0,//12
                    0,//13
                    0,//14
                    0,//15 
                    0,//16
                    0,//17
                    0,//18
                    0,//19
                    0,//20
                    0,//21
                    "D"
                  );
                }

            }

            para.Add("UDT", OrderCreationDT.AsTableValuedParameter("udt_ProductionOutData"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("sp_ProductionoutData", para,
            commandType: CommandType.StoredProcedure);

            return result;
        }
         #endregion "Production-Out"

    }
}
