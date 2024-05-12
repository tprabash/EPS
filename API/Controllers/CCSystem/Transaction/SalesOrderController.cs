using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace API.Controllers.CCSystem.Transaction
{
    [Authorize]
    public class SalesOrderController : BaseApiController
    {
        private readonly IApplicationCartonDbContext _context;
        private readonly ISalesRepository _salesRepository;
        public SalesOrderController(IApplicationCartonDbContext context, ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
            _context = context;
        }        

        [HttpGet("SORefNo")]
        public async Task<IActionResult> GetSalesOrderRef()
        {
            var refNo = await _salesRepository.GetSalesOrderRefAsync();
            return Ok(refNo);
        }

        [HttpGet("SO/{orderRef}")]
        public async Task<IActionResult> GetSalesOrder(string orderRef)
        {
            var saleOredrList = await _salesRepository.GetSalesOrderAsync(orderRef);
            return Ok(saleOredrList);
        }        

        [HttpPost("SaSO")]
        public async Task<IActionResult> SaveSalesOrder(List<SalesOrderDeliveryDto> salesOrder)
        {
            var result = await _salesRepository.SaveSalesOrderAsync(salesOrder);
            return Ok(result);
        }

        [HttpGet("PendSO/{customerId}")]
        public async Task<IActionResult> GetPendCostSalesOrders(int customerId)
        {
            var result = await _context.TransSalesOrderHeader 
                .Where(x => x.CustomerId == customerId)               
                .Join(_context.TransSalesOrderItemDt , h => h.AutoId , d => d.SOHeaderId ,
                    (h , d ) => new {
                       autoId = h.AutoId,
                       orderRef = h.OrderRef,
                       costingId = d.CostingId
                    })
                .Where(d => d.costingId == 0).Distinct()
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("SOList/{customerPO}")]
        public async Task<IActionResult> GetSalesOrderList(string customerPO) 
        {
            var result = await  _context.TransSalesOrderHeader
                    .Where(x => x.CustomerRef.Contains(customerPO))
                    .Join(_context.MstrCustomerHeader, s => s.CustomerId , c => c.AutoId , 
                     (s,c) => new {
                       autoId = s.AutoId,
                       customer = c.Name,
                       trnsDate = s.TrnsDate,
                       delDate = s.DelDate,
                       orderRef = s.OrderRef,
                       customerRef = s.CustomerRef
                     })       
                    .ToListAsync();
            return Ok(result);
        }

        [HttpGet("SalesList/{orderRef}")]
        public async Task<IActionResult> GetSalesOrderListByOrderRef(string orderRef) 
        {
            var result = await  _context.TransSalesOrderHeader
                    .Where(x => x.OrderRef.Contains(orderRef))
                    .Join(_context.MstrCustomerHeader, s => s.CustomerId , c => c.AutoId , 
                     (s,c) => new {
                       autoId = s.AutoId,
                       customer = c.Name,
                       trnsDate = s.TrnsDate,
                       delDate = s.DelDate,
                       orderRef = s.OrderRef,
                       customerRef = s.CustomerRef
                     })       
                    .ToListAsync();
            return Ok(result);
        }

        [HttpGet("SODtList/{customerPO}")]
        public async Task<IActionResult> GetSalesOrderDetailList(string customerPO)
        {
            var result = await _salesRepository.GetSalesOrderDetailsAsync(customerPO);
            return Ok(result);
        }

        [HttpGet("SOHead/{SOHeaderId}")]
        public async Task<IActionResult> GetPendSalesOrderItem(int SOHeaderId)
        {
            var saleOredrList = await _salesRepository.GetPendSalesOrderItemAsync(SOHeaderId);
            return Ok(saleOredrList);
        }

        [HttpGet("CSOList/{CostingId}")]
        public async Task<IActionResult> GetCostSalesOrderList(int CostingId)
        {
            var saleOredrList = await _context.TransSalesOrderItemDt
                .Where(x => x.CostingId == CostingId)
                .Join(_context.TransSalesOrderHeader , i => i.SOHeaderId , h => h.AutoId 
                    , (i, h) => new {
                        autoId = h.AutoId,
                        customerRef = h.CustomerRef,
                        orderRef = h.OrderRef
                    }).ToListAsync();
            return Ok(saleOredrList);
        }

        [HttpGet("JobPedItems/{id}")]
        public async Task<IActionResult> GetPendOrderItems(int id)
        {
            var pendOrderItems = await _salesRepository.GetPendOrderItemsAsync(id);
            return Ok(pendOrderItems);
        }

        [HttpPost("JobPedOrders")]
        public async Task<IActionResult> GetPendDelivOrder(PendingOrderItemsDto items)
        {
            var pendOrders = await _salesRepository.GetPendDelivOrderAsync(items);
            return Ok(pendOrders);
        }

        [HttpGet("JCList/{customerPO}")]
        public async Task<IActionResult> GetJobCardList(string customerPO) 
        {
            var result = await _salesRepository.GetJobCardListAsync(customerPO);
            return Ok(result);
        }

        [HttpGet("JCLists/{jobcardNo}")]
        public async Task<IActionResult> GetJobCardLists(string jobcardNo) 
        {
            var result = await _salesRepository.GetJobCardListsAsync(jobcardNo);
            return Ok(result);
        }

        [HttpGet("RefNum/{TransType}")]
        public async Task<IActionResult> GetRefNumber(string TransType)
        {
           var refNum = await _salesRepository.GetRefNumberAsync(TransType);
           return Ok(refNum);
        }

        [HttpPost("CostComb")]
        public async Task<IActionResult> GetCostComination(PendingOrderItemsDto items)
        {
            var combin = await _context.TransCostingHeader
                .Where(x => x.CustomerId == items.CustomerId && x.ArticleId == items.ArticleId 
                    && x.ColorId == items.ColorId && x.SizeId == items.SizeId)
                .Join(_context.MstrCombination, h => h.CombinId, c => c.AutoId
                    , (h, c) =>
                    new
                    {
                        CombinId = c.AutoId,
                        Combination = c.Combination
                    }).Distinct().ToListAsync();
          
            return Ok(combin);
        }

        [HttpPost("SaveJob")]
        public async Task<IActionResult> SaveJobCard(List<TransJobDetail> transJob) 
        {
            var result = await _salesRepository.SaveJobCardAsync(transJob);
            return Ok(result);
        }

        [HttpGet("JobCard/{jobNo}")]
        public async Task<IActionResult> GetJobCardDetails(string jobNo) 
        {
            var result = await _salesRepository.GetJobCardDetailsAsync(jobNo);
            return Ok(result);
        }

        [HttpGet("FPO/JobList")]
        public async Task<IActionResult> GetFPOPendingJobs() 
        {
            var jobList = await _salesRepository.GetFPOPendingJobsAsync();
            return Ok(jobList);
        }

        [HttpGet("FPOList/{customerRef}")]
        public async Task<IActionResult> GetFPONoList(string customerRef) 
        {
            var result = await _salesRepository.GetFPONoListAsync(customerRef);
            return Ok(result);
        }

        [HttpGet("FPO/JobList/{id}")]
        public async Task<IActionResult> GetFPOPendingJobDt(int id) 
        {
            var jobList = await _salesRepository.GetFPOPendingJobDtAsync(id);
            return Ok(jobList);
        }

        [HttpPost("SaveFPO")]
        public async Task<IActionResult> SaveFPO(List<FacProdOrderDto> facProdOrderDto)
        {
            var result = await _salesRepository.SaveFPOAsync(facProdOrderDto);
            return Ok(result);
        }

        [HttpGet("FPODetails/{FPONo}")]
        public async Task<IActionResult> GetFPODetails(string FPONo)
        {
            var FPODetails = await _salesRepository.GetFPODetailsAsync(FPONo);
            return Ok(FPODetails);
        }

        [HttpPost("DeleteFPO")]
        public async Task<IActionResult> DeleteFPO(DeleteFPODto fpoDto)
        {
            var result = await _salesRepository.DeleteFPOAsync(fpoDto);
            return Ok(result);
        }

        [HttpPost("CancelJob")]
        public async Task<IActionResult> CancelJobCard(CancelJobcardDto JobHDto)
        {
            var result = await _salesRepository.CancelJobCardAsync(JobHDto);
            return Ok(result);
        }

        [HttpGet("FPPOIn/{FPPODId}")]
        public async Task<IActionResult> GetFPPOInDetails(int FPPODId)
        {
            var result = await _salesRepository.GetFPPOInDetailsAsync(FPPODId);
            return Ok(result);
        }

        [HttpPost("SaveFPPOIn")]
        public async Task<IActionResult> SaveFPPOInDeails(TransProductionDetails prod)
        {
            var result = await _salesRepository.SaveFPPOInAsync(prod);
            return Ok(result);
        }

        [HttpGet("FPPOTot")]
        public async Task<IActionResult> GetTransProductionTot()
        {
            var result = await _salesRepository.GetTransProductionTotAsync();
            return Ok(result);
        }

        [HttpGet("FPPOOut/{FPPODId}")]
        public async Task<IActionResult> GetFPPOOutDetails(int FPPODId)
        {
            var result = await _salesRepository.GetFPPOOutDetailsAsync(FPPODId);
            return Ok(result);
        }

        [HttpPost("SaveFPPOOut")]
        public async Task<IActionResult> SaveFPPOOutDetails(TransProductionDetails prod)
        {
            var result = await _salesRepository.SaveFPPOOutAsync(prod);
            return Ok(result);
        }

        [HttpPost("SaveFPPORej")]
        public async Task<IActionResult> SaveFPPORejectDetails(List<TransProdDetailsDto> prodDetails)
        {
            var result = await _salesRepository.SaveFPPORejectAsync(prodDetails);
            return Ok(result);
        }  
       
        [HttpPost("SaveCost")]
        public async Task<IActionResult> SaveCosting(List<SavedCostingDto> costDetails)
        {
            var result = await _salesRepository.SaveCostingAsync(costDetails);
            return Ok(result);
        }

        [HttpGet("CostList/{id}")]
        public async Task<IActionResult> GetCostHeaderList(long id) 
        {
            var result = await _salesRepository.GetCostHeaderListAsync(id);
            return Ok(result);
        }
        
        [HttpGet("CostRefList/{refNo}")]
        public async Task<IActionResult> GetCostHeaderListByRef(string refNo) 
        {
            var result = await _salesRepository.GetCostHeaderByRefListAsync(refNo);
            return Ok(result);
        }

        [HttpPost("CostHd")]
        public async Task<IActionResult> GetCostHeader(CostHeaderDto costHead)
        {
            var result = await _salesRepository.GetCostHeaderAsync(costHead);
            return Ok(result);
        }  

        [HttpPost("CSDt")]
        public async Task<IActionResult> GetCostingDetails(CostHeaderDto costHeader)
        {
            var result = await _salesRepository.GetCostingDetailsAsync(costHeader);
            return Ok(result);
        }    

        [HttpPost("AttachCS")]
        public async Task<IActionResult> AttachCostSheetSO(TransSalesOrderItemDt soItem)
        {
            var result = await _salesRepository.AttachCostSheetSOAsync(soItem);
            return Ok(result);
        }   

        [HttpPost("RemoveCS")]
        public async Task<IActionResult> RemoveCostSheetSO(TransSalesOrderItemDt soItem)
        {
            var result = await _salesRepository.RemoveCostSheetSOAsync(soItem);
            return Ok(result);
        }  

        [HttpPost("AppRouteDt")]
        public async Task<IActionResult> GetApproveRouteDeatils(ApproveUserDto appUser)
        {
            var result = await _salesRepository.GetApprovalRouteDetailsAsync(appUser);
            return Ok(result);
        }    

        [HttpPost("SaveApprove")]
        public async Task<IActionResult> SaveApproveCenterDt(TransApprovalCenter appCenter)
        {
            var result = await _salesRepository.SaveApproveCenterAsync(appCenter);
            return Ok(result);
        }   

        [HttpGet("ACDt/{userId}")]
        public async Task<IActionResult> GetApproveCenterDt(int userId)
        {
            var result = await _salesRepository.GetApproveCenterDetailsAsync(userId);                       
            return Ok(result);
        }

        [HttpPost("UploadFile"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFiles(IFormCollection data)
        {
            long result = 0 ; var soHeaderId = 0; var userId = 0; var autoId = 0; 
            var file = data.Files.First(); 
            /// GET OTHER USER DATA SALES ORDER HEADER ID AND USER ID  
            if (data.TryGetValue("soHeaderId", out var so)) {
                soHeaderId = Convert.ToInt32(so);
            }  
            if (data.TryGetValue("autoId", out var id)) {
                autoId = Convert.ToInt32(id);
            }
            if (data.TryGetValue("userId", out var user)) {
                userId = Convert.ToInt32(user);
            }

            var uploadResponse = new FileUploadResponse();

            var f = file;
            string name = f.FileName.Replace(@"\\\\", @"\\");
            
            if (f.Length > 0)
            {
                var memoryStream = new MemoryStream();
                try
                {
                    await f.CopyToAsync(memoryStream);
                    
                        var fileObj = new TransSalesOrderFileUpload()
                        {
                            AutoId = autoId,
                            FileName = Path.GetFileName(name),
                            FileSize = memoryStream.Length,
                            SOHeaderId = Convert.ToInt64(soHeaderId),
                            UploadDate = DateTime.Now,
                            UploadUserID = userId,
                            DocFile = memoryStream.ToArray()
                        };
                        result = await _salesRepository.SaveSalesOrderUploadAsync(fileObj);
                        // _context.FileUpload.Add(fileObj);
                        // await _context.SaveChangesAsync(default);

                        // var id = fileObj.AutoId;
                        // result = id;                   
                }
                finally
                {
                    memoryStream.Close();
                    memoryStream.Dispose();
                }
            }
            return Ok(result);
        }

        [HttpGet("DownloadFile/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var selectedFile = await _context.FileUpload
                .Select(x => new {x.FileName , x.DocFile, x.AutoId })
                .Where(f => f.AutoId == id)
                .FirstOrDefaultAsync();
            // return Ok(selectedFile);
            var stream = selectedFile.DocFile;            
            // if (stream == null)
            //     return NotFound();
            // return new FileContentResult( stream , "application/octet-stream" ); 
            
            return File(stream, "application/octet-stream", selectedFile.FileName);
        }

        // [HttpGet("PendFPO")]
        // public async Task<IActionResult> GetMINPendFPODetails()
        // {
        //     var result = await _salesRepository.GetMINPendFPODetailsAsync();
        //     return Ok(result);
        // }  

        [HttpGet("MINDt/{MINHeaderId}")]
        public async Task<IActionResult> GetMINDetails(long MINHeaderId)
        {
            var result = await _salesRepository.GetMINDetailsAsync(MINHeaderId);
            return Ok(result);
        } 
        
        [HttpGet("CLogList/{CostingId}")]
        public async Task<IActionResult> GetCostLogDetails(long CostingId)
        {
            var costloglist = await _salesRepository.GetCostLogDetailsAsync(CostingId);
            return Ok(costloglist);
        }

        [HttpPost("CancelSO")]
        public async Task<IActionResult> CancelSalesOrder(TransSalesOrderHd salesOrderHd)
        {
            var result = await _salesRepository.CancelSalesOrderAsync(salesOrderHd);
            return Ok(result);
        }

        [HttpGet("CostAttachSOList/{id}")]
        public async Task<IActionResult> GetCostAttachedSOList(long id) 
        {
            var result = await _salesRepository.GetCostAttachedSOListAsync(id);
            return Ok(result);
        }
        [HttpGet("SOStatusList/{CustPORef}")]
        public async Task<IActionResult> GetSalesOrderStatus(string CustPORef)
        {
            var saleOredrStatusList = await _salesRepository.GetSalesOrderStatusAsync(CustPORef);
            return Ok(saleOredrStatusList);
        }

        [HttpPost("UpSOPr")]
        public async Task<IActionResult> updateSalesOrderPrice(TransSalesOrderItemDt soItem)
        {
            var result = await _salesRepository.updateSalesOrderPriceAsync(soItem);
            return Ok(result);
        }

        [HttpPost("SalesDef")]
        public async Task<IActionResult> saveSalesOrderDefault(TransSalesOrderDefault salesDefault)
        {
            var result = await _salesRepository.saveSalesOrderDefaultAsync(salesDefault);
            return Ok(result);
        } 

        [HttpGet("SOHDefault/{CustomerId}")]
        public async Task<IActionResult> GetSalesOrderDefault(int CustomerId)
        {
            var saleOredrdefautvalue = await _salesRepository.GetSalesOrderDefaultAsync(CustomerId);
            return Ok(saleOredrdefautvalue);
        }

        [HttpGet("SOD/{CustomerId}")]
        public async Task<IActionResult> GetSalesDefault(int CustomerId)
        {
            var result = await _salesRepository.GetSalesDefaultAsync(CustomerId);
            return Ok(result);
        } 
        [HttpGet("ExRateByDt/{myDate}")]
        public async Task<IActionResult> GetExchangeRateByDate(DateTime myDate) 
        {
            DateTime? requestDate = myDate;
            var result = await _context.TransExchangeRate
                .Where(x => x.ValidFrom == requestDate)
                .Join(_context.MstrCurrency , e => e.CurrencyFId , f => f.AutoId , (e,f) => new  {e,f} )
                .Join(_context.MstrCurrency , a => a.e.CurrencyTId , t => t.AutoId , (a,t) =>
                    new {
                    autoId = a.e.AutoId,
                    validFrom = a.e.ValidFrom,
                    rate = a.e.Rate,
                    currencyFrom = a.f.Code,
                    currencyTo = t.Code
                    })
                .ToListAsync();
            return Ok(result);
        }   

        #region CCS-Dashboard

        [HttpPost("DashOneD")]
        public async Task<IActionResult> GetDashboardOneDetails(DashboardSearchDto dashDto)
        {
            var result = await _salesRepository.GetDashboardOneDetailsAsync(dashDto);
            return Ok(result);
        }

        #endregion 

        #region GetPossibility
        [HttpGet("GetCheckQty/{SOHeaderId}")]
        public async Task<IActionResult> GetPossibility(long SOHeaderId)
        {
            var result = await _salesRepository.GetPossibilityAsync(SOHeaderId);
            return Ok(result);
        }
        #endregion

        #region GetTransferableDeliveries
        [HttpPost("PendTransfers")]
        public async Task<IActionResult> GetTransferableDeliveries(TransfairableDeliveryDto prod)
        {
            var result = await _salesRepository.GetTransferableDeliveriesAsync(prod);
            return Ok(result);
        }
        #endregion

        #region GetUnFinishPOs
        [HttpGet("UnFinishPOs/{RefNo}")]
         public async Task<IActionResult> GetTransfairablePoRef(string RefNo)
        {
            var result = await _salesRepository.GetTransfairablePoRefAsync(RefNo);
            return Ok(result);
        }
        #endregion

        #region TransfairableAlterDelivery
        [HttpGet("FairableAlterDel/{FPPOID}")]
        public async Task<IActionResult> TransfairableAlterDelivery(long FPPOID)
        {
            var result = await _salesRepository.TransfairableAlterDeliveryAsync(FPPOID);
            return Ok(result);
        }
        #endregion
        
        #region SaveTranfer
        [HttpPost("SaveTransfer")]
        public async Task<IActionResult> SaveTranfer(SavedTranferDto trnsDto) 
        {
            var result = await _salesRepository.SaveTranferAsync(trnsDto);
            return Ok(result);
        }
        #endregion

        #region GetTranferList
        [HttpGet("GetTranferList/{CustomerId}")]
        public async Task<IActionResult> TransferList(int CustomerId) 
        {
            var result = await _salesRepository.TransferListAsync(CustomerId);
            return Ok(result);
        }
        #endregion

        #region GetTranferDetails
        [HttpGet("TranferDetails/{TransferHdId}")]
        public async Task<IActionResult> GetTranferDetails(long TransferHdId) 
        {
            var result = await _salesRepository.GetTranferDetailsAsync(TransferHdId);
            return Ok(result);
        }
        #endregion

        #region GetStoreSite
        //  [HttpGet("Store/{soDelvId}")]
        // public async Task<IActionResult> GetStoreSite(long soDelvId)
        // {
        //     var storeSiteList = await _context.TransFtyProductionProcessOrderDt
        //         .Where(x => x.SODelivDtId == soDelvId)
        //         .Select(x => new { x.ShortCode, x.Name, x.AutoId , x.bActive })
        //         .ToListAsync();
        //     return Ok(storeSiteList);
        // }

        [HttpGet("Store/{soDelvId}")]
        public async Task<IActionResult> GetStoreSite(long soDelvId)
        {
            var storeSiteList = await _context.TransFtyProductionProcessOrderDt
                .Where(x => x.SODelivDtId == soDelvId)
                .Join(_context.MstrStoreSite, c => c.DispatchId, b => b.AutoId
                    , (c, b) =>
                    new
                    {
                        autoId = b.AutoId,
                        siteName = b.SiteName,
                    }).ToListAsync();
            return Ok(storeSiteList);
        }
        #endregion
        #region "Block Booking"
          [HttpPost("GetBlockBookingData")]
          public async Task<IActionResult> GetBlockBookingData(BlockBookingDto wsDt)
           {
            var result = await _salesRepository.GetBlockBookingData(wsDt);
            return Ok(result);
           }

        [HttpPost("SaveBlockBookingData")]
            public async Task<IActionResult> SaveBlockBookingData(List<SaveBlockBookingDto> wsDt)
            {
                var result = await _salesRepository.SaveBlockBookingData(wsDt);
                return Ok(result);
            }
        #endregion "Block Booking"
   
         #region "Order Creation"
        [HttpPost("GetOCData")]
        public async Task<IActionResult> GetOCData(OrderCreationDto ocdto)
        {
            var result = await _salesRepository.GetOCData(ocdto);
            return Ok(result);
        }

        [HttpPost("SaveOCData")]
        public async Task<IActionResult> SaveOCData(List<SaveOrderCreationDto >ocdto)
        {
                var result = await _salesRepository.SaveOCData(ocdto);
                return Ok(result);
        }
     #endregion "Order Creation"

     #region "Recipe"
       [HttpPost("GetRecipeData")]
       public async Task<IActionResult> GetRecipeData(RecipeDto rcpdto)
       {
        var result = await _salesRepository.GetRecipeData(rcpdto);
        return Ok(result);
       }
       [HttpPost("SaveRecipeData")]
       public async Task<IActionResult> SaveRecipeData(List<SaveRecipeDto >rcpdto)
       {
        var result = await _salesRepository.SaveRecipeData(rcpdto);
        return Ok(result);
       }       
#endregion "Recipe"
#region "Washing Costing"
       [HttpPost("GetCostingData")]
       public async Task<IActionResult> GetCostingData(CostSheetDto costdto)
       {
        var result = await _salesRepository.GetCostingData(costdto);
        return Ok(result);
       }
       [HttpPost("SaveCostingData")]
       public async Task<IActionResult> SaveCostingData(List<SaveCostSheetDto >costdto)
       {
        var result = await _salesRepository.SaveCostingData(costdto);
        return Ok(result);
       }       
#endregion "Washing Costing"
      #region "ProductionIssueTo"
        [HttpPost("GetProductionIssueToData")]
        public async Task<IActionResult> GetProductionIssueToData(ProductionDto productdto)
        {
            var result = await _salesRepository.GetProductionIssueToData(productdto);
            return Ok(result);
        }
        [HttpPost("SaveProductionIssueToData")]
        public async Task<IActionResult> SaveProductionIssueToData(SaveProductionDto saveProductionDto)
        {
            var result = await _salesRepository.SaveProductionIssueToData(saveProductionDto);
            return Ok(result);
        }
        #endregion "ProductionIssueTo"

        #region "ProductionIUpdate"

        [HttpPost("GetProductionUpdateData")]
        public async Task<IActionResult> GetProductionUpdateData(ProductionDto productdto)
        {
            var result = await _salesRepository.GetProductionUpdateData(productdto);
            return Ok(result);
        }
        [HttpPost("SaveProductionUpdateData")]
        public async Task<IActionResult> SaveProductionUpdateData(SaveProductionDto saveProductionDto)
        {
            var result = await _salesRepository.SaveProductionUpdateData(saveProductionDto);
            return Ok(result);
        }

        [HttpPost("GetProductionUpdateDetailData")]
        public async Task<IActionResult> GetProductionUpdateDetailData(ProductionOutDetailsDto productdto)
        {
            var result = await _salesRepository.GetProductionUpdateDetailData(productdto);
            return Ok(result);
        }
        #endregion "ProductionIUpdate"

        #region "ProductionQcOut"

        [HttpPost("GetProductionQcOutData")]
        public async Task<IActionResult> GetProductionQcOutData(ProductionDto productdto)
        {
            var result = await _salesRepository.GetProductionQcOutData(productdto);
            return Ok(result);
        }
        [HttpPost("SaveProductionQcOutData")]
        public async Task<IActionResult> SaveProductionQcOutData(SaveProductionDto saveProductionDto)
        {
            var result = await _salesRepository.SaveProductionQcOutData(saveProductionDto);
            return Ok(result);
        }
        #endregion "ProductionQcOut"

        #region "ProductionDispatch"

        [HttpPost("GetProductionDispatchData")]
        public async Task<IActionResult> GetProductionDispatchData(ProductionDto productdto)
        {
            var result = await _salesRepository.GetProductionDispatchData(productdto);
            return Ok(result);
        }
        [HttpPost("SaveProductionDispatchData")]
        public async Task<IActionResult> SaveProductionDispatchData(SaveDispatchDto saveDispatchDto)
        {
            var result = await _salesRepository.SaveProductionDispatchData(saveDispatchDto);
            return Ok(result);
        }
        #endregion "ProductionDispatch"

        #region "PO-Association"

        [HttpPost("GetPOAssociationData")]
        public async Task<IActionResult> GetPOAssociationData(POAssociationDto productdto)
        {
            var result = await _salesRepository.GetPOAssociationData(productdto);
            return Ok(result);
        }
        [HttpPost("SavePOAssociationData")]
        public async Task<IActionResult> SavePOAssociationData(SavePOAssociationDto saveAssociationhPoDto)
        {
            var result = await _salesRepository.SavePOAssociationDataAsync(saveAssociationhPoDto);
            return Ok(result);
        }
        #endregion "PO-Association"

        #region "GRN"

        [HttpPost("GetGRNData")]
        public async Task<IActionResult> GetGRNData(GRNDto productdto)
        {
            var result = await _salesRepository.GetGRNData(productdto);
            return Ok(result);
        }
        [HttpPost("SaveGRNDATA")]
        public async Task<IActionResult> SaveGRNDATA(SaveGRNDto wsDt)
        {
            var result = await _salesRepository.SaveGRNDATA(wsDt);
            return Ok(result);
        }
        #endregion "GRn"

        #region "Production Out"

        [HttpPost("GetProductionOutData")]
        public async Task<IActionResult> GetProductionOutData(ProductionOutDto productdto)
        {
            var result = await _salesRepository.GetProductionOutData(productdto);
            return Ok(result);
        }

        #endregion "Production Out"

    
      }
}