using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.CCSystem.Transaction
{
    [Authorize]
    public class PurchasingController : BaseApiController
    {
        private readonly IApplicationCartonDbContext _context;
        private readonly IPurchasingRepository _purchasingRepository;

        public PurchasingController(IApplicationCartonDbContext context, IPurchasingRepository purchasingRepository)
        {
            _context = context;
            _purchasingRepository = purchasingRepository;
        }

        #region Supplier

        [HttpGet("Supplier")]
        public async Task<IActionResult> GetSupplier()
        {
            var result = await _context.TransSupplierHeader
                    .Select(x => new
                    {
                        x.SupplierId,
                        x.Name,
                        x.CompanyId,
                        x.ShortCode,
                        x.Address,
                        x.City,
                        x.State,
                        x.ZipPostalCode
                    ,
                        x.CountryId,
                        x.Tel,
                        x.Email,
                        x.CurrencyId,
                        x.SupTypeId,
                        x.VATNo,
                        x.TaxNo,
                        x.TinNo,
                        x.CreditDays,
                        x.ShipmentTolorence
                    ,
                        x.AccountGroupId,
                        x.bActive,
                        x.LocationId
                    })
                    .ToListAsync();
            return Ok(result);
        }

        #endregion

        #region Purchase Order

        [HttpPost("PendIndent")]
        public async Task<IActionResult> GetPurchasingOrderGetIndent(IndentSearchDto searchDto)
        {
            var result = await _purchasingRepository.GetPurchaseOrderGetIndentAsync(searchDto);
            return Ok(result);
        }

        [HttpPost("SavePO")]
        public async Task<IActionResult> SavePurchasingOrder(SavePurchaseOrderDto purchaseOrderDto)
        {
            var result = await _purchasingRepository.SavePurchaseOrderAsync(purchaseOrderDto);
            return Ok(result);
        }

        [HttpGet("PODt/{POHeaderId}")]
        public async Task<IActionResult> GetPurchaseOrderDetails(int POHeaderId)
        {
            var result = await _purchasingRepository.GetPurchasingOrderDtAsync(POHeaderId);
            return Ok(result);
        }

        [HttpPost("POHd")]
        public async Task<IActionResult> GetPurchaseOrderGetHeader(PurchaseOrderSearchDto header)
        {
            var result = await _purchasingRepository.GetPurchaseOrderGetHeaderAsync(header);
            return Ok(result);
        }

        [HttpPost("CancelPO")]
        public async Task<IActionResult> CancelPurchaseOrder(TransPurchaseOrderHeader poHeader)
        {
            var result = await _purchasingRepository.CancelPurchaseOrderAsync(poHeader);
            return Ok(result);
        }

        [HttpPost("ReopenPO")]
        public async Task<IActionResult> ReopenPurchaseOrder(TransPurchaseOrderHeader poHeader)
        {
            var result = await _purchasingRepository.ReopenPurchaseOrderAsync(poHeader);
            return Ok(result);
        }

        [HttpPost("GRNRec")]
        public async Task<IActionResult> GetGRNReceivingList(GRNSearchDto search)
        {
            var result = await _purchasingRepository.GetGRNReceivingListAsync(search);
            return Ok(result);
        }

        [HttpGet("GRNRecDt/{POHeaderId}")]
        public async Task<IActionResult> GetGRNReceivingDetails(long POHeaderId)
        {
            var result = await _purchasingRepository.GetGRNReceivingDetailsAsync(POHeaderId);
            return Ok(result);
        }

        [HttpPost("GRNSave")]
        public async Task<IActionResult> SaveGRN(GRNSaveDto saveDto)
        {
            var result = await _purchasingRepository.SaveGRNAsync(saveDto);
            return Ok(result);
        }

        [HttpPost("GRNHd")]
        public async Task<IActionResult> GetGRNHeaderList(SearchGRNDto searchDto)
        {
            var result = await _purchasingRepository.GetGRNHeaderListAsync(searchDto);
            return Ok(result);
        }

        [HttpGet("GrnDt/{GRNHeaderId}")]
        public async Task<IActionResult> GetGRNDetails(long GRNHeaderId)
        {
            var result = await _purchasingRepository.GetGRNDetailsAsync(GRNHeaderId);
            return Ok(result);
        }

        [HttpPost("CancelGRN")]
        public async Task<IActionResult> CancelGRN(TransGRNHeader grnHeader)
        {
            var result = await _purchasingRepository.CancelGRNAsync(grnHeader);
            return Ok(result);
        }

        [HttpPost("GetGRNData")]
        public async Task<IActionResult> GetGRNData(GRNDetailsDto wsdt)
        {
            var result = await _purchasingRepository.GetGRNData(wsdt);
            return Ok(result);
        }
        
        #endregion 

    }
}