using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.MTrack;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using BoldReports.RDL.DOM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Caching.Memory;
//using Newtonsoft.Json;
using Type = API.Entities.Type;

namespace API.Controllers.CCSystem.Master
{
    [Authorize]
    public class MasterController : BaseApiController
    {
        private readonly IMasterRepository _masterRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationCartonDbContext _context;

        private readonly IMTrackMasterRepository _mTrackMasterRepository;

        public MasterController(IMasterRepository masterRepository, IApplicationCartonDbContext context, IMapper mapper
        , IMTrackMasterRepository mTrackMasterRepository)
        {
            _mapper = mapper;
            _context = context;
            _masterRepository = masterRepository;
            _mTrackMasterRepository = mTrackMasterRepository;
        }

        #region "UserLocation"

        [HttpPost("Loc/SetDefault")]
        public async Task<IActionResult> SetDefaultLocation(MstrUserLocation userLoc)
        {
            var result = await _masterRepository.SetDefaultLocationAsync(userLoc);
            return Ok(result);
        }

        #endregion "UserLocation"

        #region "Color"

        [HttpGet("Color")]
        public async Task<IActionResult> GetColor()
        {
            var result = await _context.MstrColor
                .Select(x => new { x.AutoId, x.Code, x.Name }).ToListAsync();
            return Ok(result);
        }

        [HttpPost("Color")]
        public async Task<ActionResult> SaveColor(MstrColor color)
        {
            var result = await _masterRepository.SaveColorAsync(color);
            return Ok(result);
        }

        [HttpGet("ColorCard")]
        public async Task<IActionResult> GetColorCard()
        {
            var result = await _context.MstrColorCard.ToListAsync();
            return Ok(result);
        }

        [HttpPost("ColorCard")]
        public async Task<ActionResult> SaveColorCard(MstrColorCard colorCard)
        {
            var result = await _masterRepository.SaveColorCardAsync(colorCard);
            return Ok(result);
        }

        [HttpPost("ColorCard/Deactive")]
        public async Task<ActionResult> DeactiveColorCard(MstrColorCard colorCard)
        {
            var result = await _masterRepository.DeactiveColorCardAsync(colorCard);
            return Ok(result);
        }

        #endregion "Color"

        #region Color Allocation

        [HttpGet("ColorAlloc/{id}")]
        public async Task<IActionResult> GetColorAllocationDetails(int id)
        {
            var result = await _masterRepository.GetColorAllocDetailsAsync(id);
            return Ok(result);
        }

        [HttpPost("SaveColorAll")]
        public async Task<IActionResult> SaveColorAllocation(List<MstrColorAllocCard> colorAlloc)
        {
            var result = await _masterRepository.SaveColorAllocationAsync(colorAlloc);
            return Ok(result);
        }

        [HttpPost("DelColorAll")]
        public async Task<IActionResult> DeleteColorAllocation(List<MstrColorAllocCard> colorAlloc)
        {
            var result = await _masterRepository.DeleteColorAllocationAsync(colorAlloc);
            return Ok(result);
        }

        #endregion Color Allocation

        #region Size Allocation

        [HttpGet("SizeAlloc/{id}")]
        public async Task<IActionResult> GetSizeAllocDetails(int id)
        {
            var result = await _masterRepository.GetSizeAllocDetailsAsync(id);
            return Ok(result);
        }

        [HttpPost("SaveSizeAll")]
        public async Task<IActionResult> SaveSizeAllocation(List<MstrSizeAllocCard> sizeAlloc)
        {
            var result = await _masterRepository.SaveSizeAllocationAsync(sizeAlloc);
            return Ok(result);
        }

        [HttpPost("DelSizeAll")]
        public async Task<IActionResult> DeleteSizeAllocation(List<MstrSizeAllocCard> sizeAlloc)
        {
            var result = await _masterRepository.DeleteSizeAllocationAsync(sizeAlloc);
            return Ok(result);
        }

        #endregion Size Allocation

        #region "Size"

        [HttpGet("SizeCard")]
        public async Task<IActionResult> GetSizeCard()
        {
            var result = await _context.MstrSizeCard.ToListAsync();
            return Ok(result);
        }

        [HttpGet("Size")]
        public async Task<IActionResult> GetSize()
        {
            var result = await _context.MstrSize.ToListAsync();
            return Ok(result);
        }

        [HttpPost("SizeCard/Deactive")]
        public async Task<ActionResult> DeactiveSizeCard(MstrSizeCard sizeCard)
        {
            var result = await _masterRepository.DeactiveSizeCardAsync(sizeCard);
            return Ok(result);
        }

        [HttpPost("SizeCard")]
        public async Task<ActionResult> SaveSizeCard(MstrSizeCard sizeCard)
        {
            var result = await _masterRepository.SaveSizeCardAsync(sizeCard);
            return Ok(result);
        }

        [HttpPost("Size")]
        public async Task<ActionResult> SaveSize(MstrSize size)
        {
            var result = await _masterRepository.SaveSizeAsync(size);
            return Ok(result);
        }

        #endregion "Size"

        #region "Company"

        [HttpGet("Company/{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var result = await _context.MstrCompany
                .Join(_context.MstrCurrency, c => c.DefCurrencyId, a => a.AutoId
                    , (c, a) => new
                    {
                        autoId = c.AutoId,
                        address = c.Address,
                        companyName = c.CompanyName,
                        defaultCurrency = a.Code,
                        defCurrencyId = c.DefCurrencyId
                    })
                .ToListAsync();
            return Ok(result);
        }

        #endregion "Company"

        #region "SupplierType"
        [HttpGet("SupT")]
        public async Task<IActionResult> GetSupplierType()
        {
            var result = await _context.MstrSupplierType
            .Select(x => new { x.Description, x.SuppTypeId })
            .ToListAsync();
            return Ok(result);
        }
        #endregion "SupplierType"

        #region  "Article"

        [HttpGet("Articles")]
        public async Task<IActionResult> GetArticles()
        {
            var articleList = await _context.MstrArticle
                .Join(_context.MstrCategory, a => a.CategoryId, c => c.AutoId
                    , (a, c) => new { a, c })
                .Join(_context.MstrUnits, au => au.a.StorageUnitId, u => u.AutoId,
                    (au, u) => new { au, u })
                .Join(_context.MstrProductType, ap => ap.au.a.ProTypeId, p => p.AutoId,
                    (ap, p) => new { ap, p })
                .Join(_context.MstrProductGroup, aps => aps.ap.au.a.ProGroupId, s => s.AutoId,
                    (aps, s) => new { aps, s })
                .Join(_context.MstrUnits, am => am.aps.ap.au.a.MeasurementId, m => m.AutoId
                     , (am, m) =>
                     new
                     {
                         autoId = am.aps.ap.au.a.AutoId,
                         isActive = am.aps.ap.au.a.IsActive,
                         unitCode = am.aps.ap.u.Code,
                         catCode = am.aps.ap.au.c.Code,
                         prodTypeCode = am.aps.p.ProdTypeCode,
                         prodGroupCode = am.s.ProdGroupCode,
                         unitId = am.aps.ap.au.a.StorageUnitId,
                         measurementId = am.aps.ap.au.a.MeasurementId,
                         measurement = m.Code,
                         categoryId = am.aps.ap.au.a.CategoryId,
                         proTypeId = am.aps.ap.au.a.ProTypeId,
                         proGroupId = am.aps.ap.au.a.ProGroupId,
                         articleName = am.aps.ap.au.a.ArticleName + " (" + m.Code + ")",
                         stockCode = am.aps.ap.au.a.StockCode,
                         // height = am.aps.ap.au.a.Height,
                         // width = am.aps.ap.au.a.Width,
                         // length = am.aps.ap.au.a.Length,
                         // GSM = am.aps.ap.au.a.GSM,                        
                         // rollWidth = am.aps.ap.au.a.RollWidth,
                         description1 = am.aps.ap.au.a.Description1,
                         description2 = am.aps.ap.au.a.Description2,
                         avgCostPrice = am.aps.ap.au.a.AvgCostPrice,
                         maxCostPrice = am.aps.ap.au.a.MaxCostPrice,
                         lastCostPrice = am.aps.ap.au.a.LastCostPrice
                     }).Where(b => b.isActive == true)
                    .ToListAsync();
            return Ok(articleList);
        }

        [HttpGet("ArtDt/{categoryId}")]
        public async Task<IActionResult> GetArticlesColorSize(int categoryId)
        {
            var articleList = await _context.MstrArticleColorSize
                .Join(_context.MstrArticle, a => a.ArticleId, c => c.AutoId
                    , (a, c) => new { a, c })
                .Join(_context.MstrUnits, m => m.c.StorageUnitId, u => u.AutoId
                    , (m, u) => new { m, u })
                .Join(_context.MstrColor, e => e.m.a.ColorId, c => c.AutoId
                    , (e, c) => new { e, c })
                .Join(_context.MstrSize, g => g.e.m.a.SizeId, s => s.AutoId
                    , (g, s) =>
                     new
                     {
                         artColorSizeId = g.e.m.a.AutoId,
                         articleId = g.e.m.a.ArticleId,
                         isActive = g.e.m.c.IsActive,
                         unit = g.e.u.Code,
                         uomId = g.e.m.c.StorageUnitId,
                         categoryId = g.e.m.c.CategoryId,
                         articleName = g.e.m.c.ArticleName,
                         stockCode = g.e.m.c.StockCode,
                         description1 = g.e.m.c.Description1,
                         description2 = g.e.m.c.Description2,
                         size = s.Code,
                         color = g.c.Code,
                         colorId = g.e.m.a.ColorId,
                         sizeId = g.e.m.a.SizeId,
                         //avgCostPrice = g.e.m.a.AvgCostPrice,
                         //maxCostPrice = g.e.m.a.MaxCostPrice,
                         //lastCostPrice = g.e.m.a.LastCostPrice
                     }).Where(b => b.isActive == true && b.categoryId == categoryId)
                    .ToListAsync();
            return Ok(articleList);
        }

        [HttpGet("CompArti/{companyId}")]
        public async Task<IActionResult> GetArticleAllDetails(int companyId)
        {
            var articleList = await _masterRepository.GetArtileDetailsAllAsync(companyId);
            return Ok(articleList);
        }

        [HttpGet("CompArtic/{companyId}")]
        public async Task<IActionResult> GetActiveArticleAllDetails(int companyId)
        {
            var articleList = await _masterRepository.GetActiveArtileDetailsAllAsync(companyId);
            return Ok(articleList);
        }

        [HttpGet("CCArticle")]
        public async Task<IActionResult> GetCCardArticle()
        {
            var result = await _context.MstrArticle
                .Where(x => x.ColorCardId > 0 && x.IsActive == true)
                .Join(_context.MstrColorCard, x => x.ColorCardId, c => c.AutoId
                , (x, c) => new
                {
                    autoId = x.AutoId,
                    articleName = x.ArticleName,
                    stockCode = x.StockCode,
                    colorCardId = x.ColorCardId,
                    colorCard = c.Name
                }).ToListAsync();
            return Ok(result);
        }

        [HttpGet("RCArticle")]
        public async Task<IActionResult> GetRCardArticle()
        {
            var result = await _context.MstrArticle
                .Where(x => x.ColorCardId > 0 && x.IsActive == true)
                .Join(_context.MstrColorCard, x => x.ColorCardId, c => c.AutoId
                , (x, c) => new
                {
                    autoId = x.AutoId,
                    articleName = x.ArticleName,
                    stockCode = x.StockCode,
                    colorCardId = x.ColorCardId,
                    colorCard = c.Name
                }).OrderByDescending(x => x.autoId).Take(1).ToListAsync();
            return Ok(result);
        }

        [HttpGet("SCArticle")]
        public async Task<IActionResult> GetSCardArticle()
        {
            var result = await _context.MstrArticle
                .Where(x => x.SizeCardId > 0 && x.IsActive == true)
                .Join(_context.MstrSizeCard, x => x.SizeCardId, s => s.AutoId
                , (x, s) => new
                {
                    autoId = x.AutoId,
                    articleName = x.ArticleName,
                    stockCode = x.StockCode,
                    sizeCardId = x.SizeCardId,
                    sizeCard = s.Name
                }).ToListAsync();
            return Ok(result);
        }

        [HttpGet("DCArticle")]
        public async Task<IActionResult> GetDCardArticle()
        {
            var result = await _context.MstrArticle
                .Where(x => x.SizeCardId > 0 && x.IsActive == true)
                .Join(_context.MstrSizeCard, x => x.SizeCardId, s => s.AutoId
                , (x, s) => new
                {
                    autoId = x.AutoId,
                    articleName = x.ArticleName,
                    stockCode = x.StockCode,
                    sizeCardId = x.SizeCardId,
                    sizeCard = s.Name
                }).OrderByDescending(x => x.autoId).Take(1).ToListAsync();
            return Ok(result);
        }

        [HttpPost("ArtProdWise")]
        public async Task<IActionResult> GetArticleDetails(ArticleSerchDto article)
        {
            var articleList = await _masterRepository.GetArtileDetailsAsync(article);
            return Ok(articleList);
        }

        [HttpPost("CartArtProdWise")]
        public async Task<IActionResult> GetCartonArtileDetails(ArticleSerchDto article)
        {
            var articleList = await _masterRepository.GetCartonArtileDetailsAsync(article);
            return Ok(articleList);
        }

        [HttpGet("ArtiColor/{id}")]
        public async Task<IActionResult> GetArticleColor(int id)
        {
            var colorList = await _masterRepository.GetArticlColorAsync(id);
            return Ok(colorList);
        }

        [HttpGet("ArtiSize/{id}")]
        public async Task<IActionResult> GetArticleSize(int id)
        {
            var sizeList = await _masterRepository.GetArticlSizeAsync(id);
            return Ok(sizeList);
        }

        [HttpPost("SaveArticle")]
        public async Task<IActionResult> SaveArticle(SaveArticleDto article)
        {
            var result = await _masterRepository.SaveArticleAsync(article);
            return Ok(result);
        }

        [HttpPost("DAArticle")]
        public async Task<IActionResult> DeactiveArticle(MstrArticle article)
        {
            var result = await _masterRepository.DeactiveArticleAsync(article);
            return Ok(result);
        }

        [HttpPost("DABrandCode")]
        public async Task<IActionResult> DeactiveBrandCode(MstrBrandCode brandcode)
        {
            var result = await _masterRepository.DeactiveBrandCodeAsync(brandcode);
            return Ok(result);
        }

        [HttpPost("DAAddM")]
        public async Task<IActionResult> DeactiveAddChargeModule(MstrAddChargeModule addchargeM)
        {
            var result = await _masterRepository.DeactiveAddChargeModulesAsync(addchargeM);
            return Ok(result);
        }

        [HttpPost("DAStoreSite")]
        public async Task<IActionResult> DeactiveSotrSite(MstrStoreSite storeSite)
        {
            var result = await _masterRepository.DeactiveSotrSiteAsync(storeSite);
            return Ok(result);
        }

        [HttpPost("DelArticle")]
        public async Task<IActionResult> DeleteArticle(MstrArticle article)
        {
            var result = await _masterRepository.DeleteArticleAsync(article);
            return Ok(result);
        }

        [HttpGet("ArtPrice/{id}")]
        public async Task<IActionResult> GetArticlePriceList(int id)
        {
            var result = await _context.MstrArticle
                .Where(x => x.AutoId == id)
                .Select(x => new { x.AvgCostPrice, x.LastCostPrice, x.MaxCostPrice, x.SalesPrice })
                .SingleOrDefaultAsync();

            return Ok(result);
        }

        [HttpPost("SaveArtBC")]
        public async Task<IActionResult> SaveArticleBrandcode(MstrArticleBrandcode ArticleBrandcode)
        {
            var result = await _masterRepository.SaveArticleBrandcodeAsync(ArticleBrandcode);
            return Ok(result);
        }

        [HttpPost("DelArtBC")]
        public async Task<ActionResult> DeleteArticleBrandCodeMapping(MstrCustomerBrand cusBrand)
        {
            var result = await _masterRepository.DeleteArticleBrandCodeMappingAsync(cusBrand);
            return Ok(result);
        }

        [HttpGet("BCArticle/{brandCodeid}")]
        public async Task<IActionResult> GetBrandCodeArticle(int brandCodeid)
        {
            var result = await _context.MstrArticleBrandcode
                .Where(x => x.BrandCodeId == brandCodeid)
                .Join(_context.MstrArticle, x => x.ArticleId, a => a.AutoId
                    , (x, a) => new { x, a })
                .Join(_context.MstrColor, xc => xc.x.ColorId, c => c.AutoId
                    , (xc, c) => new { xc, c })
                .Join(_context.MstrSize, xs => xs.xc.x.SizeId, s => s.AutoId
                    , (xs, s) => new
                    {
                        autoId = xs.xc.x.AutoId,
                        articleName = xs.xc.a.ArticleName,
                        colorname = xs.c.Name,
                        sizename = s.Name,
                        articleId = xs.xc.a.AutoId,
                        colorId = xs.c.AutoId,
                        sizeId = s.AutoId
                    }).ToListAsync();
            return Ok(result);
        }
        #endregion "Article"

        #region Assign Article Color

        [HttpGet("GetAtiClr/{id}")]
        public async Task<IActionResult> getArtColorPermitDt(int id)
        {
            var result = await _masterRepository.getArtColorPermitDtAsync(id);
            return Ok(result);
        }

        [HttpPost("SaveArtColor")]
        public async Task<IActionResult> SaveArticleColor(List<MstrArticleColor> colors)
        {
            var result = await _masterRepository.SaveArticleColorAsync(colors);
            return Ok(result);
        }

        [HttpPost("DelArtColor")]
        public async Task<IActionResult> DeleteArticleColor(List<MstrArticleColor> colors)
        {
            var result = await _masterRepository.DeleteArticleColorAsync(colors);
            return Ok(result);
        }

        #endregion Assign Article Color

        #region Assign Article Size

        [HttpGet("GetAtiSize/{id}")]
        public async Task<IActionResult> getArtSizePermitDt(int id)
        {
            var result = await _masterRepository.getArtSizePermitDtAsync(id);
            return Ok(result);
        }

        [HttpPost("SaveArtSize")]
        public async Task<IActionResult> SaveArticleSize(List<MstrArticleSize> size)
        {
            var result = await _masterRepository.SaveArticleSizeAsync(size);
            return Ok(result);
        }

        [HttpPost("DelArtSize")]
        public async Task<IActionResult> DeleteArticleSize(List<MstrArticleSize> size)
        {
            var result = await _masterRepository.DeleteArticleSizeAsync(size);
            return Ok(result);
        }

        #endregion Assign Article Size

        #region "Article UOM Conversion"

        [HttpGet("ArtBase/{id}")]
        public async Task<IActionResult> GetArticleUOMConversion(int id)
        {
            var result = await _context.MstrArticleUOMConversion
                .Where(x => x.ArticleId == id && x.IsActive == true)
                .Select(x => new { x.AutoId, x.ArticleId, x.UnitId, x.Value, x.Version })
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("ArtBaseAll/{id}")]
        public async Task<IActionResult> GetArticleUOMConversionAll(int id)
        {
            var result = await _context.MstrArticleUOMConversion
                .Where(x => x.ArticleId == id)
                .Join(_context.MstrArticle, u => u.ArticleId, a => a.AutoId, (u, a) => new { u, a })
                .Join(_context.MstrUnits, x => x.u.UnitId, b => b.AutoId, (x, b) =>
                 new
                 {
                     articleName = x.a.ArticleName,
                     autoId = x.u.AutoId,
                     articleId = x.u.ArticleId,
                     unitId = x.u.UnitId,
                     value = x.u.Value,
                     version = x.u.Version,
                     isActive = x.u.IsActive,
                     unit = b.Code
                 })
                .ToListAsync();

            return Ok(result);
        }

        [HttpPost("SaveAUOM")]
        public async Task<IActionResult> SaveArticleUOMConv(MstrArticleUOMConversion uOMConversion)
        {
            var result = await _masterRepository.SaveArticleUOMConvAsync(uOMConversion);
            return Ok(result);
        }

        [HttpPost("ActiveAUOM")]
        public async Task<IActionResult> ActiveArticleUOMConv(MstrArticleUOMConversion uOMConversion)
        {
            var result = await _masterRepository.ActiveArticleUOMConvAsync(uOMConversion);
            return Ok(result);
        }

        #endregion "Article UOM Conversion"

        #region "Special Instruction"

        [HttpGet("SpeInst")]
        public async Task<IActionResult> GetSpecialInstruction()
        {
            var result = await _context.MstrSpecialInstruction
                    .Select(x => new { x.AutoId, x.Description })
                    .ToListAsync();
            return Ok(result);
        }

        [HttpPost("SaveSpeInst")]
        public async Task<IActionResult> SaveSpecialInstruction(MstrSpecialInstruction specialInstruction)
        {
            var result = await _masterRepository.SaveSpecialInstructionAsync(specialInstruction);
            return Ok(result);
        }

        #endregion "Special Instruction"

        #region "CodeDefinition"

        [HttpPost("CodeDef")]
        public async Task<IActionResult> GetCodeSettingDetails(MstrCodeDefinition codeDef)
        {
            var result = await _context.MstrCodeDefinition
                .Where(x => x.CategoryId == codeDef.CategoryId && x.ProdTypeId == codeDef.ProdTypeId
                            && x.ProdGroupId == codeDef.ProdGroupId)
                .Select(x => new
                {
                    x.AutoId,
                    x.FlexFieldId,
                    x.FieldName,
                    x.IsCode,
                    x.IsCounter,
                    x.IsName,
                    x.IsValue,
                    x.IsSeperator,
                    x.Seperator,
                    x.CounterPad,
                    x.CounterStart,
                    x.IsProductField,
                    x.SeqNo,
                    x.SortOrder
                })
                .ToListAsync();
            return Ok(result);
        }

        [HttpPost("SaveCDef")]
        public async Task<IActionResult> SaveCodeDefinition(MstrCodeDefinition codeDefinition)
        {
            var result = await _masterRepository.SaveCodeDefinitionAsync(codeDefinition);
            return Ok(result);
        }

        [HttpPost("DeleteCDef")]
        public async Task<IActionResult> DeleteCodeDefinition(MstrCodeDefinition codeDefinition)
        {
            var result = await _masterRepository.DeleteCodeDefinitionAsync(codeDefinition);
            return Ok(result);
        }

        #endregion "CodeDefinition"

        #region "CustomerHeader"
        [HttpGet("Cust")]
        public async Task<IActionResult> GetCus()
        {
            var customerList = await _context.MstrCustomerHeader
            .Select(x => new { x.ShortCode, x.Name, x.AutoId, x.bActive })
            .ToListAsync();
            return Ok(customerList);
        }

        [HttpGet("Customer/{locId}")]
        public async Task<IActionResult> GetCustomer(int locId)
        {
            var customerList = await _context.MstrCustomerHeader
                .Where(x => x.LocationId == locId)
                .Select(x => new { x.ShortCode, x.Name, x.AutoId, x.bActive })
                .ToListAsync();
            return Ok(customerList);
        }

        [HttpGet("CusAll/{LocId}")]
        public async Task<IActionResult> GetCustomerHeaderAll(int LocId)
        {
            var result = await _masterRepository.GetCustomerHdAllAsync(LocId);
            return Ok(result);
        }

        [HttpPost("CustHdDeactive")]
        public async Task<ActionResult> DeactiveCustomerHeader(MstrCustomerHeader mstrCustomerHeader)
        {
            var result = await _masterRepository.DeactiveCustomerHdAsync(mstrCustomerHeader);
            return Ok(result);
        }

        [HttpPost("SaveCustomerHd")]
        public async Task<ActionResult> SaveCustomerHeader(MstrCustomerHeader MstrCustomerHeader)
        {
            var result = await _masterRepository.SaveCustomerHdAsync(MstrCustomerHeader);
            return Ok(result);
        }

        #endregion "CustomerHeader"

        #region "CustomerLocation"       


        [HttpGet("CustomerLocs/{id}")]
        public async Task<IActionResult> GetCustomerLocs(int id)
        {
            var customerList = await _context.MstrCustomerLocation
                    //.Where(x => x.CustomerId == id && x.bActive == true)
                    .Where(x => x.CustomerId == id)
                    .Select(x => new { x.ShortCode, x.Name, x.AutoId, x.bActive })
                    .ToListAsync();
            return Ok(customerList);
        }

        [HttpGet("CustomerLoc/{id}")]
        public async Task<IActionResult> GetCustomerLoc(int id)
        {
            var customerList = await _context.MstrCustomerLocation
                    .Where(x => x.CustomerId == id && x.bActive == true)
                    .Select(x => new { x.ShortCode, x.Name, x.AutoId })
                    .ToListAsync();
            return Ok(customerList);
        }

        [HttpPost("DeactCusLoc")]
        public async Task<ActionResult> DeactiveCustomerLocation(MstrCustomerLocation customerLoc)
        {
            var result = await _masterRepository.DeactiveCusLocAsync(customerLoc);
            return Ok(result);
        }

        [HttpPost("SaveCustomerLoc")]
        public async Task<ActionResult> SaveCustomerLoc(MstrCustomerLocation customerLoc)
        {
            var result = await _masterRepository.SaveCustomerLocAsync(customerLoc);
            return Ok(result);
        }

        #endregion "CustomerLocation"

        #region "CustomerUser"

        [HttpGet("CustomerUser/{id}")]
        public async Task<IActionResult> GetCustomerUser(int id)
        {
            var customerList = await _context.MstrCustomerUsers
                    .Where(x => x.CustomerId == id)
                    .Select(x => new { Name = x.Title + ' ' + x.FirstName + ' ' + x.LastName, x.AutoId })
                    .ToListAsync();
            return Ok(customerList);
        }

        [HttpGet("CustomerUser/All/{id}")]
        public async Task<IActionResult> GetCustomerUserAllDt(int id)
        {
            var customerList = await _context.MstrCustomerUsers
                    .Where(x => x.CustomerId == id)
                    .ToListAsync();
            return Ok(customerList);
        }

        [HttpPost("SaveCusUser")]
        public async Task<IActionResult> SaveCustomerUser(MstrCustomerUsers customerUser)
        {
            var result = await _masterRepository.SaveCustomerUserAsync(customerUser);
            return Ok(result);
        }

        [HttpPost("CusUser/Deactive")]
        public async Task<IActionResult> DeactiveCustomerUser(MstrCustomerUsers cusUser)
        {
            var result = await _masterRepository.DeactiveCustomerUserAsync(cusUser);
            return Ok(result);
        }

        #endregion "CustomerUser"

        #region "CustomerCurrency"

        [HttpGet("CusCurrency/{id}")]
        public async Task<IActionResult> GetCusCurrency(int id)
        {
            var currencyList = await _context.MstrCustomerCurrency
                .Where(x => x.CustomerId == id)
                .Join(_context.MstrCurrency, m => m.CurrencyId, c => c.AutoId
                    , (m, c) =>
                    new
                    {
                        autoId = m.AutoId,
                        currencyId = m.CurrencyId,
                        code = c.Code,
                        name = c.Name,
                        symbol = c.Symbol
                    })
                .ToListAsync();

            return Ok(currencyList);
        }

        [HttpPost("SaveCusC")]
        public async Task<IActionResult> SaveCusCurrency(MstrCustomerCurrency cusCurrency)
        {
            var result = await _masterRepository.SaveCustomerCurrencyAsync(cusCurrency);
            return Ok(result);
        }

        [HttpPost("DeleteCusC")]
        public async Task<IActionResult> DeleteCusCurrency(MstrCustomerCurrency cusCurrency)
        {
            var result = await _masterRepository.DeleteCusCurrencyAsync(cusCurrency);
            return Ok(result);
        }

        #endregion "CustomerCurrency"

        #region "CustomerBrand"

        [HttpGet("CusBrand/{customerId}")]
        public async Task<IActionResult> GetCustomerBrand(int customerId)
        {
            var brandList = await _context.MstrCustomerBrand
                .Where(x => x.CustomerId == customerId)
                .Join(_context.MstrBrand, c => c.BrandId, b => b.AutoId
                    , (c, b) =>
                    new
                    {
                        autoId = c.AutoId,
                        brand = b.Name,
                        brandId = c.BrandId
                    })
                .ToListAsync();
            return Ok(brandList);
        }

        [HttpPost("SaveCB")]
        public async Task<IActionResult> SaveCustomerBrand(MstrCustomerBrand cusBrand)
        {
            var result = await _masterRepository.SaveCustomerBrandAsync(cusBrand);
            return Ok(result);
        }

        [HttpPost("DeleteCB")]
        public async Task<IActionResult> DeleteCusBrand(MstrCustomerBrand cusBrand)
        {
            var result = await _masterRepository.DeleteCusBrandAsync(cusBrand);
            return Ok(result);
        }


        #endregion "CustomerBrand"

        #region "CustomerDivision"

        [HttpGet("CusDivision/{id}")]
        public async Task<IActionResult> GetCustomerDivision(int id)
        {
            var divisionList = await _context.MstrCustomerDivision
                .Where(x => x.CustomerId == id)
                .Select(x => new { x.Details, x.AutoId, x.bActive })
                .ToListAsync();

            return Ok(divisionList);
        }

        [HttpPost("SaveCD")]
        public async Task<IActionResult> SaveCustomerDivision(MstrCustomerDivision cusDivision)
        {
            var result = await _masterRepository.SaveCustomerDivisionAsync(cusDivision);
            return Ok(result);
        }

        [HttpPost("DisableCD")]
        public async Task<IActionResult> DisableCustomerDivision(MstrCustomerDivision cusDivision)
        {
            var result = await _masterRepository.DisableCusDivisionAsync(cusDivision);
            return Ok(result);
        }

        #endregion "CustomerDivision"

        #region "CustomerAddressList"

        [HttpPost("SaveCusAddress")]
        public async Task<IActionResult> SaveCustomerAddressList(MstrCustomerAddressList cusAddressList)
        {
            var result = await _masterRepository.SaveCusAddressAsync(cusAddressList);
            return Ok(result);
        }

        [HttpGet("CusAddress/{customerId}")]
        public async Task<IActionResult> GetCustomerAddressList(int customerId)
        {
            var result = await _masterRepository.GetCustomerAddressAsync(customerId);
            return Ok(result);
        }

        //[HttpGet("CusAddr/{customerId}")]
        //public async Task<IActionResult> GetActiveCustomerAddressList(int customerId)
        //{
        //    var result = await _masterRepository.GetActiveCustomerAddressAsync(customerId);
        //    return Ok(result);
        //}

        [HttpPost("DeactCusAdd")]
        public async Task<IActionResult> DeactiveCustomerAddList(MstrCustomerAddressList cusAddressList)
        {
            var result = await _masterRepository.DeactiveCustomerAddAsync(cusAddressList);
            return Ok(result);
        }

        #endregion "CustomerAddressList"

        #region "AddressType"

        [HttpGet("AddressType")]
        public async Task<IActionResult> GetAddressType()
        {
            var addressList = await _context.MstrAddressType
                .Select(x => new { x.AddressCode, x.AddressCodeName, x.AutoId })
                .ToListAsync();
            return Ok(addressList);
        }

        [HttpPost("SaveAddType")]
        public async Task<ActionResult> SaveAddressType(MstrAddressType addressType)
        {
            var result = await _masterRepository.SaveAddressTypeAsync(addressType);
            return Ok(result);
        }

        #endregion "AddressType"

        #region "Unit"

        [HttpPost("Editunits")]
        public async Task<ActionResult> saveUnits(MstrUnits units)
        {
            var result = await _masterRepository.SaveUnitAsync(units);
            return Ok(result);
        }

        [HttpGet("Units")]
        public async Task<IActionResult> GetUnit()
        {
            var result = await _context.MstrUnits.ToListAsync();
            return Ok(result);
        }

        [HttpPost("saveunits")]
        public async Task<ActionResult> Register(MstrUnitsDto MstrUnitsDto)
        {
            var user = _mapper.Map<MstrUnits>(MstrUnitsDto);

            user.Code = MstrUnitsDto.Code;
            user.Name = MstrUnitsDto.Name;

            _context.MstrUnits.Add(user);
            await _context.SaveChangesAsync(default);

            return Ok();
        }

        #endregion "Unit"

        #region "Unit Conversion"

        [HttpGet("UnitConv")]
        public async Task<ActionResult> GetUnitConversionDt()
        {
            var result = await _context.UnitConversion
                .Join(_context.MstrUnits, c => c.FromUnitId, f => f.AutoId,
                    (c, f) => new { c, f })
                .Join(_context.MstrUnits, tc => tc.c.ToUnitId, t => t.AutoId,
                    (tc, t) => new
                    {
                        autoId = tc.c.AutoId,
                        toUnit = t.Code,
                        fromUnitId = tc.c.FromUnitId,
                        toUnitId = tc.c.ToUnitId,
                        fromUnit = tc.f.Code,
                        value = tc.c.Value
                    }).ToListAsync();

            return Ok(result);
        }

        [HttpPost("SaveUC")]
        public async Task<ActionResult> SaveUnitConversion(MstrUnitConversion unitConv)
        {
            var result = await _masterRepository.SaveUnitConversionAsync(unitConv);
            return Ok(result);
        }

        #endregion "Unit Conversion"

        #region "Flute Type"

        [HttpPost("SaveFT")]
        public async Task<ActionResult> SaveFluteType(MstrFluteTypes fluteTypes)
        {
            var result = await _masterRepository.SaveFluteTypeAsync(fluteTypes);
            return Ok(result);
        }

        #endregion "FLute Type"

        #region "Process"

        [HttpPost("SaveProcess")]
        public async Task<ActionResult> saveProcess(MstrProcess MstrProcess)
        {
            var result = await _masterRepository.SaveProcessAsync(MstrProcess);
            return Ok(result);
        }

        [HttpGet("Process/{id}")]
        public async Task<IActionResult> GetProcess(int id)
        {
            var result = await _context.MstrProcess
                .Select(x => new { x.AutoId, x.Process, x.LocationId })
                .Where(x => x.LocationId == id)
                .ToListAsync();
            return Ok(result);
        }

        #endregion "Process"

        #region "StoreSite"

        [HttpPost("SaveStoreSite")]
        public async Task<ActionResult> saveStoreSite(MstrStoreSite MstrStoreSite)
        {
            var result = await _masterRepository.SaveStoresiteAsync(MstrStoreSite);
            return Ok(result);
        }

        [HttpGet("Storesite")]
        public async Task<IActionResult> GetStoresite()
        {
            var result = await _context.MstrStoreSite.ToListAsync();
            return Ok(result);
        }

        #endregion "StoreSite"

        #region  "Location"

        [HttpGet("MasterLocation")]
        public async Task<IActionResult> GetMstrLocation()
        {
            var result = await _context.MstrLocation.ToListAsync();
            return Ok(result);
        }

        #endregion "Location"

        #region "Category"

        [HttpPost("SaveCategory")]
        public async Task<ActionResult> saveCategory(MstrCategory Category)
        {
            var result = await _masterRepository.SaveCategoryAsync(Category);
            return Ok(result);
        }

        [HttpGet("Category")]
        public async Task<IActionResult> GetCategory()
        {
            var result = await _context.MstrCategory.ToListAsync();
            return Ok(result);
        }

        #endregion "Category"   

        #region "MaterialType"   

        [HttpGet("MaterialType")]
        public async Task<IActionResult> GetMaterialType()
        {
            var result = await _context.MstrMaterialType.ToListAsync();
            return Ok(result);
        }

        [HttpPost("SaveMaterialType")]
        public async Task<ActionResult> SaveMaterialType(MstrMaterialType MaterialType)
        {
            var result = await _masterRepository.SaveMaterialTypeAsync(MaterialType);
            return Ok(result);
        }

        #endregion "MaterialType"

        #region "Brand"

        /// GET LOCATION BAESED BRAND ONLY
        [HttpGet("Brand/{LocId}")]
        public async Task<IActionResult> GetBrand(int LocId)
        {
            var result = await _context.MstrBrand
                    .Where(x => x.LocationId == LocId)
                    .ToListAsync();
            return Ok(result);
        }

        [HttpPost("SaveBrand")]
        public async Task<ActionResult> saveProcess(MstrBrand MstrBrand)
        {
            var result = await _masterRepository.SaveBrandAsync(MstrBrand);
            return Ok(result);
        }

        [HttpGet("BrandCode")]
        public async Task<IActionResult> GetBrandCode()
        {
            var result = await _context.MstrBrandCode
            .Select(x => new { x.AutoId, x.BrandId, x.Name }).ToListAsync();
            return Ok(result);
        }

        [HttpGet("BrandCodes/{id}")]
        public async Task<IActionResult> GetBrandCode(int id)
        {
            var result = await _context.MstrBrandCode
                .Select(x => new { x.AutoId, x.BrandId, x.Name, x.IsActive })
                .Where(e => e.BrandId == id).ToListAsync();
            return Ok(result);
        }

        [HttpGet("BrandCode/{brandid}")]
        public async Task<IActionResult> GetBrandCodeforCosSales(int brandid)
        {
            var result = await _context.MstrBrandCode
                .Select(x => new { x.AutoId, x.BrandId, x.Name, x.IsActive })
                .Where(e => e.BrandId == brandid && e.IsActive == true).ToListAsync();
            return Ok(result);
        }

        [HttpPost("SaveBrandCode")]
        public async Task<ActionResult> saveProcess(MstrBrandCode MstrBrandCode)
        {
            var result = await _masterRepository.SaveBrandCodeAsync(MstrBrandCode);
            return Ok(result);
        }

        [HttpGet("BrandAll/{id}")]
        public async Task<IActionResult> GetBrandAllocDetails(int id)
        {
            var result = await _masterRepository.getBrandAllocDetailsAsync(id);
            return Ok(result);
        }


        [HttpPost("saveBrandAll")]
        public async Task<IActionResult> saveBrandAllocation(List<MstrFASubToMainCategoryAll> brandAlloc)
        {
            var result = await _masterRepository.saveBrandAllocationAsync(brandAlloc);
            return Ok(result);
        }

        [HttpPost("delBrandToCat")]
        public async Task<IActionResult> deleteBrandToCategoryAllocation(List<MstrFASubToMainCategoryAll> brandToCateall)
        {
            var result = await _masterRepository.ddeleteBrandToCategoryAllocationAsync(brandToCateall);
            return Ok(result);
        }


        #endregion "Brand"

        #region "SalesCategory"

        [HttpGet("SalesCat")]
        public async Task<IActionResult> GetSalesCategory()
        {
            var customerList = await _context.MstrSalesCategory
                    .Select(x => new { x.Name, x.Code, x.AutoId })
                    .ToListAsync();
            return Ok(customerList);
        }

        #endregion "SalesCategory"     

        #region "PaymentTerms"

        [HttpGet("PayTerms")]
        public async Task<IActionResult> GetPaymentTerms()
        {
            var payTermList = await _context.MstrPaymentTerm
                .Select(x => new { x.Name, x.Code, x.AutoId })
                .ToListAsync();

            return Ok(payTermList);
        }

        [HttpPost("SavePT")]
        public async Task<IActionResult> SavePaymentTerms(MstrPaymentTerm paymentTerm)
        {
            var result = await _masterRepository.SavePaymentTermsAsync(paymentTerm);
            return Ok(result);
        }


        #endregion "PaymentTerms" 

        #region "Countries"

        [HttpGet("Countries")]
        public async Task<IActionResult> GetCountries()
        {
            var countryList = await _context.MstrCountries
                .Select(x => new { x.Name, x.Code, x.AutoId, x.Alpha3Code, x.Alpha2Code, x.Numeric })
                .ToListAsync();

            return Ok(countryList);
        }

        [HttpPost("SaveCou")]
        public async Task<IActionResult> SaveCountries(MstrCountries countries)
        {
            var result = await _masterRepository.SaveCountriesAsync(countries);
            return Ok(result);
        }

        #endregion "Countries"

        #region "Currency"

        [HttpGet("Currency")]
        public async Task<IActionResult> GetCurrency()
        {
            var currencyList = await _context.MstrCurrency
            .Select(x => new { x.AutoId, x.Code, x.Name, x.Symbol })
            .ToListAsync();

            return Ok(currencyList);
        }

        [HttpPost("SaveCurr")]
        public async Task<IActionResult> SaveCurrency(MstrCurrency currency)
        {
            var result = await _masterRepository.SaveCurrencyAsync(currency);
            return Ok(result);
        }

        #endregion "Currency"

        #region "SalesAgent"

        [HttpGet("SalesAgent/{id}")]
        public async Task<IActionResult> GetSalesAgent(int id)
        {
            var result = await _context.MstrSalesAgent
                .Where(x => x.LocationId == id)
                .Select(x => new { x.Name, x.AutoId, x.Email, x.bActive })
                .ToListAsync();

            return Ok(result);
        }

        [HttpPost("SaveSA")]
        public async Task<IActionResult> SaveSalesAgent(MstrSalesAgent salesAgent)
        {
            var result = await _masterRepository.SaveSalesAgentAsync(salesAgent);
            return Ok(result);
        }

        #endregion "SalesAgent"

        #region "ProductionDefinition"

        [HttpGet("ProdDefDt/{id}")]
        public async Task<ActionResult> GetProdDefinition(byte id)
        {
            var ProdDefList = await _masterRepository.GetProdDefinitionAsync(id);
            return Ok(ProdDefList);
        }

        [HttpGet("ProdDefList")]
        public async Task<ActionResult> GetProdDefinitionList()
        {
            var ProdDefList = await _context.MstrProdDefinitionHd
                .Select(x => new { x.AutoId, x.PDName }).ToListAsync();
            return Ok(ProdDefList);
        }

        [HttpPost("SaveProdDef")]
        public async Task<ActionResult> SaveProdDefinition(ProdDefinitionDto prodDefinitionDt)
        {
            var result = await _masterRepository.SaveProdDefinitionAsync(prodDefinitionDt);
            return Ok(result);
        }

        [HttpPost("DeleteProdDef")]
        public async Task<ActionResult> DeleteProdDefinition(ProdDefinitionDto prodDefinitionDt)
        {
            var result = await _masterRepository.DeleteProdDefinitionAsync(prodDefinitionDt);
            return Ok(result);
        }

        #endregion "ProductionDefinition"

        #region "Product Type"        

        [HttpGet("ProdType")]
        public async Task<ActionResult> GetProductTypeAll()
        {
            var ProdTypeList = await _context.MstrProductType
                .Where(x => x.IsActive == true)
                .Select(x => new { x.AutoId, x.ProdTypeCode, x.ProdTypeName, x.IsActive, x.bAutoArticle })
                .ToListAsync();
            return Ok(ProdTypeList);
        }

        [HttpGet("ProdType/{catId}")]
        public async Task<IActionResult> GetProcuctType(int catId)
        {
            var prodTypeList = await _context.MstrCatProductType
                .Where(x => x.CategoryId == catId)
                .Join(_context.MstrProductType, c => c.ProdTypeId, p => p.AutoId,
                    (c, p) => new { c, p })
                .Join(_context.MstrCategory, y => y.c.CategoryId, l => l.AutoId
                    , (y, l) => new
                    {
                        autoId = y.p.AutoId,
                        bAutoArticle = y.p.bAutoArticle,
                        categoryId = y.c.CategoryId,
                        prodTypeCode = y.p.ProdTypeCode,
                        prodTypeName = y.p.ProdTypeName,
                        isActive = y.p.IsActive,
                        categoryName = l.Name
                    })
                .ToListAsync();
            return Ok(prodTypeList);
        }

        [HttpPost("SaveProdType")]
        public async Task<ActionResult> SaveProductType(MstrProductType MstrProductType)
        {
            var result = await _masterRepository.SaveProductTypeAsync(MstrProductType);
            return Ok(result);
        }

        [HttpPost("Deactive/ProdType")]
        public async Task<ActionResult> DeactProductType(MstrProductType MstrProductType)
        {
            var result = await _masterRepository.DeactProductTypeAsync(MstrProductType);
            return Ok(result);
        }

        [HttpGet("CatProdT/{catId}")]
        public async Task<ActionResult> GetCatProdTypeDetails(int catId)
        {
            var result = await _masterRepository.GetCatProductTypeDtAsync(catId);
            return Ok(result);
        }

        [HttpPost("DeleteCatPT")]
        public async Task<ActionResult> DeleteCatProdType(List<MstrCatProductType> prod)
        {
            var result = await _masterRepository.DeleteCatProdTypeAsync(prod);
            return Ok(result);
        }

        [HttpPost("AssignCatPT")]
        public async Task<ActionResult> AssignCatProdType(List<MstrCatProductType> prod)
        {
            var result = await _masterRepository.AssignCatProdTypeAsync(prod);
            return Ok(result);
        }

        #endregion "Product Type"

        #region "Product Group"

        [HttpGet("PGroup/{id}")]
        public async Task<ActionResult> GetProductGroup(int id)
        {
            var ProdGroupList = await _masterRepository.GetProductGroupAsync(id);
            return Ok(ProdGroupList);
        }

        [HttpGet("PTGroup/{id}")]
        public async Task<ActionResult> GetProdTypeGroup(int id)
        {
            var ProdGroupList = await _masterRepository.GetProdTypeGroupAsync(id);
            return Ok(ProdGroupList);
        }

        [HttpPost("AssignPGroup")]
        public async Task<IActionResult> AssignProdTypeGroup(List<MstrProdTypeGroup> prod)
        {
            var result = await _masterRepository.AssignProdTypeGroupAsync(prod);
            return Ok(result);
        }

        [HttpPost("DeletePGroup")]
        public async Task<IActionResult> DeleteProdTypeGroup(List<MstrProdTypeGroup> prod)
        {
            var result = await _masterRepository.DeleteProdTypeGroupAsync(prod);
            return Ok(result);
        }


        [HttpGet("ProdGroup")]
        public async Task<IActionResult> GetProcuctGroupAll()
        {
            var result = await _context.MstrProductGroup
                // .Where(x => x.IsActive == true)
                .Select(x => new { x.AutoId, x.ProdGroupName, x.ProdGroupCode, x.IsActive, x.SerialNo })
                .ToListAsync();
            return Ok(result);
        }

        [HttpPost("SaveProdGroup")]
        public async Task<ActionResult> SaveProductGroup(MstrProductGroup MstrProductGroup)
        {
            var result = await _masterRepository.SaveProductGroupAsync(MstrProductGroup);
            return Ok(result);
        }

        [HttpPost("Deactive/ProdGroup")]
        public async Task<ActionResult> DeactiveProdGroup(MstrProductGroup MstrProductGroup)
        {
            var result = await _masterRepository.DeactiveProdGroupAsync(MstrProductGroup);
            return Ok(result);
        }

        #endregion "Product Group"

        #region "Product Sub Category"

        [HttpGet("PSubCat/{id}")]
        public async Task<ActionResult> GetProdSubCategory(int id)
        {
            var ProdSubCatList = await _masterRepository.GetProductSubCatAsync(id);
            return Ok(ProdSubCatList);
        }

        // [HttpGet("ProdSubCat")]
        // public async Task<IActionResult> GetProductSubCat()
        // {
        //     var result = await _context.MstrProductSubCat.ToListAsync();
        //     return Ok(result);
        // }

        [HttpPost("SaveProdSubCat")]
        public async Task<ActionResult> SaveProductSubCat(MstrProductSubCat MstrProductSubCat)
        {
            var result = await _masterRepository.SaveProductSubCatAsync(MstrProductSubCat);
            return Ok(result);
        }

        [HttpPost("Deactive/PSubCat")]
        public async Task<ActionResult> DeactiveProdSubCat(MstrProductSubCat MstrProductSubCat)
        {
            var result = await _masterRepository.DeactiveProdSubCatAsync(MstrProductSubCat);
            return Ok(result);
        }

        #endregion "Product Sub Category"    

        #region "Costing Group"

        [HttpPost("SaveCostGroup")]
        public async Task<ActionResult> SaveCostingGroup(MstrCostingGroup MstrCostingGroup)
        {
            var result = await _masterRepository.SaveCostGroupAsync(MstrCostingGroup);
            return Ok(result);
        }

        [HttpGet("CostingGroup/{locId}")]
        public async Task<IActionResult> GetCostingGroup(int locId)
        {
            var result = await _context.MstrCostingGroup
                .Where(x => x.LocationId == locId)
                .Select(x => new { x.AutoId, x.IsMaterialAllocated, x.Name })
                .OrderBy(x => x.Name).ToListAsync();
            return Ok(result);
        }

        #endregion "Costing Group"    

        #region "Flute Type"

        [HttpGet("FluteType/{id}")]
        public async Task<IActionResult> GetFluteTypeDt(int id)
        {
            var result = await _context.MstrFluteTypes
                .Where(x => x.LocationId == id && x.IsActive == true)
                .Select(x => new { x.AutoId, x.Code, x.Factor })
                .ToListAsync();

            return Ok(result);
        }

        #endregion "Flute Type"   

        #region "Sequence Settings"

        [HttpGet("SeqSettDt/{locId}")]
        public async Task<IActionResult> GetSequenceSettings(int locId)
        {
            var result = await _context.TransSequenceSettings
                .Where(x => x.LocationId == locId)
                .Select(x => new { x.TransType, x.Prefix, x.SeqLength, x.SeqNo, x.CurrentYear, x.AutoId }).ToListAsync();
            return Ok(result);
        }

        [HttpPost("SaveSeqSett")]
        public async Task<ActionResult> SaveSequenceSettings(TransSequenceSettings sequenceSett)
        {
            var result = await _masterRepository.SaveSequenceSetAsync(sequenceSett);
            return Ok(result);
        }

        #endregion "Sequence Settings"      

        #region Flex Field Details

        [HttpPost("SaveFlexFDt")]
        public async Task<IActionResult> SaveFlexFieldDetails(MstrFlexFieldDetails flexFieldDt)
        {
            var result = await _masterRepository.SaveFlexFieldDetailsAsync(flexFieldDt);
            return Ok(result);
        }

        [HttpGet("FlexFieldDt/{id}")]
        public async Task<IActionResult> GetFlexFieldDetails(int id)
        {
            var flexFieldDt = await _masterRepository.GetFlexFieldDtAsync(id);
            return Ok(flexFieldDt);
        }

        /// GET FLEX FIELD LIST RELATED TO CATEGORY AND PROD TYPE
        [HttpPost("FFListCatPT")]
        public async Task<IActionResult> GetFlexFieldCatTypeWise(MstrFlexFieldDetails ffDetails)
        {
            var result = await _context.MstrFlexFieldDetails
                .Where(x => x.CategoryId == ffDetails.CategoryId && x.ProdTypeId == ffDetails.ProdTypeId && x.isActive == true)
                .Select(x => new
                {
                    x.AutoId,
                    x.FlexFieldCode,
                    x.FlexFieldName,
                    x.Mandatory
                    ,
                    x.ModuleId,
                    x.ValueList,
                    x.DataType
                })
                .ToListAsync();

            return Ok(result);
        }

        //// GET ONLY VALUE LISTED FLEX FIELD LIST
        [HttpGet("FlexFldDt/Val")]
        public async Task<IActionResult> GetFlexFieldList()
        {
            var flexFieldDt = await _context.MstrFlexFieldDetails
                .Select(x => new { x.AutoId, x.FlexFieldCode, x.FlexFieldName, x.ValueList })
                .Where(x => x.ValueList == true).ToListAsync();
            return Ok(flexFieldDt);
        }

        [HttpPost("Deactive/FlexFldDt")]
        public async Task<IActionResult> DeactiveFlexFieldDt(MstrFlexFieldDetails flexFieldDt)
        {
            var result = await _masterRepository.DeactiveFlexFieldDtAsync(flexFieldDt);
            return Ok(result);
        }

        #endregion

        #region Flex Field ValueList

        [HttpGet("FFValList/{id}")]
        public async Task<IActionResult> GetFlexFieldValueList(int id)
        {
            var result = await _context.MstrFlexFieldValueList
                .Where(x => x.FlexFieldId == id)
                .Join(_context.MstrFlexFieldDetails, v => v.FlexFieldId, d => d.AutoId,
                   (v, d) => new
                   {
                       autoId = v.AutoId,
                       flexFieldId = v.FlexFieldId,
                       flexFeildVlaue = v.FlexFeildVlaue
                   }).ToListAsync();
            return Ok(result);
        }

        [HttpPost("SaveFFValList")]
        public async Task<IActionResult> SaveFlexFieldValueList(MstrFlexFieldValueList flexFieldval)
        {
            var result = await _masterRepository.SaveFlexFieldValListAsync(flexFieldval);
            return Ok(result);
        }

        [HttpPost("DeleteFFValList")]
        public async Task<IActionResult> DeleteFlexFieldValList(MstrFlexFieldValueList flexFieldval)
        {
            var result = await _masterRepository.DeleteFlexFieldValListAsync(flexFieldval);
            return Ok(result);
        }

        #endregion

        #region  Reject Reason

        [HttpGet("RejReason/{id}")]
        public async Task<IActionResult> GetRejectReason(int id)
        {
            var rejReasonList = await _context.MstrRejeReasons
                .Where(x => x.LocationId == id)
                .Select(x => new { x.AutoId, x.Details, x.IsActive })
                .ToListAsync();

            return Ok(rejReasonList);
        }

        [HttpPost("SaveRReason")]
        public async Task<IActionResult> SaveRejectReason(MstrRejectionReasons rejReason)
        {
            var result = await _masterRepository.SaveRejectReasonAsync(rejReason);
            return Ok(result);
        }

        #endregion Reject Reason

        #region Customer Type

        [HttpGet("CusType")]
        public async Task<IActionResult> GetCustomerType()
        {
            var result = await _context.MstrCustomerType
                    .Select(x => new { x.AutoId, x.Details })
                    .ToListAsync();
            return Ok(result);
        }
        [HttpPost("SaveCustType")]
        public async Task<IActionResult> SaveCustomerType(MstrCustomerType customerType)
        {
            var result = await _masterRepository.SaveCustomerTypeAsync(customerType);
            return Ok(result);
        }

        #endregion Customer Type

        #region Invoice Type

        [HttpGet("InvType")]
        public async Task<IActionResult> GetInvoiceType()
        {
            var result = await _context.MstrInvoiceType
                    .Select(x => new { x.AutoId, x.Details, x.FormatName })
                    .ToListAsync();
            return Ok(result);
        }
        [HttpPost("SaveInvType")]
        public async Task<IActionResult> SaveInvoiceType(MstrInvoiceType invoiceType)
        {
            var result = await _masterRepository.SaveInvoiceTypeAsync(invoiceType);
            return Ok(result);
        }
        #endregion Invoice Type

        #region Payment Mode

        [HttpGet("PayMode")]
        public async Task<IActionResult> GetPaymentMode()
        {
            var result = await _context.MstrPaymentMode
                    .Select(x => new { x.AutoId, x.Name })
                    .ToListAsync();
            return Ok(result);
        }
        [HttpPost("SavePayMode")]
        public async Task<IActionResult> SavePaymentMode(MstrPaymentMode paymentMode)
        {
            var result = await _masterRepository.SavePaymentModeAsync(paymentMode);
            return Ok(result);
        }
        #endregion Payment Mode

        #region Receipt Type

        [HttpGet("RecType")]
        public async Task<IActionResult> GetReceiptType()
        {
            var result = await _context.MstrReceiptType
                    .Select(x => new { x.AutoId, x.Name })
                    .ToListAsync();
            return Ok(result);
        }

        #endregion Receipt Type

        #region DispatchSite

        [HttpGet("DispatchSite")]
        public async Task<IActionResult> GetDispatchSite()
        {
            var result = await _context.MstrDispatchSite
                .Join(_context.MstrStoreSite, d => d.DispatchId, s => s.AutoId,
                 (d, s) =>
                 new
                 {
                     autoId = s.AutoId,
                     siteName = s.SiteName,
                     siteCode = s.SiteCode
                 }).ToListAsync();

            return Ok(result);
        }

        [HttpPost("SaveDisSite")]
        public async Task<IActionResult> SaveDispatchSite(MstrDispatchSite dispatchSite)
        {
            var result = await _masterRepository.SaveDispatchSiteAsync(dispatchSite);
            return Ok(result);
        }

        #endregion DispatchSite

        #region Master Company

        [HttpGet("GetCurrency")]
        public async Task<IActionResult> GetCurrencyName()
        {
            var result = await _context.MstrCurrency
                            .Select(x => new
                            {
                                autoId = x.AutoId,
                                name = x.Name
                            }).ToListAsync();
            return Ok(result);
        }

        [HttpGet("GetComp")]
        public async Task<IActionResult> GetCompany()
        {
            var result = await _context.MstrCompany
                        .Join(_context.MstrCurrency, Co => Co.DefCurrencyId, Cu => Cu.AutoId,
                        (Co, Cu) =>
                        new
                        {
                            autoId = Co.AutoId,
                            companyName = Co.CompanyName,
                            address = Co.Address,
                            svat = Co.SVATNo,
                            boiregNo = Co.BOIRegNo,
                            currencyName = Cu.Name,
                            currencyId = Co.DefCurrencyId
                        }).ToListAsync();
            return Ok(result);
        }

        [HttpPost("SaveComp")]
        public async Task<IActionResult> SaveCompany(MstrCompany company)
        {
            var result = await _masterRepository.SaveMasterCompanyAsync(company);
            return Ok(result);
        }

        #endregion Master Company

        #region SerialNoInventory
        [HttpPost("InvRefNo")]
        public async Task<IActionResult> GetInventoryRefNo(MstrSerialNoInventory serialNo)
        {
            var result = await _masterRepository.GetInventoryRefNoAsync(serialNo);
            return Ok(result);
        }

        #endregion SerialNoInventory

        #region User Sites
        [HttpGet("UserSites/{userId}")]
        public async Task<IActionResult> GetUserSite(int userId)
        {
            var result = await _context.MstrUserSite
                                .Where(x => x.TypeId == Type.DefaultSite && x.bActive == true)
                                .Join(_context.MstrStoreSite, x => x.SiteId, s => s.AutoId,
                                (x, s) =>
                                new
                                {
                                    autoId = s.AutoId,
                                    siteName = s.SiteName
                                }).ToListAsync();
            return Ok(result);
        }
        #endregion User Site

        #region User Master Settings
        [HttpGet("UserIntMS")]
        public async Task<IActionResult> GetIntentUserMasterSettings()
        {
            var result = await _masterRepository.GetUserMasterSettingAsync();
            return Ok(result);
        }
        #endregion User Master Settings

        #region Carton Type
        [HttpGet("GetCartType")]
        public async Task<IActionResult> GetCartonType()
        {
            var result = await _context.MstrCartonType
                        .Select(x => new { x.AutoId, x.Name, x.Description })
                        .ToListAsync();
            return Ok(result);
        }

        [HttpGet("GetCartBoxType/{id}")]
        public async Task<IActionResult> GetCartonBoxType(int id)
        {
            var cartonBoxTypeList = await _masterRepository.GetCartonBoxTypeAsync(id);
            return Ok(cartonBoxTypeList);
        }

        [HttpPost("SaveCarton")]
        public async Task<IActionResult> SaveCarton(MstrCartonType carton)
        {
            var result = await _masterRepository.SaveMasterCartonTypeAsync(carton);
            return Ok(result);
        }
        #endregion Carton Type

        #region Report Date range
        [HttpGet("GetDS/{id}")]
        public async Task<IActionResult> GetDateSelection(int id)
        {
            var result = await _masterRepository.getDateSelectionAsync(id);
            return Ok(result);
        }
        #endregion Report Date range

        #region Customer Other Code
        [HttpGet("CusCode")]
        public async Task<IActionResult> GetCustomerOtherCode()
        {
            var result = await _context.MstrCustomerOtherCode
                    .Select(x => new { x.AutoId, x.CodeHeaderValue })
                    .ToListAsync();
            return Ok(result);
        }
        [HttpGet("CusCodes/{customerId}")]
        public async Task<IActionResult> GetCustomerOtherCodes(int customerId)
        {
            var result = await _context.MstrCustomerHeader
                    .Where(x => x.AutoId == customerId)
                    .Join(_context.MstrCustomerOther, c => c.AutoId, o => o.CustomerId,
                     (c, o) => new { c, o })
                    .Join(_context.MstrCustomerOtherCode, a => a.o.CustOtherId, b => b.AutoId,
                     (a, b) =>
                    new
                    {
                        autoId = b.AutoId,
                        codeHeaderValue = b.CodeHeaderValue
                    })
                    .ToListAsync();
            return Ok(result);
        }
        [HttpPost("SaveCusCode")]
        public async Task<IActionResult> SaveCustomerOtherCode(MstrCustomerOtherCode CustomerOtherCode)
        {
            var result = await _masterRepository.SaveCustomerOtherCodeAsync(CustomerOtherCode);
            return Ok(result);
        }
        #endregion Customer Other Code

        #region Customer Other
        //ALL CUSTOMER OTHER DETAILS 
        [HttpGet("CusOther")]
        public async Task<IActionResult> GetCustomerOther()
        {
            var result = await _context.MstrCustomerOther
                    .Select(x => new { x.AutoId, x.CustomerId, x.CustOtherId, x.Description })
                    .ToListAsync();
            return Ok(result);
        }
        //CUSTOMER OTHER DETAILS BASED ON CUSTOMER OTHER CODE
        [HttpGet("CusOthers/{id}")]
        public async Task<IActionResult> GetCustomerOthers(int id)
        {
            var result = await _context.MstrCustomerOtherCode
                    .Where(x => x.AutoId == id)
                    .Join(_context.MstrCustomerOther, c => c.AutoId, o => o.CustOtherId,
                    (c, o) =>
                     new
                     {
                         autoId = o.AutoId,
                         description = o.Description,
                         customerId = o.CustomerId,
                         name = c.CodeHeaderValue
                     })
                    .ToListAsync();
            return Ok(result);
        }

        [HttpPost("SaveCusOther")]
        public async Task<IActionResult> SaveCustomerOther(MstrCustomerOther CustomerOther)
        {
            var result = await _masterRepository.SaveCustomerOtherAsync(CustomerOther);
            return Ok(result);
        }
        #endregion Customer Other 

        #region "Reasons"

        [HttpGet("Reasons/{moduleid}")]
        public async Task<IActionResult> GetReasons(int moduleid)
        {
            var reasonList = await _context.MstrReason
            .Select(x => new { x.AutoId, x.ModuleId, x.Module, x.Reason, x.IsActive })
            .Where(e => e.ModuleId == moduleid && e.IsActive == true).ToListAsync();

            return Ok(reasonList);
        }
        #endregion "Reasons"

        #region "UserMasterSettings"
        [HttpGet("UserM/{id}")]
        public async Task<IActionResult> GetUserMasterSettings(int id)
        {
            var result = await _context.MstrUserMasterSetting
                    .Select(x => new { x.UserMasterSetId, x.AgentId, x.bIntent, x.CostAtt })
                    .Where(x => x.AgentId == id)
                    .ToListAsync();
            return Ok(result);
        }
        [HttpPost("SaveUserM")]
        public async Task<IActionResult> SaveUerMasterSettings(MstrUserMasterSetting UserMasterSettings)
        {
            var result = await _masterRepository.SaveUerMasterSettingsAsync(UserMasterSettings);
            return Ok(result);
        }
        #endregion "UserMasterSettings"

        [HttpPost("SUserSite")]
        public async Task<ActionResult> saveUserSite(UserSiteDto UserSite)
        {
            var result = await _masterRepository.saveUserSiteAsync(UserSite);
            return Ok(result);
        }
        [HttpGet("UserSite/{id}")]
        public async Task<ActionResult<IEnumerable<UserSiteDto>>> GetUserSiteList(int id)
        {
            var usersiteList = await _masterRepository.GetUserSiteList(id);
            return Ok(usersiteList);
        }
        [HttpPost("SiteUserDelete")]
        public async Task<IActionResult> DeleteUserSiteList(List<UserSiteDto> siteList)
        {
            var result = await _masterRepository.DeleteUserSiteListAsync(siteList);
            return Ok(result);
        }
        #region "ReportList"

        [HttpGet("UserReport/{id}")]
        public async Task<ActionResult<IEnumerable<UserReportList>>> GetUserReportList(int id)
        {
            var reportList = await _masterRepository.GetUserReportList(id);
            return Ok(reportList);
        }



        [HttpPost("ReportUserSave")]
        public async Task<IActionResult> SaveUserReportList(List<ReportUserDto> reportList)
        {
            var result = await _masterRepository.SaveUserReportListAsync(reportList);
            return Ok(result);
        }

        [HttpPost("ReportUserDelete")]
        public async Task<IActionResult> DeleteUserReportList(List<ReportUserDto> reportList)
        {
            var result = await _masterRepository.DeleteUserReportListAsync(reportList);
            return Ok(result);
        }
        #endregion "ReportList"

        #region DashBoardSalesStats
        [HttpPost("GetDashBoardOneData")]
        public async Task<ActionResult> GetDashBoardOneData(dashBoardOneDto dashboardDt)
        {
            var result = await _masterRepository.GetDashBoardOneDataAsync(dashboardDt);
            return Ok(result);
        }
        #endregion DashBoardSalesStats

        #region SalesDashBoardChart
        [HttpPost("DashDt")]
        public async Task<IActionResult> GetDashBoardChartDetails(DashboardExpChartSearchDto dash)
        {
            var result = await _masterRepository.GetDashBoardChartDetailsAsync(dash);
            return Ok(result);
        }
        #endregion SalesDashBoardChart

        #region PurchaseOrder Type

        [HttpGet("POType")]
        public async Task<IActionResult> GetPurchaseOrderType()
        {
            var result = await _context.MstrPurchaseOrderType
                    .Select(x => new { x.POTypeId, x.Details })
                    .ToListAsync();
            return Ok(result);
        }

        [HttpPost("SavePoType")]
        public async Task<ActionResult> SavePurchaseOrderType(MstrPurchaseOrderType purchaseordertype)
        {
            var result = await _masterRepository.SavePurchaseOrderTypeAsync(purchaseordertype);
            return Ok(result);
        }
        #endregion

        #region Ports

        [HttpGet("Ports")]
        public async Task<IActionResult> GetPorts()
        {
            var result = await _context.MstrPorts
                    .Select(x => new { x.PortId, x.PortCode, x.PortName, x.CountryId, x.PortOfLoading, x.PortOfDischarge })
                    .ToListAsync();
            return Ok(result);
        }
        [HttpGet("Port")]
        public async Task<IActionResult> GetPort()
        {
            var result = await _context.MstrPorts
                .Join(_context.MstrCountries, x => x.CountryId, c => c.AutoId,
                (x, c) =>
                new
                {
                    PortId = x.PortId,
                    PortCode = x.PortCode,
                    PortName = x.PortName,
                    CountryId = x.CountryId,
                    Country = c.Name,
                    PortOfLoading = x.PortOfLoading,
                    PortOfDischarge = x.PortOfDischarge
                }).ToListAsync();
            return Ok(result);
        }
        [HttpPost("SaveP")]
        public async Task<ActionResult> SaveUerMasterPorts(MstrPorts Ports)
        {
            var result = await _masterRepository.SaveUerMasterPortsAsync(Ports);
            return Ok(result);
        }
        #endregion

        #region Forwarder

        [HttpGet("Forwarder")]
        public async Task<IActionResult> GetForwarder()
        {
            var result = await _context.MstrForwarder
                    .Select(x => new { x.ForwarderId, x.Contact, x.EmailId, x.Name })
                    .ToListAsync();
            return Ok(result);
        }
        [HttpPost("SaveForw")]
        public async Task<ActionResult> SaveForwarder(MstrForwarder forwarder)
        {
            var result = await _masterRepository.SaveForwarderAsync(forwarder);
            return Ok(result);
        }
        #endregion

        #region Delivery Terms

        [HttpGet("DeliTerms")]
        public async Task<IActionResult> GetDeliveryTerms()
        {
            var result = await _context.MstrDeliveryTerms
                    .Select(x => new { x.DeliTermsId, x.Description, x.Code })
                    .ToListAsync();
            return Ok(result);
        }

        [HttpPost("SaveDelT")]
        public async Task<ActionResult> SaveMasterDeliveryTerm(MstrDeliveryTerms deliveryterms)
        {
            var result = await _masterRepository.SaveMasterDeliveryTermAsync(deliveryterms);
            return Ok(result);
        }
        #endregion Delivery Terms

        #region Shipment Modes

        [HttpGet("ShipModes")]
        public async Task<IActionResult> GetShipmentModes()
        {
            var result = await _context.MstrShipmentModes
                    .Select(x => new { x.ShipModeId, x.Description, x.Code })
                    .ToListAsync();
            return Ok(result);
        }

        #endregion

        #region Supplier Header
        [HttpPost("SaveSup")]
        public async Task<ActionResult> SaveSupplierHd(TransSupplierHeader transSupplierHeader)
        {
            var result = await _masterRepository.SaveSupplierHdAsync(transSupplierHeader);
            return Ok(result);
        }
        #endregion Supplier Header

        #region Supplier Currency
        [HttpGet("SupplierC/{supplierId}")]
        public async Task<IActionResult> GetSupplierC(int supplierId)
        {
            var result = await _context.TransSupplierCurrency
                    .Where(x => x.SupplierId == supplierId)
                    .Join(_context.MstrCurrency, m => m.CurrencyId, c => c.AutoId
                    , (m, c) =>
                    new
                    {
                        supplierCId = m.SuppCurId,
                        currencyId = m.CurrencyId,
                        code = c.Code,
                        name = c.Name,
                        symbol = c.Symbol
                    })
                    .ToListAsync();
            return Ok(result);
        }
        [HttpPost("SaveSupC")]
        public async Task<ActionResult> SaveSupplierCurrency(TransSupplierCurrency suppliercurrency)
        {
            var result = await _masterRepository.SaveSupplierCurrencyAsync(suppliercurrency);
            return Ok(result);
        }
        [HttpPost("DeleteSupC")]
        public async Task<IActionResult> DeleteSupCurrency(TransSupplierCurrency supplierCurrency)
        {
            var result = await _masterRepository.DeleteSupCurrencyAsync(supplierCurrency);
            return Ok(result);
        }
        #endregion Supplier Currency
        #region Supplier Type
        [HttpPost("SaveSupT")]
        public async Task<ActionResult> SaveSupplierType(MstrSupplierType suppliertype)
        {
            var result = await _masterRepository.SaveSupplierTypeAsync(suppliertype);
            return Ok(result);
        }
        #endregion Supplier Type
        #region Additional Charges

        [HttpGet("AddCharg/{moduleId}")]
        public async Task<ActionResult> GetAdditionalCharges(int moduleId)
        {
            var result = await _context.MstrAddChargeModule
                        .Where(x => x.ModuleId == moduleId && x.bActive == true)
                         .Join(_context.MstrAddionalCharges, m => m.AddChargeId, c => c.AddChargeId
                            , (m, c) =>
                            new
                            {
                                addChargeId = m.AddChargeId,
                                description = c.Description
                            }).ToListAsync();
            return Ok(result);
        }

        [HttpGet("AddChargs")]
        public async Task<IActionResult> GetMasterAddCharges()
        {
            var result = await _context.MstrAddionalCharges
                        .Select(x => new { x.Description, x.AddChargeId })
                        .ToListAsync();
            return Ok(result);
        }


        [HttpPost("SaveAddC")]
        public async Task<ActionResult> SaveMasterAdditonalCharges(MstrAdditionalCharges addcharge)
        {
            var result = await _masterRepository.SaveMasterAdditonalChargesAsync(addcharge);
            return Ok(result);
        }
        #endregion Additional Charges

        #region Additional Charges Module

        [HttpGet("AddChargM")]
        public async Task<ActionResult> GetAdditionalChargesModule()
        {
            var result = await _context.MstrAddChargeModule
                        .Select(x => new
                        {
                            x.ModuleId,
                            x.AutoId,
                            x.Module,
                            x.AddChargeId,
                            x.AddChargeType
                       ,
                            x.bActive
                        })
                        .ToListAsync();
            return Ok(result);
        }

        [HttpPost("SaveAddCM")]
        public async Task<ActionResult> SaveMasterAddChargeBasisToModuleMap(MstrAddChargeModule addCM)
        {
            var result = await _masterRepository.SaveMasterAddChargeBasisToModuleMapAsync(addCM);
            return Ok(result);
        }

        #endregion Additional Charges Module 

        #region Account Type
        [HttpGet("AccountT")]
        public async Task<IActionResult> GetAccountType()
        {
            var result = await _context.MstrAccountType
            .Select(x => new { x.Description, x.AccTypeId })
            .ToListAsync();
            return Ok(result);
        }
        [HttpPost("SaveAccT")]
        public async Task<ActionResult> SaveAccountType(MstrAccountType accounttype)
        {
            var result = await _masterRepository.SaveAccountTypeAsync(accounttype);
            return Ok(result);
        }
        #endregion Account Type

        #region GRN Type

        [HttpGet("GRNType")]
        public async Task<ActionResult> GetGRNType()
        {
            var result = await _context.MstrGRNType
                        .Select(x => new { x.Description, x.GRNTypeId })
                        .ToListAsync();
            return Ok(result);
        }

        [HttpPost("SaveGRNT")]
        public async Task<ActionResult> SaveGRNType(MstrGRNType grntype)
        {
            var result = await _masterRepository.SaveGRNTypeAsync(grntype);
            return Ok(result);
        }
        #endregion GRN Type

        #region Shipment Mode
        [HttpPost("SaveShipmentM")]
        public async Task<ActionResult> SaveShipmentMode(MstrShipmentModes shipmentModes)
        {
            var result = await _masterRepository.SaveShipmentModeAsync(shipmentModes);
            return Ok(result);
        }

        #endregion Shipment Mode

        #region "Products"   

        [HttpGet("Products")]
        public async Task<ActionResult> GetProductsAll()
        {
            var ProductsList = await _context.MstrProducts
                .Select(x => new { x.productId, x.Description, x.bActive })
                .ToListAsync();
            return Ok(ProductsList);
        }

        [HttpPost("Deactive/Product")]
        public async Task<ActionResult> DeactProduct(MstrProducts MstrProducts)
        {
            var result = await _masterRepository.DeactProductAsync(MstrProducts);
            return Ok(result);
        }

        [HttpPost("SaveProuct")]
        public async Task<ActionResult> SaveProduct(MstrProducts MstrProducts)
        {
            var result = await _masterRepository.SaveProductAsync(MstrProducts);
            return Ok(result);
        }


        #endregion ""Products"
        #region ENUM TYPES

        [HttpGet("Enum/{enumType}")]
        public async Task<ActionResult> GetEnumValues(string enumType)
        {
            var result = await _context.MstrEnumValues
                .Where(x => x.EnumType == enumType && x.IsActive == 1)
                .Select(x => new
                {
                    x.EnumType,
                    x.EnumValueId,
                    x.ValueId,
                    x.ValueCode,
                    x.DisplayName,
                    x.IsActive
                }).OrderBy(x => x.ValueId).ToListAsync();
            return Ok(result);
        }


        #endregion ENUM TYPES

        #region Basis

        [HttpGet("Basis")]
        public async Task<ActionResult> GetBasis()
        {
            var result = await _context.MstrBasis
                .Select(x => new
                {
                    x.BaseId,
                    x.Description
                }).ToListAsync();
            return Ok(result);
        }
        [HttpPost("SaveBasis")]
        public async Task<ActionResult> SaveMasterBasis(MstrBasis basis)
        {
            var result = await _masterRepository.SaveMasterBasisAsync(basis);
            return Ok(result);
        }
        #endregion Basis

        #region DispatchReason
        [HttpGet("DispatchR")]
        public async Task<IActionResult> GetDispatchReason()
        {
            var result = await _context.MstrDispatchReason
                    .Select(x => new { x.DisReasonId, x.Description })
                    .ToListAsync();
            return Ok(result);
        }
        [HttpPost("SaveDispatchR")]
        public async Task<ActionResult> SaveDispatchReason(MstrDispatchReason dispatchreason)
        {
            var result = await _masterRepository.SaveDispatchReasonAsync(dispatchreason);
            return Ok(result);
        }
        #endregion DispatchReason

        #region SupplierHeader
        [HttpPost("SupplierDeactive")]
        public async Task<ActionResult> DeactiveSupplier(MstrSupplierHeader mstrSupplierHeader)
        {
            var result = await _masterRepository.DeactiveSupplierAsync(mstrSupplierHeader);
            return Ok(result);
        }
        #endregion SupplierHeader

        #region SupplierAddressList

        [HttpGet("SupplierA/{supplierId}")]
        public async Task<IActionResult> GetSupplierAddressList(int supplierId)
        {
            var result = await _context.TransSupplierAddresses
            .Where(x => x.SupplierId == supplierId)
            .Select(x => new
            {
                x.SuppAddId,
                x.AddressTypeId,
                x.Address,
                x.City,
                x.State,
                x.ZipPostalCode,
                x.CountryId,
                x.Tel,
                x.VATNo,
                x.TaxNo,
                x.TinNo,
                x.bActive
            }).ToListAsync();
            return Ok(result);
        }
        [HttpPost("SupAddSave")]
        public async Task<ActionResult> SaveSupplierAddressList(TransSupplierAddresses supplieraddresses)
        {
            var result = await _masterRepository.SaveSupplierAddressListAsync(supplieraddresses);
            return Ok(result);
        }
        #endregion SupplierAddressList

        #region Package Mapping


        [HttpPost("SavePackMap")]
        public async Task<ActionResult> SavePackageMapping(MstrMapping packmap)
        {
            var result = await _masterRepository.SavePackageMappingAsync(packmap);
            return Ok(result);
        }

        [HttpPost("GetMappedDt")]
        public async Task<IActionResult> GetMappedDt(PackmapDto mapDt)
        {
            var result = await _masterRepository.GetMappedDtAsync(mapDt);
            return Ok(result);
        }
        #endregion Package Mapping

        #region SpecialCategory
        [HttpGet("SpeCat")]
        public async Task<IActionResult> GetSpecialCategory()
        {
            var result = await _context.MstrSpecialCategory
                    .Select(x => new { x.AutoId, x.Code, x.Description, x.IsActive })
                    .ToListAsync();
            return Ok(result);
        }

        [HttpPost("SpeCatSve")]
        public async Task<IActionResult> SaveMasterSpecialCategory(MstrSpecialCategory category)
        {
            var result = await _masterRepository.SaveMasterSpecialCategoryAsync(category);
            return Ok(result);
        }

        [HttpPost("DactCat")]
        public async Task<IActionResult> DeactiveSpecialCategory(MstrSpecialCategory category)
        {
            var result = await _masterRepository.DeactiveSpecialCategoryAsync(category);
            return Ok(result);
        }

        #endregion SpecialCategory

        #region Sub Category
        [HttpGet("SubCat")]
        public async Task<IActionResult> GetSubCategory()
        {
            var result = await _context.MstrSubCategory
                    .Select(x => new { x.AutoId, x.Code, x.Description, x.IsActive })
                    .ToListAsync();
            return Ok(result);
        }

        [HttpPost("SubCatSve")]
        public async Task<IActionResult> SaveMasterSubCategory(MstrSubCategory category)
        {
            var result = await _masterRepository.SaveMasterSubCategoryAsync(category);
            return Ok(result);
        }

        [HttpPost("DactSubCat")]
        public async Task<IActionResult> DeactiveSubCategory(MstrSubCategory category)
        {
            var result = await _masterRepository.DeactiveSubCategoryAsync(category);
            return Ok(result);
        }

        [HttpGet("GroupSubCat/{groupId}")]
        public async Task<ActionResult> GetGroupSubCatDt(int groupId)
        {
            var result = await _masterRepository.GetGroupSubCatDtAsync(groupId);
            return Ok(result);
        }

        #endregion Sub Category
        #region ProdGroup Sub Category
        [HttpPost("AssignGroupSubCat")]
        public async Task<ActionResult> AssignProdGroupSubCat(List<MstrProdGroupSubCat> prod)
        {
            var result = await _masterRepository.AssignProdGroupSubCatAsync(prod);
            return Ok(result);
        }

        [HttpPost("DeleteGroupSubCat")]
        public async Task<ActionResult> DeleteProdGroupSubCat(List<MstrProdGroupSubCat> prod)
        {
            var result = await _masterRepository.DeleteProdGroupSubCatAsync(prod);
            return Ok(result);
        }
        #endregion ProdGroup Sub Category

        #region Prod SubCat Category 
        [HttpGet("SubCatCat/{subcatId}")]
        public async Task<ActionResult> GetSubCatCategoryDt(int subcatId)
        {
            var result = await _masterRepository.GetSubCatCategoryDtAsync(subcatId);
            return Ok(result);
        }

        [HttpPost("AssignSubCat")]
        public async Task<ActionResult> AssignSubCatCategory(List<MstrProdSubCatCategory> prod)
        {
            var result = await _masterRepository.AssignSubCatCategoryAsync(prod);
            return Ok(result);
        }

        [HttpPost("DeleteSubCat")]
        public async Task<ActionResult> DeleteSubCatCategory(List<MstrProdSubCatCategory> prod)
        {
            var result = await _masterRepository.DeleteSubCatCategoryAsync(prod);
            return Ok(result);
        }
        #endregion Prod SubCat Category 

        #region Stock Adjuestment Reason

        [HttpGet("SAReason")]
        public async Task<IActionResult> GetStockAdjuestmentReason()
        {
            var result = await _context.MstrStockAdjuestmentReason
            .Select(x => new { x.Reason, x.Sign, x.ReasonId })
            .ToListAsync();
            return Ok(result);
        }

        #endregion Stock Adjuestment Reason


        #region Fixed Assets

        [HttpPost("SaveFAmaincategory")]
        public async Task<ActionResult> SaveFAMainCategory(MstrMainCategory famaincategory)
        {
            var result = await _masterRepository.SaveFAMainCategoryAsync(famaincategory);
            return Ok(result);
        }


        [HttpGet("MainCategory")]
        public async Task<IActionResult> GetMainCategory()
        {
            var result = await _context.MstrMainCategory
                .Select(x => new { x.AutoId, x.Code, x.Name }).ToListAsync();
            return Ok(result);
        }

        [HttpPost("SaveFASubcategory")]
        public async Task<ActionResult> SaveFASubCategory(MstrFASubCategory fasubcategory)
        {
            var result = await _masterRepository.SaveFASubCategoryAsync(fasubcategory);
            return Ok(result);
        }

        [HttpGet("SubCategory")]
        public async Task<IActionResult> GetFASubCategory()
        {
            var result = await _context.MstrFASubCategory
                .Select(x => new { x.AutoId, x.Code, x.Name, x.SerialNo }).ToListAsync();
            return Ok(result);
        }

        [HttpGet("FASubAll/{id}")]
        public async Task<IActionResult> GetFASubAllocDetails(int id)
        {
            var result = await _masterRepository.GetFASubAllocDetailsAsync(id);
            return Ok(result);
        }


        [HttpPost("saveFASubtoMain")]
        public async Task<IActionResult> saveSubToMainCategoryAllocation(List<MstrFASubToMainCategoryAll> SubtoMainAlloc)
        {
            var result = await _masterRepository.saveSubToMainCategoryAllocationAsync(SubtoMainAlloc);
            return Ok(result);
        }

        [HttpPost("deleteFASubtoMain")]
        public async Task<IActionResult> deleteSubToMainCategoryAllocation(List<MstrFASubToMainCategoryAll> SubtoMainAlloc)
        {
            var result = await _masterRepository.deleteSubToMainCategoryAllocationAsync(SubtoMainAlloc);
            return Ok(result);
        }

        #endregion Fixed Assets 


        #region "Model"
        [HttpPost("SaveModel")]
        public async Task<ActionResult> saveModel(MstrModel MstrModel)
        {
            var result = await _masterRepository.saveModelAsync(MstrModel);
            return Ok(result);
        }

        [HttpGet("Model")]
        public async Task<IActionResult> GetModel()
        {
            var result = await _context.MstrModel
            .Select(x => new { x.AutoId, x.ModelName }).ToListAsync();
            return Ok(result);
        }

        [HttpGet("ModelAll/{id}")]
        public async Task<IActionResult> GetModelAllocDetails(int id)
        {
            var result = await _masterRepository.GetModelAllocDetailsAsync(id);
            return Ok(result);
        }

        [HttpPost("saveModelAll")]
        public async Task<IActionResult> saveModelAllocation(List<MstrFASubToMainCategoryAll> modelAlloc)
        {
            var result = await _masterRepository.saveModelAllocationAsync(modelAlloc);
            return Ok(result);
        }

        [HttpPost("delModelToBrand")]
        public async Task<IActionResult> deleteModelToBrandAllocation(List<MstrFASubToMainCategoryAll> modelToBrandall)
        {
            var result = await _masterRepository.deleteModelToBrandAllocationAsync(modelToBrandall);
            return Ok(result);
        }

        #endregion "Model"

        #region Season
        [HttpGet("GetSeason")]
        public async Task<IActionResult> GetSeason()
        {
            var seasonList = await _context.MstrSeason
            .Select(x => new { x.Description, x.AutoId })
            .ToListAsync();
            return Ok(seasonList);
        }
        #endregion "Season"
        #region Gender
        [HttpGet("GetGender")]
        public async Task<IActionResult> GetGender()
        {
            var genderList = await _context.MstrGender
            .Select(x => new { x.Description, x.AutoId })
            .ToListAsync();
            return Ok(genderList);
        }
        #endregion "Gender"
        #region "fabricCom"
        [HttpGet("GetfabricCom")]
        public async Task<IActionResult> GetfabricCom()
        {
            var fabriccomList = await _context.MstrFabricCom
            .Select(x => new { x.Description, x.AutoId })
            .ToListAsync();
            return Ok(fabriccomList);
        }
        #endregion "fabricCom"        
        #region "fabricCategory"
        [HttpGet("GetfabricCategory")]
        public async Task<IActionResult> GetfabricCategory()
        {
            var fabriccategoryList = await _context.MstrFabricCategory
            .Select(x => new { x.Description, x.AutoId })
            .ToListAsync();
            return Ok(fabriccategoryList);
        }
        #endregion "fabricCom"        
        #region "Washtype"
        [HttpGet("GetwashType")]
        public async Task<IActionResult> GetwashType()
        {
            var washtypeList = await _context.MstrWashType
            .Select(x => new { x.Description, x.AutoId })
            .ToListAsync();
            return Ok(washtypeList);
        }
        #endregion "Washtype"             

        #region "Save Apperale Article"
        [HttpPost("saveApperaleArticle")]
        public async Task<IActionResult> saveApperaleArticle(List<SaveArticleApperaleDetailDto> aaDt)
        {
            var result = await _masterRepository.saveApperaleArticle(aaDt);
            return Ok(result);
        }
        #endregion "Save Apperale Article"

        #region MWS Master
        [HttpPost("GetMWSMasterData")]
        public async Task<IActionResult> GetMWSMasterData(MWSMasterDto wsDt)
        {
            var result = await _masterRepository.GetMWSMasterData(wsDt);
            return Ok(result);
        }

        [HttpPost("SaveMWSMasterData")]
        public async Task<IActionResult> SaveMWSMasterData(List<SaveMWSMasterDto> wsDt)
        {

            var result = await _masterRepository.SaveMWSMasterData(wsDt);
            return Ok(result);
        }
        #endregion "MWS Master"

        #region Get MachineBreaks
        [HttpGet("GetMachinBK/{location}")]
        public async Task<IActionResult> GetFactoryWiseMachineBreak(string location)
        {
            var result = await _mTrackMasterRepository.GetFactoryWiseMachineBreakAsync(location);
            return Ok(result);
        }
        #endregion Get MachineBreaks

        #region Update MachineBreaks
        [HttpPost("GetMachinUp")]
        public async Task<IActionResult> UpdateMachineBreak(MachineBreakUpdateDto machine)
        {
            var result = await _mTrackMasterRepository.UpdateMachineBreakAsync(machine);
            return Ok(result);
        }
        #endregion Update MachineBreaks

        #region Get Sample Data
        [HttpPost("SampleDt")]
            public async Task<IActionResult> GetSampleDetails(SampleDto sample)
            {
                var result = await _mTrackMasterRepository.GetSampleDetailsAsync(sample);
                return Ok(result);
        }
        #endregion Get Sample Data

        

    }
}