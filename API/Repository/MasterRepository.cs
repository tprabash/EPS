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
    public class MasterRepository : DbConnCartonRepositoryBase , IMasterRepository
    {
         private readonly IApplicationCartonDbContext _context;

        public MasterRepository(IDbConnectionFactory dbConnectionFactory, IApplicationCartonDbContext context) : base(dbConnectionFactory)
        {
            _context = context;
        }

        // #region Menu List
        // public async Task<IEnumerable<MenuJoinList>> GetMenuListAsync()
        // {
        //     IEnumerable<MenuJoinList> menus = await DbConnection.QueryAsync<MenuJoinList>("spMenuListGet", null
        //              , commandType: CommandType.StoredProcedure);

        //     return menus;
        // }

        
        // public async Task<int> SaveMenuListAsync(MenuListDto menuListDto)
        // {
        //     DynamicParameters para = new DynamicParameters();

        //     para.Add("@mId", menuListDto.AutoIdx);
        //     para.Add("@mName",menuListDto.MenuName.Trim());
        //     para.Add("@mDescription",menuListDto.MenuDescription.Trim());
        //     para.Add("@mType", menuListDto.mType.Trim());
        //     para.Add("@GroupName",menuListDto.GroupName.Trim());
        //     para.Add("@AgentLevel",menuListDto.AgentLevelId);
        //     para.Add("@AgentId",menuListDto.AgentId);
        //     para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

        //     await DbConnection.ExecuteAsync("spMenuListSave", para , commandType: CommandType.StoredProcedure);

        //     return para.Get<int>("Result");
        // }

        // #endregion


        //#region User Menu List            
        
        // public async Task<IEnumerable<PermitMenuDto>> GetAuthMenuListAsync(UserDto userDto)
        // {
        //     IEnumerable<PermitMenuDto> menuList = Enumerable.Empty<PermitMenuDto>();
        //     DynamicParameters para = new DynamicParameters();

        //     para.Add("AgentId" , userDto.UserId);

        //     if (userDto.ModuleId == 1) {
        //        return menuList = await DbConnection.QueryAsync<PermitMenuDto>("spMenuListAuthorize" , para
        //             , commandType: CommandType.StoredProcedure);
        //     }
            
        //     return menuList;
        // }        

        // public async Task<IEnumerable<UserMenuList>> GetUserMenuList(int userId)
        // {            
        //     DynamicParameters para = new DynamicParameters();

        //     para.Add("AgentId", userId);

        //     IEnumerable<UserMenuList> userMenuList = await DbConnection.QueryAsync<UserMenuList>("spMenuUserGetList", para
        //             , commandType: CommandType.StoredProcedure);
        //     return userMenuList;
        // }

        // public async Task<int> SaveUserMenuListAsync(List<MenuUserDto> menuList)
        // {
        //     DataTable MenuUserDT = new DataTable();
        //     DynamicParameters para = new DynamicParameters();

        //     MenuUserDT.Columns.Add("AgentId", typeof(int));
        //     MenuUserDT.Columns.Add("MenuId", typeof(int));
        //     MenuUserDT.Columns.Add("CreUserID", typeof(int));

        //     foreach (var item in menuList)
        //     {
        //         MenuUserDT.Rows.Add(item.AgentId
        //                 , item.MenuId
        //                 , item.CreUserID);
        //     }

        //     para.Add("MenuUserDT", MenuUserDT.AsTableValuedParameter("MenuUserType"));
        //     para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

        //     await DbConnection.ExecuteAsync("spMenuUserSave", para, commandType: CommandType.StoredProcedure);

        //     return para.Get<int>("Result");

        // }

        
        // public async Task<int> DeleteUserMenuListAsync(List<MenuUserDto> menuList)
        // {
        //     DataTable MenuUserDT = new DataTable();
        //     DynamicParameters para = new DynamicParameters();

        //     MenuUserDT.Columns.Add("AgentId",typeof(int));
        //     MenuUserDT.Columns.Add("MenuId",typeof(int));
        //     MenuUserDT.Columns.Add("CreUserID",typeof(int));

        //     foreach (var item in menuList)
        //     {
        //         MenuUserDT.Rows.Add( item.AgentId
        //                 , item.MenuId
        //                 , item.CreUserID);
        //     }

        //     para.Add("MenuUserDT", MenuUserDT.AsTableValuedParameter("MenuUserType"));
        //     para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

        //     await DbConnection.ExecuteAsync("spMenuUserDelete", para, commandType: CommandType.StoredProcedure);

        //     return para.Get<int>("Result");      

        // }

        //#endregion User Menu List


        #region Location            
       
        public async Task<MstrUserLocation> GetDefaultLocForUser(int userId)
        {
            return await _context.MstrUserLocation.Where(u => u.UserId == userId)
                .FirstOrDefaultAsync(p => p.IsDefault);
        }       
      
        public async Task<int> SetDefaultLocationAsync(MstrUserLocation userLoc)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("UserId", userLoc.UserId);
            para.Add("UserLocId", userLoc.AutoId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrUserLocSetDefault", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Location

        #region Article UOM Conversion

        public async Task<int> SaveArticleUOMConvAsync(MstrArticleUOMConversion articleUOM)
        {            
            DynamicParameters para = new DynamicParameters();
           
            para.Add("@AutoId", articleUOM.AutoId);
            para.Add("@ArticleId", articleUOM.ArticleId);
            para.Add("@UnitId", articleUOM.UnitId);
            para.Add("@Value", articleUOM.Value);
            para.Add("@UserId", articleUOM.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);             

            await DbConnection.ExecuteAsync("spMstrArticleUOMConversionSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> ActiveArticleUOMConvAsync(MstrArticleUOMConversion articleUOM)
        {            
            DynamicParameters para = new DynamicParameters();
           
            para.Add("@AutoId", articleUOM.AutoId);           
            para.Add("@UserId", articleUOM.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);             

            await DbConnection.ExecuteAsync("spMstrArticleUOMConversionActive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Article UOM Conversion


        #region Article Color Allocation

        public async Task<IEnumerable<ColorAllocationDto>> getArtColorPermitDtAsync(int ArticleId)
        {
            //IEnumerable<ColorAllocationDto> colorList;
            DynamicParameters para = new DynamicParameters();

            para.Add("ArticleId" , ArticleId);
           
            return await DbConnection.QueryAsync<ColorAllocationDto>("spMstrArticleColorGetPColor" , para
                    , commandType: CommandType.StoredProcedure);            
        }

        public async Task<int> SaveArticleColorAsync(List<MstrArticleColor> articleColor)
        {
            DataTable artColorDt = new DataTable();
            DynamicParameters para = new DynamicParameters();

            artColorDt.Columns.Add("ArticleId" , typeof(long));
            artColorDt.Columns.Add("ColorId" , typeof(int));
            artColorDt.Columns.Add("UserId" , typeof(int));

            foreach (var item in articleColor)
            {
                artColorDt.Rows.Add( item.ArticleId
                       , item.ColorId
                       , item.CreateUserId);                
            }   

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 
            para.Add("ArtColorTypeDT", artColorDt.AsTableValuedParameter("ArtColorType"));

            await DbConnection.ExecuteAsync("spMstrArticleColorSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

           public async Task<int> DeleteArticleColorAsync(List<MstrArticleColor> articleColor)
        {
            DataTable artColorDt = new DataTable();
            DynamicParameters para = new DynamicParameters();

            artColorDt.Columns.Add("ArticleId" , typeof(long));
            artColorDt.Columns.Add("ColorId" , typeof(int));
            artColorDt.Columns.Add("UserId" , typeof(int));

            foreach (var item in articleColor)
            {
                artColorDt.Rows.Add( item.ArticleId
                       , item.ColorId
                       , item.CreateUserId);                
            }   

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 
            para.Add("ArtColorTypeDT", artColorDt.AsTableValuedParameter("ArtColorType"));

            await DbConnection.ExecuteAsync("spMstrArticleColorDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Article Color
        
        #region Article Size Allocation

        public async Task<IEnumerable<SizeAllocationDto>> getArtSizePermitDtAsync(int ArticleId)
        {
            // IEnumerable<SizeAllocationDto> sizeList;
            DynamicParameters para = new DynamicParameters();

            para.Add("ArticleId" , ArticleId);
           
            return await DbConnection.QueryAsync<SizeAllocationDto>("spMstrArticleSizeGetPSize" , para
                    , commandType: CommandType.StoredProcedure);            
        }

        public async Task<int> SaveArticleSizeAsync(List<MstrArticleSize> articleSize)
        {
            DataTable artColorDt = new DataTable();
            DynamicParameters para = new DynamicParameters();

            artColorDt.Columns.Add("ArticleId" , typeof(long));
            artColorDt.Columns.Add("SizeId" , typeof(int));
            artColorDt.Columns.Add("UserId" , typeof(int));

            foreach (var item in articleSize)
            {
                artColorDt.Rows.Add( item.ArticleId
                       , item.SizeId
                       , item.CreateUserId);                
            }   

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 
            para.Add("ArtSizeTypeDT", artColorDt.AsTableValuedParameter("ArtSizeType"));

            await DbConnection.ExecuteAsync("spMstrArticleSizeSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeleteArticleSizeAsync(List<MstrArticleSize> articleSize)
        {
            DataTable artColorDt = new DataTable();
            DynamicParameters para = new DynamicParameters();

            artColorDt.Columns.Add("ArticleId" , typeof(long));
            artColorDt.Columns.Add("SizeId" , typeof(int));
            artColorDt.Columns.Add("UserId" , typeof(int));

            foreach (var item in articleSize)
            {
                artColorDt.Rows.Add( item.ArticleId
                       , item.SizeId
                       , item.CreateUserId);                
            }   

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 
            para.Add("ArtSizeTypeDT", artColorDt.AsTableValuedParameter("ArtSizeType"));

            await DbConnection.ExecuteAsync("spMstrArticleSizeDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Article Size

        #region Color            
        
        public async Task<int> SaveColorCardAsync(MstrColorCard mstrccard)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , mstrccard.AutoId);
            para.Add("Name", mstrccard.Name.Trim());
            para.Add("UserId", mstrccard.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrColorCardSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> SaveColorAsync(MstrColor mstrColor)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , mstrColor.AutoId);
            para.Add("Code", mstrColor.Code.ToUpper().Trim());
            para.Add("Name", mstrColor.Name.ToUpper().Trim());
            para.Add("UserId", mstrColor.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrColorSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeactiveColorCardAsync(MstrColorCard mstrccard)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("ColorCardId" , mstrccard.AutoId);
            para.Add("IsActive", mstrccard.IsActive);
            para.Add("UserId", mstrccard.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrColorCardDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion

        #region Color Allocation

        public async Task<IEnumerable<ColorAllocationDto>> GetColorAllocDetailsAsync(int ColorCardId)
        {
            //IEnumerable<ColorAllocationDto> colorList;
            DynamicParameters para = new DynamicParameters();

            para.Add("ColorCardId" , ColorCardId);
           
            return await DbConnection.QueryAsync<ColorAllocationDto>("spMstrColorGetAllocDetails" , para
                    , commandType: CommandType.StoredProcedure);            
        }

        
        public async Task<int> SaveColorAllocationAsync(List<MstrColorAllocCard> colorAlloc)
        {
            DataTable ColorAllocDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            ColorAllocDT.Columns.Add("ColorCardId",typeof(byte));
            ColorAllocDT.Columns.Add("ColorId",typeof(int));
            ColorAllocDT.Columns.Add("UserID",typeof(int));

            foreach (var item in colorAlloc)
            {
                ColorAllocDT.Rows.Add( item.ColorCardId
                        , item.ColorId
                        , item.CreateUserId);
            }         

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            para.Add("ColorAllocDT", ColorAllocDT.AsTableValuedParameter("ColorAllocType"));

            await DbConnection.ExecuteAsync("spMstrColorAllocationSave", para , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        
        public async Task<int> DeleteColorAllocationAsync(List<MstrColorAllocCard> colorAlloc)
        {
            DataTable ColorAllocDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            ColorAllocDT.Columns.Add("ColorCardId",typeof(byte));
            ColorAllocDT.Columns.Add("ColorId",typeof(int));
            ColorAllocDT.Columns.Add("UserID",typeof(int));

            foreach (var item in colorAlloc)
            {
                ColorAllocDT.Rows.Add( item.ColorCardId
                        , item.ColorId
                        , item.CreateUserId);
            }         

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            para.Add("ColorAllocDT", ColorAllocDT.AsTableValuedParameter("ColorAllocType"));

            await DbConnection.ExecuteAsync("spMstrColorAllocationDelete", para, commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");

        }

        #endregion Color Allocation


        #region Size to Size Card Allocation

        public async Task<IEnumerable<SizeAllocationDto>> GetSizeAllocDetailsAsync(int SizeCardId)
        {
            //IEnumerable<SizeAllocationDto> sizeList;
            DynamicParameters para = new DynamicParameters();

            para.Add("SizeCardId" , SizeCardId);
           
            return await DbConnection.QueryAsync<SizeAllocationDto>("spMstrSizeGetAllocDetails" , para
                    , commandType: CommandType.StoredProcedure);            
        }

        
        public async Task<int> SaveSizeAllocationAsync(List<MstrSizeAllocCard> sizeAlloc)
        {
            DataTable SizeAllocDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            SizeAllocDT.Columns.Add("SizeCardId",typeof(byte));
            SizeAllocDT.Columns.Add("SizeId",typeof(int));
            SizeAllocDT.Columns.Add("UserID",typeof(int));

            foreach (var item in sizeAlloc)
            {
                SizeAllocDT.Rows.Add( item.SizeCardId
                        , item.SizeId
                        , item.CreateUserId);
            } 

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            para.Add("SizeAllocDT", SizeAllocDT.AsTableValuedParameter("SizeAllocType"));

            await DbConnection.ExecuteAsync("spMstrSizeAllocationSave", para, commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        
        public async Task<int> DeleteSizeAllocationAsync(List<MstrSizeAllocCard> sizeAlloc)
        {
            DataTable SizeAllocDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            SizeAllocDT.Columns.Add("SizeCardId",typeof(byte));
            SizeAllocDT.Columns.Add("SizeId",typeof(int));
            SizeAllocDT.Columns.Add("UserID",typeof(int));

            foreach (var item in sizeAlloc)
            {
                SizeAllocDT.Rows.Add( item.SizeCardId
                        , item.SizeId
                        , item.CreateUserId);
            }         

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            para.Add("SizeAllocDT", SizeAllocDT.AsTableValuedParameter("SizeAllocType"));

            await DbConnection.ExecuteAsync("spMstrSizeAllocationDelete", para, commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");

        }

        #endregion Size to Size Card Allocation


        #region Size
                    
        public async Task<int> SaveSizeCardAsync(MstrSizeCard mstrscard)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , mstrscard.AutoId);
            para.Add("Name", mstrscard.Name.Trim());
            para.Add("UserId", mstrscard.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrSizeCardSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> SaveSizeAsync(MstrSize mstrSize)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , mstrSize.AutoId);
            para.Add("Code", mstrSize.Code.ToUpper().Trim());
            para.Add("Name", mstrSize.Name.ToUpper().Trim());
            para.Add("UserId", mstrSize.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrSizeSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeactiveSizeCardAsync(MstrSizeCard mstrscard)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("SizeCardId" , mstrscard.AutoId);
            para.Add("IsActive" , mstrscard.IsActive);
            para.Add("UserId", mstrscard.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrSizeCardDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion
       

        #region Article
            
        public async Task<IEnumerable<MstrColor>> GetArticlColorAsync(int articleId)
        {   
            IEnumerable<MstrColor> coloList;
            DynamicParameters para = new DynamicParameters();

            para.Add("ArticleId" , articleId);
            //para.Add("LocationId", articleDto.LocationId);

            coloList = await DbConnection.QueryAsync<MstrColor>("spMstrArticleColorGet" , para
                    , commandType: CommandType.StoredProcedure);
            
            return coloList;
        } 

        public async Task<IEnumerable<MstrSize>> GetArticlSizeAsync(int articleId)
        {   
            IEnumerable<MstrSize> sizeList;
            DynamicParameters para = new DynamicParameters();

            para.Add("ArticleId" , articleId);
            //para.Add("LocationId", articleDto.LocationId);

            sizeList = await DbConnection.QueryAsync<MstrSize>("spMstrArticleSizeGet" , para
                    , commandType: CommandType.StoredProcedure);
            
            return sizeList;
        } 

        public async Task<ArticleReturnDto> SaveArticleAsync(SaveArticleDto article)
        {
            DataTable flexField = new DataTable();
            DynamicParameters para = new DynamicParameters();

            flexField.Columns.Add("FieldId", typeof(int));
            flexField.Columns.Add("FieldCode", typeof(string));
            flexField.Columns.Add("FieldName", typeof(string));
            flexField.Columns.Add("DataType", typeof(string));
            flexField.Columns.Add("ValueList", typeof(bool));
            flexField.Columns.Add("bFlexFieldValue", typeof(bool));
            flexField.Columns.Add("dFlexFieldValue", typeof(DateTime));
            flexField.Columns.Add("iFlexFeildValue", typeof(int));
            flexField.Columns.Add("fFlexFeildValue", typeof(decimal));
            flexField.Columns.Add("cFlexFeildValue", typeof(string));

            para.Add("AutoId" , article.Article.AutoId);
            para.Add("StockCode", article.Article.StockCode.Trim());
            para.Add("ArticleName", article.Article.ArticleName.Trim());
            para.Add("Description1", article.Article.Description1.Trim());
            para.Add("Description2", article.Article.Description2.Trim());
            para.Add("ImageName", article.Article.ImageName.Trim());
            para.Add("CategoryId", article.Article.CategoryId);
            para.Add("ProTypeId", article.Article.ProTypeId);
            para.Add("ProGroupId", article.Article.ProGroupId);
            para.Add("SubCatId", article.Article.SubCatId);
            para.Add("CatId", article.Article.CatId);
            para.Add("ItemType", article.Article.ItemType);
            para.Add("UnitId", article.Article.StorageUnitId);
            para.Add("MeasurementId", article.Article.MeasurementId);
            para.Add("ColorCardId", article.Article.ColorCardId);
            para.Add("SizeCardId", article.Article.SizeCardId);
            para.Add("SalesPrice", article.Article.SalesPrice);
            para.Add("AvgCostPrice", article.Article.AvgCostPrice);
            para.Add("LastCostPrice", article.Article.LastCostPrice);
            para.Add("MaxCostPrice", article.Article.MaxCostPrice);
            para.Add("Dilution", article.Article.Dilution);
            para.Add("UserId", article.Article.CreateUserId);

            foreach (var item in article.FlexField)
            {
                flexField.Rows.Add(item.FlexFieldId,
                                    item.FlexFieldCode ,
                                    item.FlexFieldName ,
                                    item.DataType ,
                                    item.ValueList,
                                    item.bFlexFieldValue,
                                    item.dFlexFieldValue,
                                    item.iFlexFeildValue,
                                    item.fFlexFeildValue,
                                    item.cFlexFeildValue);
            } 

            para.Add("FlexFieldDT", flexField.AsTableValuedParameter("FlexFieldType"));       

            var result = await DbConnection.QueryFirstOrDefaultAsync<ArticleReturnDto>("spMstrArticleSave", para
                , commandType: CommandType.StoredProcedure);            

            return result;
        }

        public async Task<IEnumerable<ArticleDetailDto>> GetArtileDetailsAsync(ArticleSerchDto article) 
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("CategoryId", article.CategoryId);
            para.Add("ProTypeId", article.ProTypeId);
            para.Add("ProGroupId", article.ProGroupId);
            para.Add("SubCatId", article.SubCatId);
            para.Add("CatId", article.CatId);


            var result = await DbConnection.QueryAsync<ArticleDetailDto>("spMstrArticleGetDetails", para
                , commandType: CommandType.StoredProcedure);           

            return result;
        }

         public async Task<IEnumerable<ArticleDetailDto>> GetCartonArtileDetailsAsync(ArticleSerchDto article) 
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("CategoryId", article.CategoryId);
            para.Add("ProTypeId", article.ProTypeId);
            para.Add("ProGroupId", article.ProGroupId);

            var result = await DbConnection.QueryAsync<ArticleDetailDto>("spMstrCartonArticleGetDetails", para
                , commandType: CommandType.StoredProcedure);           

            return result;
        }

        public async Task<IEnumerable<ArticleDetailDto>> GetArtileDetailsAllAsync(int companyId) 
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("CompanyId", companyId);

            var result = await DbConnection.QueryAsync<ArticleDetailDto>("spMstrArticleGetAllDetails", para
                , commandType: CommandType.StoredProcedure);           

            return result;
        }

        public async Task<IEnumerable<ArticleDetailDto>> GetActiveArtileDetailsAllAsync(int companyId) 
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("CompanyId", companyId);

            var result = await DbConnection.QueryAsync<ArticleDetailDto>("spMstrActiveArticleGetAllDetails", para
                , commandType: CommandType.StoredProcedure);           

            return result;
        }

        public async Task<int> DeactiveArticleAsync(MstrArticle article)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("ArticleId" , article.AutoId);
            para.Add("bActive" , article.IsActive);
            para.Add("UserId", article.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrArticleDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeactiveBrandCodeAsync(MstrBrandCode brandcode)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("BrandCodeId" , brandcode.AutoId);
            para.Add("bActive" , brandcode.IsActive);
            para.Add("UserId", brandcode.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrBrandCodeDeactivation", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeactiveAddChargeModulesAsync(MstrAddChargeModule addchargeM)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , addchargeM.AutoId);
            para.Add("bActive" , addchargeM.bActive);
            para.Add("UserId", addchargeM.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrAddChargeModuleDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeactiveSotrSiteAsync(MstrStoreSite storesite)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("SiteId" , storesite.AutoId);
            para.Add("bActive" , storesite.bActive);
            para.Add("UserId", storesite.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrSiteDeactivation", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeleteArticleAsync(MstrArticle article)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("ArticleId" , article.AutoId);
            para.Add("UserId", article.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrArticleDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Article

       
        #region Unit 
                    
        public async Task<int> SaveUnitAsync(MstrUnits mstrUnits)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , mstrUnits.AutoId);
            para.Add("Code", mstrUnits.Code.ToUpper().Trim());
            para.Add("Name", mstrUnits.Name.Trim());
            para.Add("UserId", mstrUnits.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrUnitsSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
        
        #endregion Unit


        #region Unit Conversion

        public async Task<int> SaveUnitConversionAsync(MstrUnitConversion unitConv)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , unitConv.AutoId);
            para.Add("FromUnitId", unitConv.FromUnitId);
            para.Add("ToUnitId", unitConv.ToUnitId);
            para.Add("Value", unitConv.Value);
            para.Add("UserId", unitConv.CreateUserId);            
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrUnitConversionSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }


        #endregion Unit Conversion

        #region Flute Type

        public async Task<int> SaveFluteTypeAsync(MstrFluteTypes fluteTypes)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , fluteTypes.AutoId);
            para.Add("Factor", fluteTypes.Factor);
            para.Add("Code", fluteTypes.Code.ToUpper());
            para.Add("LocationId", fluteTypes.LocationId);
            para.Add("UserId", fluteTypes.CreateUserId);            
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrFluteTypesSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Flute Type

        #region Sales Agent

        public async Task<int> SaveSalesAgentAsync(MstrSalesAgent salesAgent)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , salesAgent.AutoId);
            para.Add("Name", salesAgent.Name);
            para.Add("Email", salesAgent.Email);
            para.Add("LocationId", salesAgent.LocationId);
            para.Add("UserId", salesAgent.CreateUserId);            
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrSalesAgentSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Sales Agent

        #region Currency

        public async Task<int> SaveCurrencyAsync(MstrCurrency currency)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , currency.AutoId);
            para.Add("Name", currency.Name);
            para.Add("Code", currency.Code.ToUpper());
            para.Add("Symbol", currency.Symbol);
            para.Add("UserId", currency.CreateUserId);            
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrCurrencySave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Currency

        #region Address Type

        public async Task<int> SaveAddressTypeAsync(MstrAddressType addressType)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , addressType.AutoId);
            para.Add("AddressCode", addressType.AddressCode.ToUpper().Trim());
            para.Add("AddressName", addressType.AddressCodeName);
            para.Add("UserId", addressType.CreateUserId);            
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrAddressTypeSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Address Type

        #region Process
                   
        public async Task<int> SaveProcessAsync(MstrProcess masterProcess)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , masterProcess.AutoId);
            para.Add("Process", masterProcess.Process.Trim());
            para.Add("UserId", masterProcess.CreateUserId);
            para.Add("LocationId", masterProcess.LocationId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrProcessSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Process

        #region Code Definition
                   
        public async Task<int> SaveCodeDefinitionAsync(MstrCodeDefinition codeDefinition)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , codeDefinition.AutoId);
            para.Add("CategoryId", codeDefinition.CategoryId);
            para.Add("ProdTypeId", codeDefinition.ProdTypeId);
            para.Add("ProdGroupId", codeDefinition.ProdGroupId);
            para.Add("SortOrder", codeDefinition.SortOrder);
            para.Add("IsProductField", codeDefinition.IsProductField);
            para.Add("FlexFieldId", codeDefinition.FlexFieldId);
            para.Add("FieldName", codeDefinition.FieldName);
            para.Add("IsCode", codeDefinition.IsCode);
            para.Add("IsName", codeDefinition.IsName);
            para.Add("IsCounter", codeDefinition.IsName);
            para.Add("IsValue", codeDefinition.IsValue);
            para.Add("CounterPad", codeDefinition.CounterPad);
            para.Add("CounterStart", codeDefinition.CounterStart);
            para.Add("SeqNo", codeDefinition.SeqNo);
            para.Add("IsSeperator", codeDefinition.IsSeperator);
            para.Add("Seperator", codeDefinition.Seperator.Trim());
            para.Add("UserId", codeDefinition.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrCodeDefinitionSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeleteCodeDefinitionAsync(MstrCodeDefinition codeDefinition)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , codeDefinition.AutoId);            
            para.Add("UserId", codeDefinition.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrCodeDefinitionDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Code Definition


        #region Reject Reason
                   
        public async Task<int> SaveRejectReasonAsync(MstrRejectionReasons rejReasons)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , rejReasons.AutoId);
            para.Add("Details", rejReasons.Details.Trim());
            para.Add("UserId", rejReasons.CreateUserId);
            para.Add("LocationId", rejReasons.LocationId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spMstrRejectReasonSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Reject Reason


        #region StoreSite
                  
        public async Task<int> SaveStoresiteAsync(MstrStoreSite masterStoreSite)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , masterStoreSite.AutoId);
            para.Add("Code", masterStoreSite.SiteCode.ToUpper().Trim());
            para.Add("Name", masterStoreSite.SiteName.Trim());
            para.Add("UserId", masterStoreSite.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrStoresiteSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion StoreSite

        #region Countries
                  
        public async Task<int> SaveCountriesAsync(MstrCountries countries)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , countries.AutoId);
            para.Add("Code", countries.Code.ToUpper().Trim());
            para.Add("Name", countries.Name.Trim());
            para.Add("Alpha2Code", countries.Alpha2Code.ToUpper().Trim());
            para.Add("Alpha3Code", countries.Alpha3Code.ToUpper().Trim());
            para.Add("Numeric", countries.Numeric);
            para.Add("UserId", countries.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCountriesSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Countries

        #region Payment Terms
                  
        public async Task<int> SavePaymentTermsAsync(MstrPaymentTerm paymentTerm)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , paymentTerm.AutoId);
            para.Add("Code", paymentTerm.Code.ToUpper().Trim());
            para.Add("Name", paymentTerm.Name.Trim());
            para.Add("UserId", paymentTerm.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrPaymentTermsSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Payment Terms


        #region Customer Header
            
        public async Task<int> DeactiveCustomerHdAsync(MstrCustomerHeader mstrCustomerHeader)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoID" , mstrCustomerHeader.AutoId);
            para.Add("bActive", mstrCustomerHeader.bActive);
            para.Add("UserId", mstrCustomerHeader.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerHeaderDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
        
        public async Task<int> DeactiveCusLocAsync(MstrCustomerLocation cusLocation)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , cusLocation.AutoId);
            para.Add("bActive", cusLocation.bActive);
            para.Add("UserId", cusLocation.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerLocationDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<ReturnCustomerHdDto>> GetCustomerHdAllAsync(int LocId)
        {
            IEnumerable<ReturnCustomerHdDto> customerList;
            DynamicParameters para = new DynamicParameters();

            para.Add("LocationId" , LocId);

            customerList = await DbConnection.QueryAsync<ReturnCustomerHdDto>("spMstrCustomerHeaderGetDetails" , para
                    , commandType: CommandType.StoredProcedure);
            
            return customerList;
        } 
               
        public async Task<int> SaveCustomerHdAsync(MstrCustomerHeader mstrCustomerHeader)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , mstrCustomerHeader.AutoId);
            para.Add("Name", mstrCustomerHeader.Name.Trim());
            para.Add("Address", mstrCustomerHeader.Address.Trim());
            para.Add("Email", mstrCustomerHeader.Email.Trim());
            para.Add("Tel", mstrCustomerHeader.Tel.Trim());
            para.Add("LocationId", mstrCustomerHeader.LocationId);
            para.Add("ShortCode" , mstrCustomerHeader.ShortCode.ToUpper().Trim());
            para.Add("CustomerID", mstrCustomerHeader.CustomerID.Trim());
            para.Add("City", mstrCustomerHeader.City.Trim());
            para.Add("CountryId", mstrCustomerHeader.CountryId);
            para.Add("CurrencyId", mstrCustomerHeader.CurrencyId);
            para.Add("VATNo", mstrCustomerHeader.VATNo.Trim());
            para.Add("TaxNo", mstrCustomerHeader.TaxNo.Trim());
            para.Add("TinNo", mstrCustomerHeader.TinNo.Trim());
            para.Add("ZipPostalCode", mstrCustomerHeader.ZipPostalCode.Trim());
            para.Add("CreditDays", mstrCustomerHeader.CreditDays);
            para.Add("Attention", mstrCustomerHeader.Attention.Trim());
            para.Add("UserId", mstrCustomerHeader.CreateUserId);
            para.Add("InvTypeId", mstrCustomerHeader.InvTypeId);
            para.Add("CusTypeId", mstrCustomerHeader.CusTypeId);
            para.Add("TaxId", mstrCustomerHeader.TaxId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerHeaderSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Customer Header


        #region Customer Location
              
        public async Task<int> SaveCustomerLocAsync(MstrCustomerLocation customerLocation)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , customerLocation.AutoId);
            para.Add("Name", customerLocation.Name.Trim());
            para.Add("ShortCode", customerLocation.ShortCode.Trim().ToUpper());
            para.Add("Address", customerLocation.Address.Trim());
            para.Add("Email", customerLocation.Email.Trim());
            para.Add("Tel", customerLocation.Tel.Trim());
            para.Add("CustomerId", customerLocation.CustomerId);
            para.Add("UserId", customerLocation.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerLocationSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Customer Location

        #region Customer User 

        public async Task<int> SaveCustomerUserAsync(MstrCustomerUsers customerUser)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , customerUser.AutoId);
            para.Add("Title", customerUser.Title);
            para.Add("FirstName", customerUser.FirstName.Trim());
            para.Add("LastName", customerUser.LastName.Trim());
            para.Add("Email", customerUser.Email.Trim());
            para.Add("Designation", customerUser.Designation.Trim());
            para.Add("CustomerId", customerUser.CustomerId);
            para.Add("UserId", customerUser.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerUserSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeactiveCustomerUserAsync(MstrCustomerUsers cusUser)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("CusUserId" , cusUser.AutoId);
            para.Add("IsActive", cusUser.IsActive);
            para.Add("UserId", cusUser.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerUserDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }


        #endregion Customer User

        #region Customer Address   

        public async Task<IEnumerable<ReturnCustomerAddDto>> GetCustomerAddressAsync(int customerId)
        {
            IEnumerable<ReturnCustomerAddDto> customeraddList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerId" , customerId);

            customeraddList = await DbConnection.QueryAsync<ReturnCustomerAddDto>("spMstrCustomerAddressGetDt" , para
                    , commandType: CommandType.StoredProcedure);
            
            return customeraddList;
        } 

        //public async Task<IEnumerable<ReturnCustomerAddDto>> GetActiveCustomerAddressAsync(int customerId)
        //{
        //    IEnumerable<ReturnCustomerAddDto> customeraddList;
        //    DynamicParameters para = new DynamicParameters();

        //    para.Add("CustomerId" , customerId);

        //    customeraddList = await DbConnection.QueryAsync<ReturnCustomerAddDto>("spMstrActiveCustomerAddressGetDt" , para
        //            , commandType: CommandType.StoredProcedure);
            
        //    return customeraddList;
        //} 

        public async Task<int> SaveCusAddressAsync(MstrCustomerAddressList cusAddressList)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , cusAddressList.AutoId);
            para.Add("CustomerLocId", cusAddressList.CusLocationId);
            para.Add("AddressTypeId ", cusAddressList.AddressTypeId);
            para.Add("CustomerID", cusAddressList.CustomerId);
            para.Add("City", cusAddressList.City.Trim());
            para.Add("CountryId", cusAddressList.CountryId);
            para.Add("CurrencyId", cusAddressList.CurrencyId);
            para.Add("VATNo", cusAddressList.VatNo.Trim());
            para.Add("TaxNo", cusAddressList.TaxNo.Trim());
            para.Add("TinNo", cusAddressList.TinNo.Trim());
            para.Add("ZipPostalCode", cusAddressList.ZipPostalCode.Trim());
            para.Add("AddressTo", cusAddressList.AddressTo.Trim());
            para.Add("Address", cusAddressList.Address.Trim());
            para.Add("Email", cusAddressList.Email.Trim());
            para.Add("Tel", cusAddressList.Tel.Trim()); 
            para.Add("UserId", cusAddressList.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerAddressSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeactiveCustomerAddAsync(MstrCustomerAddressList cusAdd)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("CusAddressId" , cusAdd.AutoId);
            para.Add("IsActive", cusAdd.bActive);
            para.Add("UserId", cusAdd.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerAddressDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

         #endregion Customer Address

        #region Customer Division
            
        public async Task<int> SaveCustomerDivisionAsync(MstrCustomerDivision cusDivision)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , cusDivision.AutoId);
            para.Add("Details", cusDivision.Details.Trim());
            para.Add("CustomerId", cusDivision.CustomerId);
            para.Add("UserId", cusDivision.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerDivisionSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DisableCusDivisionAsync(MstrCustomerDivision cusDivision)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("IsActive" , cusDivision.bActive);
            para.Add("CusDivisionId", cusDivision.AutoId);
            para.Add("UserId", cusDivision.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerDivisionDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }  

        #endregion Customer Division

        #region Customer Brand

        public async Task<int> SaveCustomerBrandAsync(MstrCustomerBrand customerBrand)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , customerBrand.AutoId);
            para.Add("BrandId", customerBrand.BrandId);
            para.Add("CustomerId", customerBrand.CustomerId);
            para.Add("UserId", customerBrand.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerBrandSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeleteCusBrandAsync(MstrCustomerBrand customerBrand)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , customerBrand.AutoId);
            para.Add("CustomerId", customerBrand.CustomerId);
            para.Add("BrandId", customerBrand.BrandId);
            para.Add("UserId", customerBrand.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerBrandDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }  

        #endregion Customer Brand

        #region Customer Currency
            
        public async Task<int> SaveCustomerCurrencyAsync(MstrCustomerCurrency customercurrency)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , customercurrency.AutoId);
            para.Add("CurrencyId", customercurrency.CurrencyId);
            para.Add("CustomerId", customercurrency.CustomerId);
            para.Add("UserId", customercurrency.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerCurrencySave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeleteCusCurrencyAsync(MstrCustomerCurrency customerCurrency)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , customerCurrency.AutoId);
            para.Add("CurrencyId", customerCurrency.CurrencyId);
            para.Add("UserId", customerCurrency.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerCurrencyDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Customer Currency

        
        #region Material Type
            
        public async Task<int> SaveMaterialTypeAsync(MstrMaterialType MstrMaterialType)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , MstrMaterialType.AutoId);
            para.Add("Code", MstrMaterialType.Code.ToUpper().Trim());
            para.Add("Name", MstrMaterialType.Name.Trim());
            para.Add("UserId", MstrMaterialType.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrMeterialTypeSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Material Type
        

        #region Category

        public async Task<int> SaveCategoryAsync(MstrCategory MstrCategory)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , MstrCategory.AutoId);
            para.Add("Code", MstrCategory.Code.ToUpper().Trim());
            para.Add("Name", MstrCategory.Name.Trim());
            para.Add("UserId", MstrCategory.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCategorySave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
            
        #endregion Category
        
        
        #region Brand

        public async Task<int> SaveBrandAsync(MstrBrand MstrBrand)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , MstrBrand.AutoId);
            para.Add("Name", MstrBrand.Name.Trim());
            para.Add("UserId", MstrBrand.CreateUserId);
            para.Add("LocationId", MstrBrand.LocationId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrBrandSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> SaveBrandCodeAsync(MstrBrandCode MstrBrandCode)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , MstrBrandCode.AutoId);
            para.Add("Name", MstrBrandCode.Name.Trim());
            para.Add("UserId", MstrBrandCode.CreateUserId);
            para.Add("BrandId", MstrBrandCode.BrandId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrBrandCodeSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

            
        #endregion Brand

        #region Prod Definition

        public async Task<ReturnDto> SaveProdDefinitionAsync(ProdDefinitionDto prodDefinitionDto)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("PdHeaderId" , prodDefinitionDto.PDHeaderId);
            para.Add("ProcessId", prodDefinitionDto.ProcessId);
            para.Add("Name", prodDefinitionDto.PDName.Trim().ToUpper());
            para.Add("ReceiveSiteId", prodDefinitionDto.ReceiveSiteId);
            para.Add("DispatchSiteId", prodDefinitionDto.DispatchSiteId);
            para.Add("UserId", prodDefinitionDto.CreateUserId);
            //para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spMstrProductDefinitionSave", para
                , commandType: CommandType.StoredProcedure);            

            return result;
        }

        public async Task<int> DeleteProdDefinitionAsync(ProdDefinitionDto prodDefDto)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("PdDetailId" , prodDefDto.AutoId);
            para.Add("PdHeaderId" , prodDefDto.PDHeaderId);
            para.Add("UserId", prodDefDto.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrProductDefinitionDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<ProdDefinitionDto>> GetProdDefinitionAsync(byte ProdHeaderId)
        {
            IEnumerable<ProdDefinitionDto> podDefiList;
            DynamicParameters para = new DynamicParameters();

            para.Add("ProdHeaderId" , ProdHeaderId);
            podDefiList = await DbConnection.QueryAsync<ProdDefinitionDto>("spMstrProductDefinationGet" , para
                    , commandType: CommandType.StoredProcedure);
            
            return podDefiList;
        }
            
        #endregion Prod Definition
        
        #region Product Group

        public async Task<int> SaveProductGroupAsync(MstrProductGroup MstrProductGroup)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , MstrProductGroup.AutoId);
            // para.Add("ProdTypeId", MstrProductGroup.ProdTypeId);
            para.Add("ProdGroupName", MstrProductGroup.ProdGroupName.Trim());
            para.Add("ProdGroupCode", MstrProductGroup.ProdGroupCode.Trim().ToUpper());
            //para.Add("SerialNo", MstrProductGroup.SerialNo);
            para.Add("UserId", MstrProductGroup.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrProductGroupSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeactiveProdGroupAsync(MstrProductGroup MstrProductGroup)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , MstrProductGroup.AutoId);  
            para.Add("IsActive" , MstrProductGroup.IsActive);         
            para.Add("UserId", MstrProductGroup.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrProductGroupDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<ProdGroupDto>> GetProductGroupAsync(int ProdTypeId)
        {
            IEnumerable<ProdGroupDto> prodGroupList;
            DynamicParameters para = new DynamicParameters();

            para.Add("ProdTypeId" , ProdTypeId);
            
            prodGroupList = await DbConnection.QueryAsync<ProdGroupDto>("spMstrProductGroupGetDt" , para
                    , commandType: CommandType.StoredProcedure);
            
            return prodGroupList;
        }

        public async Task<IEnumerable<ProdTypeGroupDto>> GetProdTypeGroupAsync(int prodTypeId)
        {
            IEnumerable<ProdTypeGroupDto> prodGroupList;
            DynamicParameters para = new DynamicParameters();

            para.Add("ProdTypeId" , prodTypeId);

            prodGroupList = await DbConnection.QueryAsync<ProdTypeGroupDto>("spMstrProdTypeGroupGetDt" , para
                    , commandType: CommandType.StoredProcedure);
            
            return prodGroupList;
        }

        public async Task<int> AssignProdTypeGroupAsync(List<MstrProdTypeGroup> prod)
        {
            DynamicParameters para = new DynamicParameters();
            DataTable prodTypeDT = new DataTable();

            prodTypeDT.Columns.Add("UserId",typeof(int));
            prodTypeDT.Columns.Add("ProdTypeId",typeof(int));
            prodTypeDT.Columns.Add("ProdGroupId",typeof(int));

            foreach (var item in prod)
            {
                prodTypeDT.Rows.Add( item.CreateUserId
                        , item.ProdTypeId
                        , item.ProdGroupId);
            } 

            para.Add("ProdGroupDT", prodTypeDT.AsTableValuedParameter("ProdGroupList"));  
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);        

             var result = await DbConnection.ExecuteAsync("spMstrProdTypeGroupSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeleteProdTypeGroupAsync(List<MstrProdTypeGroup> prod)
        {
            DynamicParameters para = new DynamicParameters();
            DataTable prodTypeDT = new DataTable();

            prodTypeDT.Columns.Add("UserId",typeof(int));
            prodTypeDT.Columns.Add("ProdTypeId",typeof(int));
            prodTypeDT.Columns.Add("ProdGroupId",typeof(int));

            foreach (var item in prod)
            {
                prodTypeDT.Rows.Add( item.CreateUserId
                        , item.ProdTypeId
                        , item.ProdGroupId);
            } 

            para.Add("ProdGroupDT", prodTypeDT.AsTableValuedParameter("ProdGroupList"));  
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);        

             var result = await DbConnection.ExecuteAsync("spMstrProdTypeGroupDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
            
        #endregion Product Group

        #region Product Type

        public async Task<int> AssignCatProdTypeAsync(List<MstrCatProductType> prod)
        {
            DynamicParameters para = new DynamicParameters();
            DataTable prodTypeDT = new DataTable();

            prodTypeDT.Columns.Add("UserId",typeof(int));
            prodTypeDT.Columns.Add("CategoryId",typeof(int));
            prodTypeDT.Columns.Add("ProdTypeId",typeof(int));

            foreach (var item in prod)
            {
                prodTypeDT.Rows.Add( item.CreateUserId
                        , item.CategoryId
                        , item.ProdTypeId);
            } 

            para.Add("ProdTypeDT", prodTypeDT.AsTableValuedParameter("ProdTypeList"));  
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);        

             var result = await DbConnection.ExecuteAsync("spMstrCatProductTypeSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");     

        }

        public async Task<int> DeleteCatProdTypeAsync(List<MstrCatProductType> prod)
        {
            DynamicParameters para = new DynamicParameters();
            DataTable prodTypeDT = new DataTable();

            prodTypeDT.Columns.Add("UserId",typeof(int));
            prodTypeDT.Columns.Add("CategoryId",typeof(int));
            prodTypeDT.Columns.Add("ProdTypeId",typeof(int));

            foreach (var item in prod)
            {
                prodTypeDT.Rows.Add( item.CreateUserId
                        , item.CategoryId
                        , item.ProdTypeId);
            } 

            para.Add("ProdTypeDT", prodTypeDT.AsTableValuedParameter("ProdTypeList"));  
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);        

             var result = await DbConnection.ExecuteAsync("spMstrCatProductTypeDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");     

        }

        public async Task<int> SaveProductTypeAsync(MstrProductType MstrProductType)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , MstrProductType.AutoId);
            // para.Add("CategoryId", MstrProductType.CategoryId);
            para.Add("ProdTypeName", MstrProductType.ProdTypeName.Trim());
            para.Add("ProdTypeCode", MstrProductType.ProdTypeCode.Trim().ToUpper());
            para.Add("AutoArticle", MstrProductType.bAutoArticle);
            para.Add("UserId", MstrProductType.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrProductTypeSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<CatProdTypeDto>> GetCatProductTypeDtAsync(int catId)
        {
            IEnumerable<CatProdTypeDto> prodTypeList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CategoryId" , catId);

            prodTypeList = await DbConnection.QueryAsync<CatProdTypeDto>("spMstrCatProductTypeGetDt" , para
                    , commandType: CommandType.StoredProcedure);
            
            return prodTypeList;
        }

        public async Task<int> DeactProductTypeAsync(MstrProductType MstrProductType)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , MstrProductType.AutoId);
            para.Add("IsActive", MstrProductType.IsActive);
            para.Add("UserId", MstrProductType.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrProductTypeDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

            
        #endregion Product Type

        #region Product SubCategory

        public async Task<int> SaveProductSubCatAsync(MstrProductSubCat MstrProductSubCat)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , MstrProductSubCat.AutoId);
            para.Add("ProdSubCatName", MstrProductSubCat.ProdSubCatName.Trim());
            para.Add("ProdSubCatCode", MstrProductSubCat.ProdSubCatCode.Trim().ToUpper());
            para.Add("ProdGroupId", MstrProductSubCat.ProdGroupId);
            para.Add("UserId", MstrProductSubCat.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrProductSubCategorySave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeactiveProdSubCatAsync(MstrProductSubCat MstrProductSubCat)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , MstrProductSubCat.AutoId);
            para.Add("IsActive" , MstrProductSubCat.IsActive);
            para.Add("UserId", MstrProductSubCat.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrProductSubCatDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<ProductSubCatDto>> GetProductSubCatAsync(int ProdGroupId)
        {
            IEnumerable<ProductSubCatDto> prodSubCatList;
            DynamicParameters para = new DynamicParameters();

            para.Add("ProdGroupId" , ProdGroupId);

            prodSubCatList = await DbConnection.QueryAsync<ProductSubCatDto>("spMstrProductSubCategoryGetDt" , para
                    , commandType: CommandType.StoredProcedure);
            
            return prodSubCatList;
        }
            
        #endregion Product SubCategory

        #region Serial No Details

        public async Task<int> SaveSequenceSetAsync(TransSequenceSettings seqSettings)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , seqSettings.AutoId);
            para.Add("TransType", seqSettings.TransType);
            para.Add("Prefix", seqSettings.Prefix.Trim().ToUpper());
            para.Add("SeqLength", seqSettings.SeqLength);
            para.Add("SeqNo", seqSettings.SeqNo);
            // para.Add("CurrentYea", seqSettings.CurrentYear);
            para.Add("LocationId", seqSettings.LocationId);
            para.Add("UserId", seqSettings.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spTransSequenceSettingsSave", para 
                    , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
            
        #endregion Serial No Details      


        #region CostGroup

        public async Task<int> SaveCostGroupAsync(MstrCostingGroup MstrCostingGroup)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , MstrCostingGroup.AutoId);
            para.Add("LocationId", MstrCostingGroup.LocationId);
            para.Add("Name", MstrCostingGroup.Name.Trim());
            para.Add("UserId", MstrCostingGroup.CreateUserId);
            para.Add("IsMaterialAlloc", MstrCostingGroup.IsMaterialAllocated);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCostingGroupSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
            
        #endregion CostGroup       

        
        #region Flex Field Details
            
        public async Task<int> SaveFlexFieldDetailsAsync(MstrFlexFieldDetails flexDetails)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , flexDetails.AutoId);
            para.Add("FieldCode", flexDetails.FlexFieldCode.ToUpper().Trim());
            para.Add("FieldName", flexDetails.FlexFieldName.Trim());
            para.Add("CategoryId", flexDetails.CategoryId);
            para.Add("ProdTypeId", flexDetails.ProdTypeId);
            para.Add("ModuleId", flexDetails.ModuleId);
            para.Add("DataType", flexDetails.DataType);
            para.Add("ValueList", flexDetails.ValueList);
            para.Add("Mandatory", flexDetails.Mandatory);
            para.Add("UserId", flexDetails.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrFlexFeildDetailsSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<FlexFieldReturnDto>> GetFlexFieldDtAsync(int CategoryId)
        {
            IEnumerable<FlexFieldReturnDto> flexFieldList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CategoryId" , CategoryId);

            flexFieldList = await DbConnection.QueryAsync<FlexFieldReturnDto>("spMstrFlexFeildDetailsGet" , para
                    , commandType: CommandType.StoredProcedure);
            
            return flexFieldList;
        }

        public async Task<int> DeactiveFlexFieldDtAsync(MstrFlexFieldDetails flexFieldDt)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , flexFieldDt.AutoId);
            para.Add("IsActive" , flexFieldDt.isActive);
            para.Add("UserId", flexFieldDt.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrFlexFeildDetailsDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        #endregion Flex Field Details

    
        #region Flex Field ValueList

        public async Task<int> SaveFlexFieldValListAsync(MstrFlexFieldValueList flexDetailsVal)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , flexDetailsVal.AutoId);
            para.Add("FlexFieldDtId", flexDetailsVal.FlexFieldId);
            para.Add("FlexFieldValue", flexDetailsVal.FlexFeildVlaue.Trim());           
            para.Add("UserId", flexDetailsVal.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrFlexFieldValueListSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeleteFlexFieldValListAsync(MstrFlexFieldValueList flexDetailsVal)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , flexDetailsVal.AutoId);          
            para.Add("UserId", flexDetailsVal.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrFlexFieldValueListDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
            
        #endregion Flex Field ValueList  


        #region User Approve Module

        public async Task<IEnumerable<UserAppModuleDto>> GetUserAppModuleDtAsync(int userId)
        {
            IEnumerable<UserAppModuleDto> approveModule;
            DynamicParameters para = new DynamicParameters();

            para.Add("UserId" , userId);

            approveModule = await DbConnection.QueryAsync<UserAppModuleDto>("spTransApprovalRouteModulesGetMenus" , para
                    , commandType: CommandType.StoredProcedure);
            
            return approveModule;
        }

        public async Task<int> SaveApproveRouteModuleAsync(TransApprovalRoutingModules appModule)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , appModule.AutoId);
            para.Add("CreateUser", appModule.CreateUserId);
            para.Add("ModuleId", appModule.MenuId);   
            para.Add("BuyPass", appModule.BuyPass);          
            para.Add("UserId", appModule.UserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spTransApprovalRouteModulesSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<ApprovalUsersDto>> GetApproveUsersAsync(int ARMId)
        {
            IEnumerable<ApprovalUsersDto> appUserList;
            DynamicParameters para = new DynamicParameters();

            // para.Add("UserId" , appUsers.UserId);
            para.Add("ARMId" , ARMId);

            appUserList = await DbConnection.QueryAsync<ApprovalUsersDto>("spTransApproversByModuleGetUsers" , para
                    , commandType: CommandType.StoredProcedure);
            
            return appUserList;
        }

        public async Task<int> SaveApproveUserAsync(TransApproversByModule appUsers)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , appUsers.AutoId);
            para.Add("CreateUser", appUsers.CreateUserId);
            para.Add("IsDefault ", appUsers.isDefault); 
            para.Add("IsFinalApprove", appUsers.isFinalApprove);         
            para.Add("UserId", appUsers.UserId);
            para.Add("ARMId", appUsers.ARMId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spTransApproversByModuleSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeleteApproveModuleAsync(TransApprovalRoutingModules appModule)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("UserId" , appModule.CreateUserId);
            para.Add("ARMId", appModule.AutoId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spTransApprovalRouteModulesDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeleteApproveUsersAsync(TransApproversByModule appUsers)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("UserId" , appUsers.CreateUserId);
            para.Add("AutoId", appUsers.AutoId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spTransApproversByModuleDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }


        #endregion User Approve Module    
      

        #region Customer Type Module  
        public async Task<int> SaveCustomerTypeAsync(MstrCustomerType customerType)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , customerType.AutoId);
            para.Add("Details", customerType.Details);
            para.Add("UserId", customerType.CreateUserId);            
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerTypeSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
        #endregion Customer Type Module

        #region Invoice Type Module
        public async Task<int> SaveInvoiceTypeAsync(MstrInvoiceType invoiceType)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId", invoiceType.AutoId);
            para.Add("Details", invoiceType.Details);
            para.Add("FormatName", invoiceType.FormatName);
            para.Add("UserId", invoiceType.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spMstrInvoiceTypeSave", para ,
            commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }
        #endregion Invoice Type Module  

        #region Payment Mode
        public async Task<int> SavePaymentModeAsync(MstrPaymentMode paymentMode)
        {   
            DynamicParameters para = new DynamicParameters();

            para.Add ("AutoId", paymentMode.AutoId);
            para.Add ("Name", paymentMode.Name);
            para.Add("UserId", paymentMode.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
           
            var result = await DbConnection.ExecuteAsync("spMstrPaymentModeSave", para ,
            commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }
        #endregion Payment Mode 
        #region DispatchSite
            public async Task<int> SaveDispatchSiteAsync(MstrDispatchSite dispatchSite)
            {
                DynamicParameters para = new DynamicParameters();

                para.Add("AutoId", dispatchSite.AutoId);
                para.Add("DispatchId",dispatchSite.DispatchId);
                para.Add("UserId",dispatchSite.CreateUserId);
                para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = await DbConnection.ExecuteAsync("spMstrDispatchSiteSave", para ,
                 commandType: CommandType.StoredProcedure);

                return para.Get<int>("Result");
        }
        #endregion DispatchSite
        #region SpecialInstruction
        public async Task<int> SaveSpecialInstructionAsync (MstrSpecialInstruction specialInstruction)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , specialInstruction.AutoId);
            para.Add("Description" ,specialInstruction.Description);
            para.Add("UserId" , specialInstruction.CreateUserId);
            para.Add("@Result" , dbType: DbType.Int32 ,direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spMsterSpecialInstructionSave" , para ,
            commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }
        #endregion  SpecialInstruction
        
        #region MasterCompany
        public async Task<int> SaveMasterCompanyAsync (MstrCompany company)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId", company.AutoId);
            para.Add("CompanyName", company.CompanyName);
            para.Add("Address", company.Address); 
            para.Add("DefaultCurrencyId", company.DefCurrencyId);
            para.Add("SVAT", company.SVATNo);
            para.Add("BOIRegNo", company.BOIRegNo);
            para.Add("UserId", company.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32 ,direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spMstrMasterCompanySave" , para ,
            commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }
        #endregion MasterCompany

        #region MasterCartonType
        public async Task<int> SaveMasterCartonTypeAsync (MstrCartonType cartonType)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId", cartonType.AutoId);
            para.Add("Name", cartonType.Name);
            para.Add("Description", cartonType.Description);
            para.Add("UserId", cartonType.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32 ,direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spMstrCartonTypeSave" , para ,
            commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<MstrCartonType>> GetCartonBoxTypeAsync(int articleId)
        {   
            IEnumerable<MstrCartonType> CartonBoxTypeList;
            DynamicParameters para = new DynamicParameters();

            para.Add("ArticleId" , articleId);
        
            CartonBoxTypeList = await DbConnection.QueryAsync<MstrCartonType>("spMstCartonBoxTypeGet" , para
                    , commandType: CommandType.StoredProcedure);
            
            return CartonBoxTypeList;
        } 
        #endregion MasterCartonType

        public async Task<int> SaveArticleBrandcodeAsync(MstrArticleBrandcode ArticleBrandcode)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("ArticleId" , ArticleBrandcode.ArticleId);
            para.Add("BrandCodeId" , ArticleBrandcode.BrandCodeId);
            para.Add("ColorId" , ArticleBrandcode.ColorId);
            para.Add("SizeId" , ArticleBrandcode.SizeId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrArticleBrandCodeSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
        public async Task<int> DeleteArticleBrandCodeMappingAsync(MstrCustomerBrand customerBrand)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , customerBrand.AutoId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrArticleBrandCodeDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<ReportSelectDateDto>> getDateSelectionAsync(int Id)
        {
            IEnumerable<ReportSelectDateDto> dateList;
            DynamicParameters para = new DynamicParameters();

            para.Add("ReqId" , Id);

            dateList= await DbConnection.QueryAsync<ReportSelectDateDto>("spGETdateRange" , para
                    , commandType: CommandType.StoredProcedure);
            
            return dateList;
        } 
        public async Task<int> SaveCustomerOtherCodeAsync(MstrCustomerOtherCode CustomerOtherCode)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId", CustomerOtherCode.AutoId);
            para.Add("CodeValue" , CustomerOtherCode.CodeHeaderValue);
            para.Add("UserId", CustomerOtherCode.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerOtherCodeSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> SaveCustomerOtherAsync(MstrCustomerOther CustomerOther)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId", CustomerOther.AutoId);
            para.Add("CustomerId", CustomerOther.CustomerId);
            para.Add("CustOtherId", CustomerOther.CustOtherId);
            para.Add("Description" , CustomerOther.Description);
            para.Add("UserId", CustomerOther.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrCustomerOtherSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
        public async Task<int> SaveUerMasterSettingsAsync(MstrUserMasterSetting UserMasterSettings)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("UserMasterSetId", UserMasterSettings.UserMasterSetId);
            para.Add("AgentId", UserMasterSettings.AgentId);
            para.Add("bIntent", UserMasterSettings.bIntent);
            para.Add("CostAtt", UserMasterSettings.CostAtt);
            para.Add("UserId", UserMasterSettings.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrUserMasterSettingSave", para
                , commandType: CommandType.StoredProcedure);            
            return para.Get<int>("Result");
        }
        public async Task<int> saveUserSiteAsync(UserSiteDto UserSite)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("TypeId" , UserSite.TypeId);
            para.Add("SiteId", UserSite.SiteId);
            para.Add("AgentId", UserSite.AgentId);
            para.Add("UserId", UserSite.createUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrUserSiteSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
        public async Task<IEnumerable<UserSiteDto>> GetUserSiteList(int AgentId)
        {   
            IEnumerable<UserSiteDto> userSiteList;
            DynamicParameters para = new DynamicParameters();

            para.Add("AgentId" , AgentId);
        
            userSiteList = await DbConnection.QueryAsync<UserSiteDto>("spMstrUserSiteGetList" , para
                    , commandType: CommandType.StoredProcedure);
            
            return userSiteList;
        } 
        [System.Obsolete]
        public async Task<int> DeleteUserSiteListAsync(List<UserSiteDto> siteList)
        {
            DataTable SiteUserDT = new DataTable();

            SiteUserDT.Columns.Add("AgentId",typeof(int));
            SiteUserDT.Columns.Add("UserSiteId",typeof(int));
            SiteUserDT.Columns.Add("CreUserID",typeof(int));

            foreach (var item in siteList)
            {
                SiteUserDT.Rows.Add( item.AgentId
                        , item.UserSiteId
                        , item.createUserId);
            }

            var SiteUserDetails = new SqlParameter("@SiteUserDT", SqlDbType.Structured); 
            SiteUserDetails.Value = SiteUserDT;
            SiteUserDetails.TypeName = "[dbo].[SiteUserType]";

            var Result = new SqlParameter("@Result", SqlDbType.Int);
            Result.Direction = ParameterDirection.Output;            

             await _context.Database
            .ExecuteSqlCommandAsync(@"exec spSiteUserDelete @SiteUserDT,@Result out" 
                ,SiteUserDetails,Result );

            return int.Parse(Result.Value.ToString());      
        }
        public async Task<IEnumerable<UserReportList>> GetUserReportList(int userId)
        {
            var AgentId = new SqlParameter("AgentId", userId);
            return await _context.UserReportList
                .FromSqlRaw("exec spReportUserGetList @AgentId" , AgentId).ToListAsync();
        }


        [System.Obsolete]
        public async Task<int> SaveUserReportListAsync(List<ReportUserDto> reportList)
        {
            DataTable ReportUserDT = new DataTable();

            ReportUserDT.Columns.Add("AgentId",typeof(int));
            ReportUserDT.Columns.Add("ReportId",typeof(int));
            ReportUserDT.Columns.Add("CreUserID",typeof(int));

            foreach (var item in reportList)
            {
                ReportUserDT.Rows.Add( item.AgentId
                        , item.ReportId
                        , item.CreUserID);
            }         

            var ReportUserDetails = new SqlParameter("@ReportUserDT", SqlDbType.Structured); 
            ReportUserDetails.Value = ReportUserDT;
            ReportUserDetails.TypeName = "[dbo].[ReportUserType]";

            var Result = new SqlParameter("@Result", SqlDbType.Int);
            Result.Direction = ParameterDirection.Output;            

             await _context.Database
            .ExecuteSqlCommandAsync(@"exec spReportUserSave @ReportUserDT,@Result out" 
                ,ReportUserDetails,Result );

            return int.Parse(Result.Value.ToString());      

        }

        [System.Obsolete]
        public async Task<int> DeleteUserReportListAsync(List<ReportUserDto> reportList)
        {
            DataTable ReportUserDT = new DataTable();

            ReportUserDT.Columns.Add("AgentId",typeof(int));
            ReportUserDT.Columns.Add("ReportId",typeof(int));
            ReportUserDT.Columns.Add("CreUserID",typeof(int));

            foreach (var item in reportList)
            {
                ReportUserDT.Rows.Add( item.AgentId
                        , item.ReportId
                        , item.CreUserID);
            }

            var ReportUserDetails = new SqlParameter("@ReportUserDT", SqlDbType.Structured); 
            ReportUserDetails.Value = ReportUserDT;
            ReportUserDetails.TypeName = "[dbo].[ReportUserType]";

            var Result = new SqlParameter("@Result", SqlDbType.Int);
            Result.Direction = ParameterDirection.Output;            

             await _context.Database
            .ExecuteSqlCommandAsync(@"exec spReportUserDelete @ReportUserDT,@Result out" 
                ,ReportUserDetails,Result );

            return int.Parse(Result.Value.ToString());      
        }

        #region Master Serial No Inventory
        public async Task<RefNumDto> GetInventoryRefNoAsync(MstrSerialNoInventory serialNo)
        {
           RefNumDto refNumDto;
           DynamicParameters para = new DynamicParameters();

            para.Add("TransType" , serialNo.TransType);
            para.Add("SiteId" , serialNo.SiteId);
            para.Add("AgentId" , serialNo.CreateUserId);

           refNumDto = await DbConnection.QuerySingleAsync<RefNumDto>("spTransSerialNoInvGet", para
               , commandType: CommandType.StoredProcedure);

           return refNumDto;
        }

        #endregion

        #region User Master Setting
        public async Task<IEnumerable<MstrAgents>> GetUserMasterSettingAsync()
        {
            IEnumerable<MstrAgents> agents = await DbConnection.QueryAsync<MstrAgents>("spMstrUserMasterSettingGetIntUser", null
                     , commandType: CommandType.StoredProcedure);

            return agents;           
        }
        #endregion User Master Setting

        #region SalsesDashBoardStats
        public async Task<DashBoardSalesDto> GetDashBoardOneDataAsync(dashBoardOneDto dashboardDt)
        {
            DashBoardSalesDto dashBoard = new DashBoardSalesDto();
            DynamicParameters para = new DynamicParameters();

            para.Add("transdate" , dashboardDt.transDate);
            para.Add("toDate" , dashboardDt.toDate);

            using (var multi = await DbConnection.QueryMultipleAsync("spDashboard_01Test", para, commandType: CommandType.StoredProcedure))
            {   
                dashBoard.DashBoardStats = multi.Read<DashBoardStatsDto>();
                dashBoard.AnualSales = multi.Read<DashBoardAnualSalesDto>();
                dashBoard.BrandCodeSales = multi.Read<DashBoardBrandCodeSalesDto>();
                dashBoard.MonthlySales = multi.Read<DashBoardMonthlySalesDto>();
		        dashBoard.MonthlyProductTypeSales = multi.Read<DashBoardMonthlyProdTypeSalesDto>();
                dashBoard.MonthlyDispatchSales = multi.Read<DashBoardMonthlyDispatchSalesDto>();
            }
            return dashBoard;
        }
        #endregion SalsesDashBoardStats

        #region SalesDashBoardChart
        public async Task<IEnumerable<DashboardExpChartDto>> GetDashBoardChartDetailsAsync(DashboardExpChartSearchDto dash)
        {
            IEnumerable<DashboardExpChartDto> dashDetails;
            DynamicParameters para = new DynamicParameters();

            para.Add("transdate" , dash.transDate);
            para.Add("toDate", dash.toDate);
            para.Add("Action", dash.Action);

            dashDetails = await DbConnection.QueryAsync<DashboardExpChartDto>("spTransGetTotalSalesDashValue" , para
                    , commandType: CommandType.StoredProcedure);
            return dashDetails;
        }
        #endregion SalesDashBoardChart

        #region Master Ports
        public async Task<int> SaveUerMasterPortsAsync(MstrPorts Ports)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("PortId", Ports.PortId);
            para.Add("PortCode", Ports.PortCode);
            para.Add("PortName", Ports.PortName);
            para.Add("CountryId", Ports.CountryId);
            para.Add("Loading", Ports.PortOfLoading);
	        para.Add("Dispatch", Ports.PortOfDischarge);
	        para.Add("UserId",Ports.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrPortSave", para
                , commandType: CommandType.StoredProcedure);            
            return para.Get<int>("Result");
        }
        #endregion Master Ports

        #region Supplier Header
        public async Task<int> SaveSupplierHdAsync(TransSupplierHeader transSupplierHeader)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("SupplierId" , transSupplierHeader.SupplierId);
            para.Add("Name", transSupplierHeader.Name);
            para.Add("CompanyId", transSupplierHeader.CompanyId);
            para.Add("ShortCode" , transSupplierHeader.ShortCode.ToUpper());
            para.Add("Address", transSupplierHeader.Address);
            para.Add("City", transSupplierHeader.City);
            para.Add("State",transSupplierHeader.State);
            para.Add("ZipPostalCode", transSupplierHeader.ZipPostalCode);
            para.Add("CountryId", transSupplierHeader.CountryId);
            para.Add("Tel", transSupplierHeader.Tel);
            para.Add("Email", transSupplierHeader.Email);
            para.Add("CurrencyId", transSupplierHeader.CurrencyId);
            para.Add("SupTypeId", transSupplierHeader.SupTypeId);
            para.Add("VATNo", transSupplierHeader.VATNo);
            para.Add("TaxNo", transSupplierHeader.TaxNo);
            para.Add("TinNo", transSupplierHeader.TinNo);
            para.Add("CreditDays", transSupplierHeader.CreditDays);
            para.Add("ShipmentTolerence", transSupplierHeader.ShipmentTolorence);
            para.Add("AccountGroupId", transSupplierHeader.AccountGroupId);
            //para.Add("LocationId", transSupplierHeader.LocationId);
            para.Add("UserId", transSupplierHeader.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrSupplierHeaderSave", para
                , commandType: CommandType.StoredProcedure);            
            return para.Get<int>("Result");
        }
        #endregion Supplier Header
        #region Supplier Currency
        public async Task<int> SaveSupplierCurrencyAsync(TransSupplierCurrency suppliercurrency)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("SuppCurId" , suppliercurrency.SuppCurId);
            para.Add("CurrencyId", suppliercurrency.CurrencyId);
            para.Add("SupplierId", suppliercurrency.SupplierId);
            para.Add("UserId", suppliercurrency.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMsterSupplierCurrencySave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
        public async Task<int> DeleteSupCurrencyAsync(TransSupplierCurrency supplierCurrency)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("SuppCurId" , supplierCurrency.SuppCurId);
            para.Add("CurrencyId", supplierCurrency.CurrencyId);
            para.Add("UserId", supplierCurrency.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrSuplierCurrencyDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
        #endregion Supplier Currency

        #region Supplier Type
        public async Task<int> SaveSupplierTypeAsync(MstrSupplierType suppliertype)
        {   
            DynamicParameters para = new DynamicParameters();

            para.Add ("SuppTypeId", suppliertype.SuppTypeId);
            para.Add ("Description", suppliertype.Description);
            para.Add("UserId", suppliertype.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
           
            var result = await DbConnection.ExecuteAsync("spMstrSupplierTypeSave", para ,
            commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }
        #endregion Supplier Type

         #region Account Type
        public async Task<int> SaveAccountTypeAsync(MstrAccountType accounttype)
        {   
            DynamicParameters para = new DynamicParameters();

            para.Add ("AccTypeId", accounttype.AccTypeId);
            para.Add ("Description", accounttype.Description);
            para.Add("UserId", accounttype.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
           
            var result = await DbConnection.ExecuteAsync("spMstrAccountTypeSave", para ,
            commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }
        #endregion Account Type

        #region GRN Type
        public async Task<int> SaveGRNTypeAsync(MstrGRNType grntype)
        {   
            DynamicParameters para = new DynamicParameters();

            para.Add ("GRNTypeId", grntype.GRNTypeId);
            para.Add ("Description", grntype.Description);
            para.Add("UserId", grntype.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
           
            var result = await DbConnection.ExecuteAsync("spMstrGRNTypeSave", para ,
            commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }
        #endregion GRN Type

        #region Shipment Mode
        public async Task<int> SaveShipmentModeAsync(MstrShipmentModes shipmentModes)
        {   
            DynamicParameters para = new DynamicParameters();

            para.Add("ShipModeId", shipmentModes.ShipModeId);
            para.Add("Code", shipmentModes.Code);
            para.Add("Description", shipmentModes.Description);
            para.Add("UserId", shipmentModes.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
           
            var result = await DbConnection.ExecuteAsync("spMstrShipmentModeSave", para ,
            commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }
        #endregion Shipment Mode

        #region Product

        public async Task<int> DeactProductAsync(MstrProducts MstrProducts)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("productId" , MstrProducts.productId);
            para.Add("bActive", MstrProducts.bActive);
            para.Add("UserId", MstrProducts.createUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrProductDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> SaveProductAsync(MstrProducts MstrProducts)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("productId" , MstrProducts.productId);
            para.Add("ProductName", MstrProducts.Description.Trim());
            para.Add("CompanyId", MstrProducts.CompanyId);
            para.Add("ModuleId", MstrProducts.ModuleId);
            para.Add("UserId", MstrProducts.createUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrProductSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
	    #endregion Product

       
        #region Master Forwarder
        public async Task<int> SaveForwarderAsync(MstrForwarder forwarder)
        {   
            DynamicParameters para = new DynamicParameters();

            para.Add("ForwarderId", forwarder.ForwarderId);
            para.Add("Name", forwarder.Name);
            para.Add("Contact", forwarder.Contact);
            para.Add("Email", forwarder.EmailId);
            para.Add("UserId", forwarder.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
           
            var result = await DbConnection.ExecuteAsync("spMsterForwarderSave", para ,
            commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }
        #endregion Master Forwarder

        #region Master PurchaseOrderType
        public async Task<int> SavePurchaseOrderTypeAsync(MstrPurchaseOrderType purchaseordertype)
        {   
            DynamicParameters para = new DynamicParameters();

            para.Add("POTypeId", purchaseordertype.POTypeId);
            para.Add("Details", purchaseordertype.Details);
            para.Add("UserId", purchaseordertype.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
           
            var result = await DbConnection.ExecuteAsync("spMstrPurchaseOrderSave", para ,
            commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }
        #endregion Master PurchaseOrderType

        #region Master DispatchReason
        public async Task<int> SaveDispatchReasonAsync(MstrDispatchReason dispatchreason)
        {   
            DynamicParameters para = new DynamicParameters();

            para.Add("DisReasonId", dispatchreason.DisReasonId);
            para.Add("Description", dispatchreason.Description);
            para.Add("UserId", dispatchreason.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
           
            var result = await DbConnection.ExecuteAsync("spMstrDispatchReasonSave", para ,
            commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }
        #endregion Master DispatchReason
        public async Task<int> DeactiveSupplierAsync(MstrSupplierHeader mstrSupplierHeader)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("SupplierId" , mstrSupplierHeader.SupplierId);
            para.Add("bActive", mstrSupplierHeader.bActive);
            para.Add("UserId", mstrSupplierHeader.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrSupplierDeactive", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
        #region Master SupplierAddressList
         public async Task<int> SaveSupplierAddressListAsync(TransSupplierAddresses supplieraddresses)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("SupplierAddId" , supplieraddresses.SuppAddId);
            para.Add("SupplierId" , supplieraddresses.SupplierId);
            para.Add("AddressTypeId" , supplieraddresses.AddressTypeId);
            para.Add("Address" , supplieraddresses.Address);
            para.Add("City" , supplieraddresses.City);
            para.Add("State" , supplieraddresses.State);
            para.Add("ZipPostalCode" , supplieraddresses.ZipPostalCode);
            para.Add("CountryId" , supplieraddresses.CountryId);
            para.Add("Tel" , supplieraddresses.Tel);
            para.Add("VATNo" , supplieraddresses.VATNo);
            para.Add("TAXNo" , supplieraddresses.TaxNo);
            para.Add("TINNo" , supplieraddresses.TinNo);
            para.Add("UserId", supplieraddresses.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spTransSupplierAddressListSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
        #endregion Master SupplierAddressList
        #region MasterBasis
        public async Task<int> SaveMasterBasisAsync(MstrBasis basis)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("BaseId" , basis.BaseId);
            para.Add("Description", basis.Description);
            para.Add("UserId", basis.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrBasisSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
        #endregion MasterBasis
        #region MasterAdditonalCharges
        public async Task<int> SaveMasterAdditonalChargesAsync(MstrAdditionalCharges addcharge)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AddChargeId" , addcharge.AddChargeId);
            para.Add("Description", addcharge.Description);
            para.Add("UserId", addcharge.CreateUserId);
            para.Add("ModuleId", addcharge.ModuleId);
            para.Add("LocationId", addcharge.LocationId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMsterAdditionalChargesSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
        #endregion MasterAdditonalCharges
        #region MasterAdditonalCharges
        public async Task<int> SaveMasterDeliveryTermAsync(MstrDeliveryTerms deliveryterms)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("DeliTermsId" , deliveryterms.DeliTermsId);
            para.Add("Code", deliveryterms.Description);
            para.Add("Description", deliveryterms.Description);
            para.Add("UserId", deliveryterms.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrDeliveryTermsSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
        #endregion MasterAdditonalCharges

        #region MasterAddCharge Basis To Module Map 
        public async Task<int> SaveMasterAddChargeBasisToModuleMapAsync(MstrAddChargeModule addCM)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , addCM.AutoId);
            para.Add("ModuleId", addCM.ModuleId);
            para.Add("Module", addCM.Module);
            para.Add("AddChargeId", addCM.AddChargeId);
            para.Add("AddChargeType", addCM.AddChargeType);
            //para.Add("PrToTotal", addCM.PrToTotal);
            //para.Add("Amount", addCM.Amount);
            para.Add("UserId", addCM.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrAddChargeBasisToModuleSave", para
            , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
        }

        #endregion MasterAddCharge Basis To Module Map

        #region Package Mapping
        public async Task<int> SavePackageMappingAsync(MstrMapping packmap)
        {
            DynamicParameters para = new DynamicParameters();

                para.Add("maptypeid" , packmap.MappingTypeId);
                para.Add("customerid" , packmap.CustomerId);
                para.Add("Refid" , packmap.RefId);
                para.Add("MapTo", packmap.MappedTo);
                para.Add("UserId", packmap.CreateUserId);
                para.Add("moduleId", packmap.modudeId);
                para.Add("locationId", packmap.locationId);
                para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMsterPackageMapSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
         #endregion Package Mapping

        public async Task<IEnumerable<PackmapDto>> GetMappedDtAsync(PackmapDto mapDt)
        {   IEnumerable<PackmapDto> PackgeMapdataList;
            DynamicParameters para = new DynamicParameters();

            para.Add("maptypeid" , mapDt.maptypeid);
            para.Add("ModuleId", mapDt.ModuleId);
            para.Add("LocationId", mapDt.LocationId);

              PackgeMapdataList = await DbConnection.QueryAsync<PackmapDto>("spMasterPackageMapGetDt", para
              , commandType: CommandType.StoredProcedure);

              return PackgeMapdataList;
        }

        #region MasterSpecialCategory
        public async Task<int> SaveMasterSpecialCategoryAsync(MstrSpecialCategory category)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , category.AutoId);
            para.Add("Code", category.Code);
            para.Add("Description", category.Description);
            para.Add("UserId", category.CreateUserId);
            para.Add("ModuleId", category.ModuleId);
            para.Add("LocationId", category.LocationId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrSpecialCategorySave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeactiveSpecialCategoryAsync(MstrSpecialCategory category)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("categoryId" , category.AutoId);
            para.Add("IsActive", category.IsActive);
            para.Add("UserId", category.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrSpecialCategoryDeactivation", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
        #endregion MasterSpecialCategory

         #region MasterSubCategory
        public async Task<int> SaveMasterSubCategoryAsync(MstrSubCategory category)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , category.AutoId);
            para.Add("Code", category.Code);
            para.Add("Description", category.Description);
            para.Add("UserId", category.CreateUserId);
            para.Add("ModuleId", category.ModuleId);
            para.Add("LocationId", category.LocationId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrSpecialSubCategorySave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> DeactiveSubCategoryAsync(MstrSubCategory subcategory)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("subCategoryId" , subcategory.AutoId);
            para.Add("IsActive", subcategory.IsActive);
            para.Add("UserId", subcategory.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrSubCategoryDeactivation", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }
        #endregion MasterSubCategory

        #region MasterProductGroupSubCategory
        public async Task<IEnumerable<ProdGroupSubCatDto>> GetGroupSubCatDtAsync(int groupId)
        {
            IEnumerable<ProdGroupSubCatDto> prodTypeList;
            DynamicParameters para = new DynamicParameters();

            para.Add("groupId" , groupId);

            prodTypeList = await DbConnection.QueryAsync<ProdGroupSubCatDto>("spMstrProdGroupSubCatGetDt" , para
                    , commandType: CommandType.StoredProcedure);
            
            return prodTypeList;
        }

        public async Task<int> AssignProdGroupSubCatAsync(List<MstrProdGroupSubCat> prod)
        {
            DynamicParameters para = new DynamicParameters();
            DataTable SubCategoryDT = new DataTable();

            SubCategoryDT.Columns.Add("UserId",typeof(int));
            SubCategoryDT.Columns.Add("ProdGroupId",typeof(int));
            SubCategoryDT.Columns.Add("SubCatId",typeof(int));
            SubCategoryDT.Columns.Add("ModuleId",typeof(int));
            SubCategoryDT.Columns.Add("LocationId",typeof(int));

            foreach (var item in prod)
            {
                SubCategoryDT.Rows.Add( item.CreateUserId
                        , item.ProdGroupId
                        , item.SubCatId
                        , item.ModuleId
                        , item.LocationId);
            } 

            para.Add("SubCategoryDT", SubCategoryDT.AsTableValuedParameter("SubCategoryList"));  
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);        

             var result = await DbConnection.ExecuteAsync("spMstrSubCatProdGropSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");     
        }

        public async Task<int> DeleteProdGroupSubCatAsync(List<MstrProdGroupSubCat> prod)
        {
            DynamicParameters para = new DynamicParameters();
            DataTable SubCategoryDT = new DataTable();

            SubCategoryDT.Columns.Add("UserId",typeof(int));
            SubCategoryDT.Columns.Add("ProdGroupId",typeof(int));
            SubCategoryDT.Columns.Add("SubCatId",typeof(int));
            SubCategoryDT.Columns.Add("ModuleId",typeof(int));
            SubCategoryDT.Columns.Add("LocationId",typeof(int));

            foreach (var item in prod)
            {
                SubCategoryDT.Rows.Add( item.CreateUserId
                        , item.ProdGroupId
                        , item.SubCatId
                        , item.ModuleId
                        , item.LocationId);
            } 

            para.Add("SubCategoryDT", SubCategoryDT.AsTableValuedParameter("SubCategoryList"));  
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);        

             var result = await DbConnection.ExecuteAsync("spMstrSubCatProdGropDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");     
        }
        #endregion MasterProductGroupSubCategory

        #region MasterProductSubCatCategory
        public async Task<IEnumerable<SubCatCategoryDto>> GetSubCatCategoryDtAsync(int subcatId)
        {
            IEnumerable<SubCatCategoryDto> prodTypeList;
            DynamicParameters para = new DynamicParameters();

            para.Add("subcatId" , subcatId);

            prodTypeList = await DbConnection.QueryAsync<SubCatCategoryDto>("spMstrProdSubCatCategoryGetDt" , para
                    , commandType: CommandType.StoredProcedure);
            
            return prodTypeList;
        }

        public async Task<int> AssignSubCatCategoryAsync(List<MstrProdSubCatCategory> prod)
        {
            DynamicParameters para = new DynamicParameters();
            DataTable CategoryDT = new DataTable();

            CategoryDT.Columns.Add("UserId",typeof(int));
            CategoryDT.Columns.Add("SubCatId",typeof(int));
            CategoryDT.Columns.Add("CatId",typeof(int));
            CategoryDT.Columns.Add("ModuleId",typeof(int));
            CategoryDT.Columns.Add("LocationId",typeof(int));

            foreach (var item in prod)
            {
                CategoryDT.Rows.Add( item.CreateUserId
                        , item.SubCatId
                        , item.CatId
                        , item.ModuleId
                        , item.LocationId);
            } 

            para.Add("CategoryDT", CategoryDT.AsTableValuedParameter("SpecialCategoryList"));  
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);        

             var result = await DbConnection.ExecuteAsync("spMstrCategorySubCategorySave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");     
        }

        public async Task<int> DeleteSubCatCategoryAsync(List<MstrProdSubCatCategory> prod)
        {
            DynamicParameters para = new DynamicParameters();
            DataTable CategoryDT = new DataTable();

            CategoryDT.Columns.Add("UserId",typeof(int));
            CategoryDT.Columns.Add("SubCatId",typeof(int));
            CategoryDT.Columns.Add("CatId",typeof(int));
            CategoryDT.Columns.Add("ModuleId",typeof(int));
            CategoryDT.Columns.Add("LocationId",typeof(int));

            foreach (var item in prod)
            {
                CategoryDT.Rows.Add( item.CreateUserId
                        , item.SubCatId
                        , item.CatId
                        , item.ModuleId
                        , item.LocationId);
            } 

            para.Add("CategoryDT", CategoryDT.AsTableValuedParameter("SpecialCategoryList"));  
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);        

             var result = await DbConnection.ExecuteAsync("spMstrSubCatCategoryDelete", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");     
        }
        #endregion MasterProductSubCatCategory

        #region Fixed Asset
        
        public async Task<int> SaveFAMainCategoryAsync(MstrMainCategory mstrMainCategory)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , mstrMainCategory.AutoId);
            para.Add("Code", mstrMainCategory.Code.Trim());
            para.Add("Name", mstrMainCategory.Name.Trim());
            para.Add("Companyid", mstrMainCategory.Companyid);
            para.Add("moduleid", mstrMainCategory.ModuleId);
            para.Add("UserId", mstrMainCategory.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spFAMainCategorySave", para
            , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> SaveFASubCategoryAsync(MstrFASubCategory mstrFASubCategory)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , mstrFASubCategory.AutoId);
            para.Add("Code", mstrFASubCategory.Code.Trim());
            para.Add("Name", mstrFASubCategory.Name.Trim());
            para.Add("Companyid", mstrFASubCategory.Companyid);
            para.Add("moduleid", mstrFASubCategory.ModuleId);
            para.Add("UserId", mstrFASubCategory.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            await DbConnection.ExecuteAsync("spFASubCategorySave", para
            , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<ColorAllocationDto>> GetFASubAllocDetailsAsync(int mainId)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("maincatId" , mainId);
           
            return await DbConnection.QueryAsync<ColorAllocationDto>("FA.spMstrSubGetAllocDetails" , para
                    , commandType: CommandType.StoredProcedure);            
        }

        public async Task<int> saveSubToMainCategoryAllocationAsync(List<MstrFASubToMainCategoryAll> SubtoMainAlloc)
        {
            DataTable SubToMainAllocDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            SubToMainAllocDT.Columns.Add("mainid",typeof(int));
            SubToMainAllocDT.Columns.Add("subid",typeof(int));
            SubToMainAllocDT.Columns.Add("brandid",typeof(int));
            SubToMainAllocDT.Columns.Add("modelid",typeof(int));
           

            foreach (var item in SubtoMainAlloc)
            {
                SubToMainAllocDT.Rows.Add( item.maincatid
                        , item.subcatid,0,0);
            }         

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
	        para.Add("Companyid", SubtoMainAlloc[0].Companyid);
            para.Add("moduleid", SubtoMainAlloc[0].ModuleId);
            para.Add("UserId", SubtoMainAlloc[0].CreateUserId);
            para.Add("SubToMainAllocDT", SubToMainAllocDT.AsTableValuedParameter("FASubtoMainAllocType"));

            await DbConnection.ExecuteAsync("FA.spMstrSubToMainAllocationSave", para , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
            
        }
	    public async Task<int> deleteSubToMainCategoryAllocationAsync(List<MstrFASubToMainCategoryAll> SubtoMainAlloc)
        {
            DataTable SubToMainAllocDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            SubToMainAllocDT.Columns.Add("mainid",typeof(int));
            SubToMainAllocDT.Columns.Add("subid",typeof(int));
            SubToMainAllocDT.Columns.Add("brandid",typeof(int));
            SubToMainAllocDT.Columns.Add("modelid",typeof(int));
           

            foreach (var item in SubtoMainAlloc)
            {
                SubToMainAllocDT.Rows.Add( item.maincatid
                        , item.subcatid,0,0);
            }         

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            para.Add("SubToMainAllocDT", SubToMainAllocDT.AsTableValuedParameter("FASubtoMainAllocType"));

            await DbConnection.ExecuteAsync("FA.spMstrSubToMainAllocationDelete", para , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
            
        }

        #endregion Fixed Asset


        public async Task<IEnumerable<BrandAllocationDto>> getBrandAllocDetailsAsync(int categoryId)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("categoryId" , categoryId);
           
            return await DbConnection.QueryAsync<BrandAllocationDto>("spMstrBrandAllocDetails" , para
                    , commandType: CommandType.StoredProcedure);            
        }

        public async Task<int> saveBrandAllocationAsync(List<MstrFASubToMainCategoryAll> brandAlloc)
        {
            DataTable brandAllocDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            brandAllocDT.Columns.Add("mainid",typeof(int));
            brandAllocDT.Columns.Add("subid",typeof(int));
            brandAllocDT.Columns.Add("brandid",typeof(int));
            brandAllocDT.Columns.Add("modelid",typeof(int));
           

            foreach (var item in brandAlloc)
            {
                brandAllocDT.Rows.Add( item.maincatid
                        , item.subcatid,0,0);
            }         

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
	        para.Add("Companyid", brandAlloc[0].Companyid);
            para.Add("moduleid", brandAlloc[0].ModuleId);
            para.Add("UserId", brandAlloc[0].CreateUserId);
            para.Add("SubToMainAllocDT", brandAllocDT.AsTableValuedParameter("FASubtoMainAllocType"));

            await DbConnection.ExecuteAsync("spMstrBrandToCategoryAllocationSave", para , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
            
        }

	    public async Task<int> ddeleteBrandToCategoryAllocationAsync(List<MstrFASubToMainCategoryAll> brandToCateall)
        {
            DataTable brandAllocDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            brandAllocDT.Columns.Add("mainid",typeof(int));
            brandAllocDT.Columns.Add("subid",typeof(int));
            brandAllocDT.Columns.Add("brandid",typeof(int));
            brandAllocDT.Columns.Add("modelid",typeof(int));
           

            foreach (var item in brandToCateall)
            {
                brandAllocDT.Rows.Add( item.maincatid
                        , item.subcatid,0,0);
            }         

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            para.Add("SubToMainAllocDT", brandAllocDT.AsTableValuedParameter("FASubtoMainAllocType"));

            await DbConnection.ExecuteAsync("spMstrBrandToCategoryAllocationDelete", para , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
            
        }

        #region Model

        public async Task<int> saveModelAsync(MstrModel MstrModel)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId" , MstrModel.AutoId);
            para.Add("Name", MstrModel.ModelName.Trim());
            para.Add("UserId", MstrModel.CreateUserId);
	        para.Add("ModuleId", MstrModel.ModuleId);
	        para.Add("Companyid", MstrModel.Companyid);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrModelSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }


        public async Task<IEnumerable<ModelAllocationDto>> GetModelAllocDetailsAsync(int brandId)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("brandId" , brandId);
           
            return await DbConnection.QueryAsync<ModelAllocationDto>("spMstrModelAllocDetails" , para
                    , commandType: CommandType.StoredProcedure);            
        }

        public async Task<int> saveModelAllocationAsync(List<MstrFASubToMainCategoryAll> modelAlloc)
        {
            DataTable modelAllocDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            modelAllocDT.Columns.Add("mainid",typeof(int));
            modelAllocDT.Columns.Add("subid",typeof(int));
            modelAllocDT.Columns.Add("brandid",typeof(int));
            modelAllocDT.Columns.Add("modelid",typeof(int));
           

            foreach (var item in modelAlloc)
            {
                modelAllocDT.Rows.Add( item.maincatid
                        , item.subcatid,0,0);
            }         

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
	        para.Add("Companyid", modelAlloc[0].Companyid);
            para.Add("moduleid", modelAlloc[0].ModuleId);
            para.Add("UserId", modelAlloc[0].CreateUserId);
            para.Add("SubToMainAllocDT", modelAllocDT.AsTableValuedParameter("FASubtoMainAllocType"));

            await DbConnection.ExecuteAsync("spMstrModelToBrandAllocationSave", para , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
            
        }

	    public async Task<int> deleteModelToBrandAllocationAsync(List<MstrFASubToMainCategoryAll> modelToBrandall)
        {
            DataTable brandAllocDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            brandAllocDT.Columns.Add("mainid",typeof(int));
            brandAllocDT.Columns.Add("subid",typeof(int));
            brandAllocDT.Columns.Add("brandid",typeof(int));
            brandAllocDT.Columns.Add("modelid",typeof(int));
           

            foreach (var item in modelToBrandall)
            {
                brandAllocDT.Rows.Add( item.maincatid
                        , item.subcatid,0,0);
            }         

            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            para.Add("SubToMainAllocDT", brandAllocDT.AsTableValuedParameter("FASubtoMainAllocType"));

            await DbConnection.ExecuteAsync("spMstrModelToBrandAllocationDelete", para , commandType: CommandType.StoredProcedure);

            return para.Get<int>("Result");
            
        }

        #endregion Model

        #region "Save Apperale Article"
        public async Task<ReturnDto> saveApperaleArticle(List<SaveArticleApperaleDetailDto> aaDt)
        {
           DataTable ArticleApperaleDT = new DataTable();

            DynamicParameters para = new DynamicParameters();

            ArticleApperaleDT.Columns.Add("F01", typeof(int));
            ArticleApperaleDT.Columns.Add("F02", typeof(int));
            ArticleApperaleDT.Columns.Add("F03", typeof(int));
            ArticleApperaleDT.Columns.Add("F04", typeof(int));
            ArticleApperaleDT.Columns.Add("F05", typeof(int));
            ArticleApperaleDT.Columns.Add("F06", typeof(int));
            ArticleApperaleDT.Columns.Add("F07", typeof(int));
            ArticleApperaleDT.Columns.Add("F08", typeof(int));     
            ArticleApperaleDT.Columns.Add("F09", typeof(int));
            ArticleApperaleDT.Columns.Add("F10", typeof(int));
            ArticleApperaleDT.Columns.Add("F11", typeof(int)); 
            ArticleApperaleDT.Columns.Add("F12", typeof(int));
            ArticleApperaleDT.Columns.Add("F13", typeof(int));
            ArticleApperaleDT.Columns.Add("F14", typeof(int));    
            ArticleApperaleDT.Columns.Add("F15", typeof(int));
            ArticleApperaleDT.Columns.Add("F16", typeof(int));
            ArticleApperaleDT.Columns.Add("F17", typeof(int));
            ArticleApperaleDT.Columns.Add("F18", typeof(int));     
            ArticleApperaleDT.Columns.Add("F19", typeof(int));   
            ArticleApperaleDT.Columns.Add("F20", typeof(int));   
            ArticleApperaleDT.Columns.Add("F21", typeof(int));   
            ArticleApperaleDT.Columns.Add("F22", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F23", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F24", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F25", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F26", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F27", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F28", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F29", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F30", typeof(string));   
            ArticleApperaleDT.Columns.Add("F31", typeof(string));   
            ArticleApperaleDT.Columns.Add("F32", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F33", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F34", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F35", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F36", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F37", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F38", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F39", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F40", typeof(string));   
            ArticleApperaleDT.Columns.Add("F41", typeof(string));   
            ArticleApperaleDT.Columns.Add("F42", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F43", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F44", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F45", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F46", typeof(string)); 
            ArticleApperaleDT.Columns.Add("F47", typeof(string)); 

           
              foreach (var item in aaDt)
            {
              var ActionId =item.ActionId;
              var SystemId = item.SystemId;
              var LocationId = item. LocationId;
              var AgentId = item. AgentId;
               
              if (item.sArticleMaster != null)
                {                  
                      ArticleApperaleDT.Rows.Add(
                        ActionId,
                        SystemId,
                        LocationId,
                        AgentId,
                        item.sArticleMaster.AutoId,//5
                        item.sArticleMaster.CategoryId,
                        item.sArticleMaster.ProTypeId,
                        item.sArticleMaster.ProGroupId,
                        item.sArticleMaster.SubCatId,
                        item.sArticleMaster.CatId,
                        item.sArticleMaster.MeasurementId,
                        item.sArticleMaster.StorageUnitId,
                        item.sArticleMaster.ColorCardId,
                        item.sArticleMaster.SizeCardId, 
                         0,//15
                         0,//16 
                         0,//17
                         0,//18
                         0,//19
                         0,//20
                         0,//21
                         item.sArticleMaster.ItemType, 
                        0,//23
                        0,//24
                        0,//25
                        0,//26
                        item.sArticleMaster.ImageName,//27
                        item.sArticleMaster.StockCode,
                        item.sArticleMaster.ArticleName,
                        item.sArticleMaster.Description1,
                        item.sArticleMaster.Description2,
                        0//32
                    );
                }
              if (item.sArticleDetails != null)
                {                  
                      ArticleApperaleDT.Rows.Add(
                        ActionId,
                        SystemId,
                        LocationId,
                        AgentId,
                        item.sArticleDetails.ArticleId,//5
                        0,//6
                        0,//7
                        0,//8
                        0,//9
                        0,//10
                        0,//11
                        0,//12
                        0,//13
                        0,//14
                        0, //15
                        item.sArticleDetails.CustomerHeaderId,//16 
                        item.sArticleDetails.BuyerId,//17
                        item.sArticleDetails.SeasonId,//18
                        item.sArticleDetails.GenderId,//19
                        item.sArticleDetails.FabricCategoryId,//20
                        item.sArticleDetails.WashTypeId,//21
                        item.sArticleDetails.FabComp,//22 
                        0,//23
                        0,//24
                        0,//25
                        0,//26
                        0,//27
                        0,//28
                        0,//29
                        0,//30
                        0,//31
                        0,//32
                        0,//33
                        0,//34
                        0,//35
                        0,//36
                        0,//37
                        0,//38
                        0,//39
                        0,//40
                        0,//41
                        0,//42   
                        0,//43     
                        item.sArticleDetails.CusWashRef,//44       
                        item.sArticleDetails.BuyerStyRef,//45
                        item.sArticleDetails.ShipStyleName,//46
                        item.sArticleDetails.MaterialDes //47            
                    );
                }
            
            }
            para.Add("UDT", ArticleApperaleDT.AsTableValuedParameter("UDT_ArticleData"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("sp_ArticleMasterData", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }
        #endregion "Save Apperale Article"
    
        #region "MWS Master"
       public async Task<IEnumerable<MWSMasterDto>> GetMWSMasterData(MWSMasterDto wsdt)
        {
            DataTable MWSMaterDT = new DataTable();
            IEnumerable<MWSMasterDto> MWSMaterList;
               
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
            MWSMaterDT.Columns.Add("F09", typeof(Int64));
            MWSMaterDT.Columns.Add("F10", typeof(Int64));
            MWSMaterDT.Columns.Add("F11", typeof(decimal)); 
            MWSMaterDT.Columns.Add("F12", typeof(decimal));  
            MWSMaterDT.Columns.Add("F13", typeof(decimal)); 
            MWSMaterDT.Columns.Add("F14", typeof(decimal));
            MWSMaterDT.Columns.Add("F15", typeof(decimal)); 
            MWSMaterDT.Columns.Add("F16", typeof(decimal));                               
            MWSMaterDT.Columns.Add("F17", typeof(decimal));
            MWSMaterDT.Columns.Add("F18", typeof(string));
            MWSMaterDT.Columns.Add("F19", typeof(string));   
            MWSMaterDT.Columns.Add("F20", typeof(string)); 
            MWSMaterDT.Columns.Add("F21", typeof(string));       
            MWSMaterDT.Columns.Add("F22", typeof(string));   
            MWSMaterDT.Columns.Add("F23", typeof(string)); 
            MWSMaterDT.Columns.Add("F24", typeof(string)); 
            MWSMaterDT.Columns.Add("F25", typeof(string)); 
            MWSMaterDT.Columns.Add("F26", typeof(string)); 
            MWSMaterDT.Columns.Add("F27", typeof(string));                                                                                              
            MWSMaterDT.Columns.Add("F28", typeof(DateTime));
            MWSMaterDT.Columns.Add("F29", typeof(DateTime));
            MWSMaterDT.Columns.Add("F30", typeof(DateTime));
     
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
                        wsdt.F25,
                        wsdt.F26,
                        wsdt.F27,
                        wsdt.F28,
                        wsdt.F29,
                        wsdt.F30
                        
              );
                          
            para.Add("UDT", MWSMaterDT.AsTableValuedParameter("udt_MasterData"));

            MWSMaterList = await DbConnection.QueryAsync<MWSMasterDto>("sp_MasterData", para
                , commandType: CommandType.StoredProcedure);

            return MWSMaterList;
        }
      public async Task<ReturnDto> SaveMWSMasterData(List<SaveMWSMasterDto> wsDt)
        {
           DataTable TMDT = new DataTable();
            DynamicParameters para = new DynamicParameters();

            TMDT.Columns.Add("ActivityNo", typeof(Int64));
            TMDT.Columns.Add("ModuleNo", typeof(Int64));
            TMDT.Columns.Add("CompanyNo", typeof(Int64));
            TMDT.Columns.Add("LocationNo", typeof(Int64));
            TMDT.Columns.Add("AgentNo", typeof(Int64));
            TMDT.Columns.Add("bActive", typeof(Int64));
            TMDT.Columns.Add("F01", typeof(Int64));
            TMDT.Columns.Add("F02", typeof(Int64));
            TMDT.Columns.Add("F03", typeof(Int64));
            TMDT.Columns.Add("F04", typeof(Int64));
            TMDT.Columns.Add("F05", typeof(Int64));
            TMDT.Columns.Add("F06", typeof(Int64));
            TMDT.Columns.Add("F07", typeof(Int64));
            TMDT.Columns.Add("F08", typeof(Int64));     
            TMDT.Columns.Add("F09", typeof(Int64));
            TMDT.Columns.Add("F10", typeof(Int64));
            TMDT.Columns.Add("F11", typeof(decimal)); 
            TMDT.Columns.Add("F12", typeof(decimal));  
            TMDT.Columns.Add("F13", typeof(decimal)); 
            TMDT.Columns.Add("F14", typeof(decimal));
            TMDT.Columns.Add("F15", typeof(decimal)); 
            TMDT.Columns.Add("F16", typeof(decimal));                               
            TMDT.Columns.Add("F17", typeof(decimal));
            TMDT.Columns.Add("F18", typeof(string));
            TMDT.Columns.Add("F19", typeof(string));   
            TMDT.Columns.Add("F20", typeof(string)); 
            TMDT.Columns.Add("F21", typeof(string));       
            TMDT.Columns.Add("F22", typeof(string));   
            TMDT.Columns.Add("F23", typeof(string)); 
            TMDT.Columns.Add("F24", typeof(string)); 
            TMDT.Columns.Add("F25", typeof(string)); 
            TMDT.Columns.Add("F26", typeof(string)); 
            TMDT.Columns.Add("F27", typeof(string));                                                                                              
            TMDT.Columns.Add("F28", typeof(DateTime));
            TMDT.Columns.Add("F29", typeof(DateTime));
            TMDT.Columns.Add("F30", typeof(DateTime));

           
              foreach (var item in wsDt)
            {
              var ActivityNo =item.ActivityNo;
              var ModuleNo = item.ModuleNo;
              var CompanyNo = item.CompanyNo;
              var LocationNo = item.LocationNo;
              var AgentNo = item.AgentNo;
              var bActive = item.bActive;
               
              if (item.sCategory != null)
                {                  
                      TMDT.Rows.Add(
                        ActivityNo,
                        ModuleNo,
                        CompanyNo,
                        LocationNo,
                        AgentNo,
                        bActive,
                        item.sCategory.AutoId,//1
                        item.sCategory.ModuleId,//2
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
                        0,//15
                        0,//16,
                        0,//17,
                        item.sCategory.Code,//18,
                        item.sCategory.Description,//19
                         item.sCategory.bActive,//20
                        0,//21
                        0
                    );
                }   
                

              
            }
    
              para.Add("UDT", TMDT.AsTableValuedParameter("udt_MasterData"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("sp_MasterData", para
                , commandType: CommandType.StoredProcedure);

            return result;

            
            
        }
        #endregion "MWS Master"
    
    }
}