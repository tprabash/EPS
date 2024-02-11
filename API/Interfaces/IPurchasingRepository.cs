using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IPurchasingRepository
    {
        Task<IEnumerable<PendIndentDto>> GetPurchaseOrderGetIndentAsync(IndentSearchDto searchDto);
        Task<ReturnDto> SavePurchaseOrderAsync(SavePurchaseOrderDto purchaseOrderDto);
        Task<GetPurchaseOrderDto> GetPurchasingOrderDtAsync(int POHeaderId);
        Task<IEnumerable<PurchaseOrderHdDto>> GetPurchaseOrderGetHeaderAsync(PurchaseOrderSearchDto header);
        Task<IEnumerable<GRNReceiveListDto>> GetGRNReceivingListAsync(GRNSearchDto search);
        Task<IEnumerable<GRNReceiveDetails>> GetGRNReceivingDetailsAsync(long POHeaderId);
        Task<ReturnDto> SaveGRNAsync(GRNSaveDto saveDto);
        Task<IEnumerable<GRNHeaderListDto>> GetGRNHeaderListAsync(SearchGRNDto grnDto);
        Task<GRNDashboardDetailsDto> GetGRNDetailsAsync(long GRNHeaderId);
        Task<int> CancelGRNAsync(TransGRNHeader grnHeader);
        Task<int> CancelPurchaseOrderAsync(TransPurchaseOrderHeader poHeader);
        Task<int> ReopenPurchaseOrderAsync(TransPurchaseOrderHeader poHeader);
        Task<IEnumerable<GRNDetailsDto>> GetGRNData(GRNDetailsDto wsdt);
    }
}