using API.Interfaces;
using API.Data;
using System.Threading.Tasks;
using API.Entities;
using Dapper;
using System.Data;
using API.DTOs;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System;

namespace API.Repository
{
    public class PurchasingRepository : DbConnCartonRepositoryBase, IPurchasingRepository
    {
        public PurchasingRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        public async Task<IEnumerable<PendIndentDto>> GetPurchaseOrderGetIndentAsync(IndentSearchDto searchDto)
        {
            IEnumerable<PendIndentDto> IntentHdList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CategoryId", searchDto.CategoryId);
            para.Add("AssignedTo", searchDto.AssignedTo);

            IntentHdList = await DbConnection.QueryAsync<PendIndentDto>("spTransPurchaseOrderGetIndent", para
                    , commandType: CommandType.StoredProcedure);

            return IntentHdList;
        }

        public async Task<ReturnDto> SavePurchaseOrderAsync(SavePurchaseOrderDto purchaseOrderDto)
        {
            DataTable PODetail = new DataTable();
            DataTable POAddChange = new DataTable();
            DynamicParameters para = new DynamicParameters();

            PODetail.Columns.Add("indentId", typeof(long));
            PODetail.Columns.Add("articleId", typeof(long));
            PODetail.Columns.Add("colorId", typeof(int));
            PODetail.Columns.Add("sizeId", typeof(int));
            PODetail.Columns.Add("openQty", typeof(int));
            PODetail.Columns.Add("orderQty", typeof(int));
            PODetail.Columns.Add("unitPrice", typeof(decimal));
            PODetail.Columns.Add("uomId", typeof(int));

            POAddChange.Columns.Add("addChargeId", typeof(int));
            POAddChange.Columns.Add("basis", typeof(int));
            POAddChange.Columns.Add("value", typeof(decimal));

            foreach (var item in purchaseOrderDto.PurchaseOrderDetails)
            {
                PODetail.Rows.Add(item.IndentId
                , item.ArticleId
                , item.ColorId
                , item.SizeId
                , item.OpenQty
                , item.OrderQty
                , item.UnitPrice
                , item.UOMId);
            }

            foreach (var item in purchaseOrderDto.PurchaseOrderCharges)
            {
                POAddChange.Rows.Add(item.AddChargeId
               , item.BasisId
               , item.Value);
            }

            para.Add("POHeaderId ", purchaseOrderDto.PurchaseOrderHeader.POHeaderId);
            para.Add("Status", POStatus.Created);
            para.Add("PORef ", purchaseOrderDto.PurchaseOrderHeader.PORef);
            para.Add("CategoryId", purchaseOrderDto.PurchaseOrderHeader.CategoryId);
            para.Add("Attention ", purchaseOrderDto.PurchaseOrderHeader.Attention);
            para.Add("OrderRef", purchaseOrderDto.PurchaseOrderHeader.OrderRef);
            para.Add("SupplierId ", purchaseOrderDto.PurchaseOrderHeader.SupplierId);
            para.Add("DeliStartDate", purchaseOrderDto.PurchaseOrderHeader.DeliveryStartDate);
            para.Add("DeliCancelDate ", purchaseOrderDto.PurchaseOrderHeader.DeliveryCancelDate);
            para.Add("DateInHouse", purchaseOrderDto.PurchaseOrderHeader.DateInHouse);
            para.Add("POTypeId ", purchaseOrderDto.PurchaseOrderHeader.POTypeId);
            para.Add("TaxId", purchaseOrderDto.PurchaseOrderHeader.TaxId);
            para.Add("PortOfLoading", purchaseOrderDto.PurchaseOrderHeader.PortOfLoading);
            para.Add("PortOfDischarge", purchaseOrderDto.PurchaseOrderHeader.PortOfDischarge);
            para.Add("CountryOfOrign ", purchaseOrderDto.PurchaseOrderHeader.CountryOfOrign);
            para.Add("CountryOfCons", purchaseOrderDto.PurchaseOrderHeader.CountryOfConsolidation);
            para.Add("CountryOfFinalDes", purchaseOrderDto.PurchaseOrderHeader.CountryOfFinalDestination);
            para.Add("ForwardingAgent", purchaseOrderDto.PurchaseOrderHeader.ForwardingAgent);
            para.Add("CurrencyId ", purchaseOrderDto.PurchaseOrderHeader.CurrencyId);
            para.Add("LocationId", purchaseOrderDto.PurchaseOrderHeader.LocationId);
            para.Add("PaymentTerm", purchaseOrderDto.PurchaseOrderHeader.PaymentTerm);
            para.Add("ShipmentMode", purchaseOrderDto.PurchaseOrderHeader.ShipmentMode);
            para.Add("DeliveryTerm ", purchaseOrderDto.PurchaseOrderHeader.DeliveryTerm);
            para.Add("LeadTimeinDays", purchaseOrderDto.PurchaseOrderHeader.LeadTimeinDays);
            para.Add("TransitTimeinDays", purchaseOrderDto.PurchaseOrderHeader.TransitTimeinDays);
            para.Add("SupplierRef", purchaseOrderDto.PurchaseOrderHeader.SupplierReference);
            para.Add("PackingType ", purchaseOrderDto.PurchaseOrderHeader.PackingType);
            para.Add("Remarks", purchaseOrderDto.PurchaseOrderHeader.Remarks);
            para.Add("UserId", purchaseOrderDto.PurchaseOrderHeader.CreateUserId);
            para.Add("ConsigneId", purchaseOrderDto.PurchaseOrderHeader.ConsigneId);
            para.Add("ConsigneAddId", purchaseOrderDto.PurchaseOrderHeader.ConsigneAddId);
            para.Add("BillToId ", purchaseOrderDto.PurchaseOrderHeader.BillToId);
            para.Add("ShipToId", purchaseOrderDto.PurchaseOrderHeader.ShipToId);
            para.Add("NotifyToId", purchaseOrderDto.PurchaseOrderHeader.NotifyToId);
            para.Add("SelectType", purchaseOrderDto.PurchaseOrderHeader.SelectType);
            para.Add("Reason", purchaseOrderDto.PurchaseOrderHeader.Reason);

            para.Add("PODetails", PODetail.AsTableValuedParameter("PODetailsType"));
            para.Add("POAddCharge", POAddChange.AsTableValuedParameter("POAddChargeType"));
            
            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransPurchaseOrderSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<GetPurchaseOrderDto> GetPurchasingOrderDtAsync(int POHeaderId)
        {
            GetPurchaseOrderDto poDeatils = new GetPurchaseOrderDto();
            DynamicParameters para = new DynamicParameters();

            para.Add("POHeaderId", POHeaderId);

            using (var multi = await DbConnection.QueryMultipleAsync("spTransPurchaseOrderGetDt", para, commandType: CommandType.StoredProcedure))
            {
                poDeatils.POHeader = multi.Read<TransPurchaseOrderHeader>();
                poDeatils.PODetails = multi.Read<PODetailsDto>();
                poDeatils.POAdditionalCharges = multi.Read<POAdditionalChargeDto>();
            }
            return poDeatils;
        }

        public async Task<IEnumerable<PurchaseOrderHdDto>> GetPurchaseOrderGetHeaderAsync(PurchaseOrderSearchDto header)
        {
            IEnumerable<PurchaseOrderHdDto> POHeaderList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CategoryId", header.CategoryId);
            para.Add("SupplierId", header.SupplierId);
            para.Add("PONo", header.PONo);

            POHeaderList = await DbConnection.QueryAsync<PurchaseOrderHdDto>("spTransPurchaseOrderGetHd", para
                    , commandType: CommandType.StoredProcedure);

            return POHeaderList;
        }

        public async Task<int> CancelPurchaseOrderAsync(TransPurchaseOrderHeader poHeader)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("UserId", poHeader.CreateUserId);
            para.Add("POHeaderId", poHeader.POHeaderId);
            para.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await DbConnection.QueryAsync<MRHeaderDto>("spTransPurchaseOrderCancel", para
                    , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<int> ReopenPurchaseOrderAsync(TransPurchaseOrderHeader poHeader)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("UserId", poHeader.CreateUserId);
            para.Add("POHeaderId", poHeader.POHeaderId);
            para.Add("Remarks", poHeader.Remarks);
            para.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await DbConnection.QueryAsync<MRHeaderDto>("spTransPurchaseOrderReopen", para
                    , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<GRNReceiveListDto>> GetGRNReceivingListAsync(GRNSearchDto search)
        {
            IEnumerable<GRNReceiveListDto> GRNRecList;
            DynamicParameters para = new DynamicParameters();

            para.Add("FromSite", search.FromSite);
            para.Add("ReceiveSite", search.ReceiveSite);
            para.Add("Supplier", search.Supplier);

            GRNRecList = await DbConnection.QueryAsync<GRNReceiveListDto>("spTransGRNGetReceivingList", para
                    , commandType: CommandType.StoredProcedure);

            return GRNRecList;
        }

        public async Task<IEnumerable<GRNReceiveDetails>> GetGRNReceivingDetailsAsync(long POHeaderId)
        {
            IEnumerable<GRNReceiveDetails> GRNRecDetails;
            DynamicParameters para = new DynamicParameters();

            para.Add("POHeaderId", POHeaderId);

            GRNRecDetails = await DbConnection.QueryAsync<GRNReceiveDetails>("spTransGRNGetReceivingDetails", para
                    , commandType: CommandType.StoredProcedure);

            return GRNRecDetails;
        }

        public async Task<ReturnDto> SaveGRNAsync(GRNSaveDto saveDto)
        {
            DataTable GRNDetail = new DataTable();
            DynamicParameters para = new DynamicParameters();

            GRNDetail.Columns.Add("GRNDetailsId", typeof(long));
            GRNDetail.Columns.Add("POHeaderId", typeof(long));
            GRNDetail.Columns.Add("PODetailsId", typeof(long));
            GRNDetail.Columns.Add("ArticleId", typeof(long));
            GRNDetail.Columns.Add("ColorId", typeof(long));
            GRNDetail.Columns.Add("SizeId", typeof(long));
            GRNDetail.Columns.Add("UOMId", typeof(int));
            GRNDetail.Columns.Add("POQty", typeof(int));
            GRNDetail.Columns.Add("Rate", typeof(int));
            GRNDetail.Columns.Add("CurrencyId", typeof(int));
            GRNDetail.Columns.Add("ReturnedQty", typeof(int));
            GRNDetail.Columns.Add("ReceivedQty", typeof(int));
            GRNDetail.Columns.Add("RejectedQty", typeof(int));
            GRNDetail.Columns.Add("PayableQty", typeof(int));
            GRNDetail.Columns.Add("FOCQty", typeof(int));
            GRNDetail.Columns.Add("IssuedQty", typeof(int));
            GRNDetail.Columns.Add("StorageId", typeof(int));
            GRNDetail.Columns.Add("UnitConvId", typeof(int));


            para.Add("GRNHeaderId", saveDto.GRNHeader.GRNHeaderId);
            para.Add("GRNNo", saveDto.GRNHeader.GRNNo);
            para.Add("GRNTypeId", saveDto.GRNHeader.GRNTypeId);
            para.Add("SupplierId", saveDto.GRNHeader.SupplierId);
            para.Add("ToSiteId", saveDto.GRNHeader.ToSiteId);
            para.Add("FromSIteId", saveDto.GRNHeader.FromSiteId);
            para.Add("DocNo", saveDto.GRNHeader.DocNo);
            para.Add("UserId", saveDto.GRNHeader.CreateUserId);
            para.Add("TransDate", saveDto.GRNHeader.TransDate);

            foreach (var item in saveDto.GRNDetails)
            {
                GRNDetail.Rows.Add(item.GRNDetailsId
                    , item.POHeaderId
                    , item.PODetailsId
                    , item.ArticleId
                    , item.ColorId
                    , item.SizeId
                    , item.UOMId
                    , item.POQty
                    , item.PORate
                    , item.CurrencyId
                    , item.ReturnedQty
                    , item.ReceivedQty
                    , item.RejectedQty
                    , item.PayableQty
                    , item.FOCQty
                    , item.IssuedQty
                    , item.StorageUnitId
                    , item.UnitConvId);
            }

            para.Add("GRNDetails", GRNDetail.AsTableValuedParameter("GRNDetailsType"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransGRNSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<GRNHeaderListDto>> GetGRNHeaderListAsync(SearchGRNDto grnDto)
        {
            IEnumerable<GRNHeaderListDto> GRNList;
            DynamicParameters para = new DynamicParameters();

            para.Add("GRNTypeId", grnDto.GRNTypeId);
            para.Add("ToSiteId", grnDto.ToSiteId);

            GRNList = await DbConnection.QueryAsync<GRNHeaderListDto>("spTransGRNHeaderGetList", para
                    , commandType: CommandType.StoredProcedure);

            return GRNList;
        }

        public async Task<GRNDashboardDetailsDto> GetGRNDetailsAsync(long GRNHeaderId)
        {
            GRNDashboardDetailsDto grnDetails = new GRNDashboardDetailsDto();
            DynamicParameters para = new DynamicParameters();

            para.Add("GRNHeaderId", GRNHeaderId);

            using (var multi = await DbConnection.QueryMultipleAsync("spTransGRNGetDetails", para, commandType: CommandType.StoredProcedure))
            {
                grnDetails.GRNHeader = multi.Read<TransGRNHeader>();
                grnDetails.GRNDetails = multi.Read<GRNDetailsDto>();
            }
            return grnDetails;
        }

        public async Task<int> CancelGRNAsync(TransGRNHeader grnHeader)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("UserId", grnHeader.CreateUserId);
            para.Add("GRNHeaderId", grnHeader.GRNHeaderId);
            para.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await DbConnection.QueryAsync<MRHeaderDto>("spTransGRNCancel", para
                    , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }


        public async Task<IEnumerable<GRNDetailsDto>> GetGRNData(GRNDetailsDto wsdt)
        {
            IEnumerable<GRNDetailsDto> GRNDataList;
            DynamicParameters para = new DynamicParameters();

            para.Add("ActivityCode", wsdt.CurrencyId);
            para.Add("CustomerId", wsdt.ArticleId);
            para.Add("SOHId", wsdt.ColorId);

            GRNDataList = await DbConnection.QueryAsync<GRNDetailsDto>("spTransGRNGetData", para
                    , commandType: CommandType.StoredProcedure);

            return GRNDataList;
        }


    }
}