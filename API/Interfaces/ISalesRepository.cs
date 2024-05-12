using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ISalesRepository
    {
         Task<TransSalesOrderHd> GetSalesOrderRefAsync();
         Task<ReturnDto> SaveSalesOrderAsync(List<SalesOrderDeliveryDto> salesOrder);
         Task<IEnumerable<SalesOrderRetDto>> GetSalesOrderAsync(string SORefNo);
         Task<IEnumerable<PendingOrderItemsDto>> GetPendOrderItemsAsync(int custometId);
         Task<IEnumerable<PendingDelivOrderDto>> GetPendDelivOrderAsync(PendingOrderItemsDto items);
         Task<RefNumDto> GetRefNumberAsync(string TransType);
         Task<ReturnDto> SaveJobCardAsync(List<TransJobDetail> trnsJob);
         Task<IEnumerable<ReturnJobCardDto>> GetJobCardDetailsAsync(string jobNo);
         Task<IEnumerable<TransJobHeader>> GetFPOPendingJobsAsync();
         Task<IEnumerable<PendJobDetailsDto>> GetFPOPendingJobDtAsync(int JobId);
         Task<ReturnDto> SaveFPOAsync(List<FacProdOrderDto> facProdOrderDtos);
         Task<IEnumerable<ReturnFPODetailsDto>> GetFPODetailsAsync(string FPONo);
         Task<int> DeleteFPOAsync(DeleteFPODto fPODto);
         Task<FPPOProductionDto> GetFPPOInDetailsAsync(int FPPODId);
         Task<int> SaveFPPOInAsync(TransProductionDetails prodDetails);
         Task<IEnumerable<TransProductionTotalDto>> GetTransProductionTotAsync();
         Task<FPPOProductionDto> GetFPPOOutDetailsAsync(int FPPODId);
         Task<int> SaveFPPOOutAsync(TransProductionDetails prodDetails);
         Task<int> SaveFPPORejectAsync(List<TransProdDetailsDto> prodDetails);
         Task<IEnumerable<PendDispatchDto>> GetPendDispatchDtAsync(PendDispatchDto prod);
        Task<ReturnDto> SaveDispatchedDtAsync(TransDispatchDto dispatch);
         Task<IEnumerable<DispatchedDetDto>> GetDispatchDetails(DispatchSearchDto dispatch);
         Task<int> CancelDispatchDtAsync(TransDispatchHeader dispHd);
         Task<ReturnDto> SaveCostingAsync(List<SavedCostingDto> costDt);
        //  Task<CostingSheetDto> GetCostingDetailsAsync(long costHearderId);
         Task<CostingSheetDto> GetCostingDetailsAsync(CostHeaderDto costHearder);
         Task<IEnumerable<SalesItemDto>> GetPendSalesOrderItemAsync(int SOHeaderId);
         Task<IEnumerable<CostHeaderDto>> GetCostHeaderAsync(CostHeaderDto costHead);
         Task<int> AttachCostSheetSOAsync(TransSalesOrderItemDt soItemDt);
         Task<int> RemoveCostSheetSOAsync(TransSalesOrderItemDt soItemDt);
         Task<IEnumerable<CostHeaderDto>> GetCostHeaderListAsync(long CustomerId);
         Task<IEnumerable<CostHeaderDto>> GetCostHeaderByRefListAsync(string RefNo);
         //Task<IEnumerable<ApprovalUsersDto>> GetApprovalRouteDetailsAsync(ApprovalUsersDto appUser);
         Task<ApproveUsersGetDto> GetApprovalRouteDetailsAsync(ApproveUserDto appUser);
         Task<int> SaveApproveCenterAsync(TransApprovalCenter appCenter);
         Task<IEnumerable<ApproveCenterDto>> GetApproveCenterDetailsAsync(int userId);
         Task<IEnumerable<PendingOrderItemsDto>> GetJobCardListAsync(string CustomerRef);
         Task<IEnumerable<FPONoListDto>> GetFPONoListAsync(string customerRef);
         Task<IEnumerable<DispatchNoListDto>> GetDispatchListAsync(string customerRef);
         Task<IEnumerable<DispatchNoListDto>> GetDispatchListsAsync(string dispatchNo);
         Task<int> SaveSalesOrderUploadAsync(TransSalesOrderFileUpload upload);
        //  Task<IEnumerable<TransFtyProductionOrder>> GetMINPendFPODetailsAsync();
         Task<IEnumerable<MINDetailsDto>> GetMINDetailsAsync(long MINHeaderId);
         Task<IEnumerable<SalesOrderDtListDto>> GetSalesOrderDetailsAsync(string customerPO);
        Task<IEnumerable<PendingOrderItemsDto>> GetJobCardListsAsync(string jobcardNo);
        Task<logDto> GetCostLogDetailsAsync(long CostingId);
        Task<int> CancelSalesOrderAsync(TransSalesOrderHd salesOrderHd);
        Task<IEnumerable<CostingAttachSODto>> GetCostAttachedSOListAsync(long CostingId);
        Task<IEnumerable<SalesOrderStatusDto>> GetSalesOrderStatusAsync(string CustPORef);
        Task<int> updateSalesOrderPriceAsync(TransSalesOrderItemDt soItemDt);
        Task<int> saveSalesOrderDefaultAsync(TransSalesOrderDefault salesDefault);
        Task<IEnumerable<SalesOrderRetDto>> GetSalesOrderDefaultAsync(int CustomerId);
        Task<IEnumerable<TransSalesOrderDefault>> GetSalesDefaultAsync(int CustomerId);
        Task<IEnumerable<DashboarDetailsDto>> GetDashboardOneDetailsAsync(DashboardSearchDto dashDto);
        Task<int> CancelJobCardAsync(CancelJobcardDto JobHDto);
        Task<IEnumerable<DispatchSODto>> GetPossibilityAsync(long SOHeaderId);
        Task<IEnumerable<TransfairableDeliveryDto>> GetTransferableDeliveriesAsync(TransfairableDeliveryDto prod);
        Task<IEnumerable<TransfairablePoRefDto>> GetTransfairablePoRefAsync(string RefNo);
        Task<IEnumerable<TransfAlternateDeliveryDto>>TransfairableAlterDeliveryAsync(long FPPOID);
        Task<ReturnDto> SaveTranferAsync(SavedTranferDto trnsDto);
        Task<IEnumerable<TransferListDto>> TransferListAsync(int CustomerId);
        Task<TranferGetDto>GetTranferDetailsAsync(long TransferHdId);
        Task<IEnumerable<BlockBookingDto>> GetBlockBookingData(BlockBookingDto wsdt);
         Task<ReturnDto> SaveBlockBookingData (List<SaveBlockBookingDto> wsDt);
        Task<IEnumerable<DispatchStockRespondDto>> GetDispatchStockAsync(DispatchRequestDto requestDto);

         // Order Creation
        Task<IEnumerable<OrderCreationDto>> GetOCData(OrderCreationDto ocdto);
        Task<ReturnDto> SaveOCData (List<SaveOrderCreationDto>ocdto);
        
        //Production Issue To
        Task<IEnumerable<ProductionDto>> GetProductionIssueToData(ProductionDto productdto);
        Task<ReturnDto> SaveProductionIssueToData (SaveProductionDto productdto);

        Task<IEnumerable<ProductionDto>> GetProductionUpdateData(ProductionDto productdto);
        Task<ReturnDto> SaveProductionUpdateData (SaveProductionDto productdto);
        Task<IEnumerable<ProductionOutDetailsDto>> GetProductionUpdateDetailData(ProductionOutDetailsDto productdto);

        Task<IEnumerable<ProductionDto>> GetProductionQcOutData(ProductionDto productdto);
        Task<ReturnDto> SaveProductionQcOutData (SaveProductionDto productdto);

        Task<IEnumerable<ProductionDto>> GetProductionDispatchData(ProductionDto productdto);
        Task<ReturnDto> SaveProductionDispatchData (SaveDispatchDto productdto);

        //Po Association
        Task<IEnumerable<POAssociationDto>> GetPOAssociationData(POAssociationDto productdto);
        Task<ReturnDto> SavePOAssociationDataAsync (SavePOAssociationDto productdto);

         //Wash Cost
        Task<IEnumerable<CostSheetDto>> GetCostingData(CostSheetDto costdto);
        Task<ReturnDto>SaveCostingData(List<SaveCostSheetDto >costdto);

                //Recipe
        Task<IEnumerable<RecipeDto>> GetRecipeData(RecipeDto rcpdto);
        Task<ReturnDto> SaveRecipeData (List<SaveRecipeDto> rcpdto);

        //GRN Data
        Task<IEnumerable<GRNDto>> GetGRNData(GRNDto wsdt);
        Task<ReturnDto> SaveGRNDATA(SaveGRNDto wsdt);

        // Order Creation
        Task<IEnumerable<ProductionOutDto>> GetProductionOutData(ProductionOutDto ocdto);
        // Task<ReturnDto> SaveProductionOutData (List<SaveOrderCreationDto>ocdto);
    }
}