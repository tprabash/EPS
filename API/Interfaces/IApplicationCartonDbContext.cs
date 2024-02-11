using System.Data;
using System.Threading;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Interfaces
{
    //
    public interface IApplicationCartonDbContext
    {       
        IDbConnection Connection { get; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<MstrLocation> MstrLocation {get; set;}        
        DbSet<MstrMenuList> MstrMenuList { get; set; }
        DbSet<MstrMenuLevel> MstrMenuLevel { get; set; }
        DbSet<ErrorLog> ErrorLog { get; set; }
        DbSet<MenuJoinList> MenuJoinList { get; set; }
        DbSet<MstrMenuUser> MstrMenuUser{ get; set; }
        DbSet<UserMenuList> UserMenuList {get; set;}
        DbSet<MstrColorCard> MstrColorCard {get; set;}
        DbSet<MstrSizeCard> MstrSizeCard {get; set;}
        DbSet<MstrColor> MstrColor {get; set;}
        DbSet<MstrSize> MstrSize {get; set;}
        DbSet<MstrUserLocation> MstrUserLocation { get;  set; }
        DbSet<MstrCompany> MstrCompany { get;  set; }
        DbSet<MstrArticle> MstrArticle { get;  set; }
        DbSet<MstrArticleColor> MstrArticleColor { get;  set; }
        DbSet<MstrArticleColorSize> MstrArticleColorSize { get; set; }
        DbSet<MstrArticleSize> MstrArticleSize { get;  set; }
        DbSet<MstrCustomerHeader> MstrCustomerHeader { get;  set; }
        DbSet<MstrCustomerLocation> MstrCustomerLocation { get;  set; }
        DbSet<MstrUnits> MstrUnits { get;  set ; }       
        DbSet<MstrStoreSite> MstrStoreSite { get;  set ; }
        DbSet<MstrProcess> MstrProcess { get;  set ; }
        DbSet<MstrBrand> MstrBrand { get;  set ; }
        DbSet<MstrBrandCode> MstrBrandCode { get;  set ; }
        DbSet<MstrMaterialType> MstrMaterialType { get;  set ; }
        DbSet<MstrCategory> MstrCategory { get;  set ; }
        DbSet<TransCostingHeader> TransCostingHeader { get;  set ; }
        DbSet<MstrCombination> MstrCombination { get;  set ; }
        DbSet<MstrCustomerUsers> MstrCustomerUsers { get;  set ; }
        DbSet<MstrSalesCategory> MstrSalesCategory { get;  set ; }
        DbSet<MstrCustomerCurrency> MstrCustomerCurrency { get;  set ; }
        DbSet<MstrCurrency> MstrCurrency { get;  set ; }        
        DbSet<MstrCountries> MstrCountries { get;  set ; }
        DbSet<MstrPaymentTerm> MstrPaymentTerm { get;  set ; }
        DbSet<MstrProductType> MstrProductType { get;  set ; }
        DbSet<MstrProductGroup> MstrProductGroup { get;  set ; }
        DbSet<MstrProductSubCat> MstrProductSubCat { get;  set ; }
        DbSet<MstrProductionDefinitionDt> MstrProdDefinitionDt { get;  set ; }
        DbSet<MstrProductionDefinitionHd> MstrProdDefinitionHd { get;  set ; }
        DbSet<MstrSalesAgent> MstrSalesAgent { get;  set ; }
        DbSet<MstrCustomerDivision> MstrCustomerDivision { get;  set ; }
        DbSet<MstrCustomerBrand> MstrCustomerBrand { get;  set ; }
        DbSet<MstrAddressType> MstrAddressType { get;  set ; }
        DbSet<MstrCustomerAddressList> MstrCustomerAddressList { get;  set ; }
        DbSet<MstrStatus> MstrStatus { get;  set ; }
        DbSet<TransFtyProductionOrder> TransFtyProductionOrder { get;  set ; }
        DbSet<TransFtyProductionOrderDt> TransFtyProductionOrderDt { get;  set ; }
        DbSet<TransFtyProductionProcessOrder> TransFtyProdProcessOrder { get;  set ; }        
        DbSet<TransFtyProductionProcessOrderDt> TransFtyProdProcessOrderDt { get;  set ; }
        DbSet<MstrCostingGroup> MstrCostingGroup { get;  set ; }
        DbSet<TransSequenceSettings> TransSequenceSettings { get;  set ; }
        DbSet<MstrFlexFieldDetails> MstrFlexFieldDetails { get;  set ; }
        DbSet<MstrFlexFieldValueList> MstrFlexFieldValueList { get;  set ; }
        DbSet<MstrCatProductType> MstrCatProductType { get;  set ; }
        DbSet<MstrProdTypeGroup> MstrProdTypeGroup { get;  set ; }
        DbSet<MstrCodeDefinition> MstrCodeDefinition {get; set;}
        DbSet<MstrRejectionReasons> MstrRejeReasons {get; set;}
        DbSet<TransDispatchHeader> TransDispatchHeader {get; set;}
        DbSet<TransDispatchDetails> TransDispatchDetails {get; set;}
        DbSet<MstrDispatchSite> MstrDispatchSite {get; set;}
        DbSet<MstrUnitConversion> UnitConversion {get; set;}
        DbSet<MstrFluteTypes> MstrFluteTypes {get; set;}        
        DbSet<MstrSpecialInstruction> MstrSpecialInstruction {get; set;}
        DbSet<MstrArticleUOMConversion> MstrArticleUOMConversion {get; set;}
        DbSet<TransSalesOrderHd> TransSalesOrderHeader { get; set;}
        DbSet<TransSalesOrderItemDt> TransSalesOrderItemDt { get; set;}
        DbSet<TransExchangeRate> TransExchangeRate { get; set;}
        DbSet<MstrTax> MstrTax { get; set;}
        DbSet<MstrBank> MstrBank { get; set;}
        DbSet<TransApprovalCenter> TransApprovalCenter { get; set;}
        DbSet<TransJobHeader> TransJobHeader { get; set;}
        DbSet<TransJobDetail> TransJobDetail { get; set;}
        DbSet<MstrCustomerType> MstrCustomerType { get; set;}
        DbSet<MstrInvoiceType> MstrInvoiceType { get; set;}
        DbSet<FileDetails> FileDetails { get; set;}
        DbSet<TransSalesOrderFileUpload> FileUpload { get; set; }
        DbSet<MstrPaymentMode> MstrPaymentMode { get; set; }
        DbSet<MstrReceiptType> MstrReceiptType { get; set; }
        DbSet<MstrReport> MstrReport { get; set; }
        DbSet<MstrCartonType> MstrCartonType {get; set;}
        DbSet<MstrArticleBrandcode> MstrArticleBrandcode { get;  set; }
        DbSet<MstrCustomerOtherCode> MstrCustomerOtherCode{get;set;}
        DbSet<MstrCustomerOther> MstrCustomerOther{get;set;}
        DbSet<MstrReason> MstrReason { get;  set ; }
        //DbSet<MstrUserMasterSettings> MstrUserMasterSettings{ get;  set ; }
        DbSet<UserReportList> UserReportList { get;  set ; }
        DbSet<MstrUserSite> MstrUserSite { get; set; }
        DbSet<MstrUserMasterSetting> MstrUserMasterSetting { get; set; }
        DbSet<TransMRHeader> TransMRHeader { get; set; }
        DbSet<TransMRDetails> TransMRDetails { get; set; }
        DbSet<MstrAccountType> MstrAccountType { get; set; }
        DbSet<MstrAdditionalCharges> MstrAddionalCharges { get; set; }
        DbSet<MstrDispatchReason> MstrDispatchReason { get; set; }
        DbSet<MstrShipmentModes> MstrShipmentModes { get; set; }
        DbSet<MstrPorts> MstrPorts { get; set; }
        DbSet<MstrSupplierType> MstrSupplierType { get; set; }
        DbSet<MstrPurchaseOrderType> MstrPurchaseOrderType { get; set; }
        DbSet<MstrForwarder> MstrForwarder { get; set; }
        DbSet<MstrDeliveryTerms> MstrDeliveryTerms { get; set; }
        DbSet<TransSupplierHeader> TransSupplierHeader { get; set; }
        DbSet<TransSupplierAddresses> TransSupplierAddresses { get; set; }
        DbSet<TransSupplierCurrency> TransSupplierCurrency { get; set; }
        DbSet<MstrGRNType> MstrGRNType { get; set; }
        DbSet<TransIndentDetails> TransIndentDetails { get; set; }
        DbSet<MstrProducts> MstrProducts { get;  set ; }
        DbSet<MstrEnumValues> MstrEnumValues { get; set; }
        DbSet<MstrBasis> MstrBasis { get; set; }
        DbSet<MstrAddChargeModule> MstrAddChargeModule { get; set; }
        DbSet<MstrSpecialCategory> MstrSpecialCategory { get; set; }
        DbSet<MstrSubCategory> MstrSubCategory { get; set; }
        DbSet<MstrMainCategory> MstrMainCategory { get; set; }
        DbSet<MstrFASubCategory> MstrFASubCategory { get; set; }
        DbSet<MstrStockAdjuestmentReason> MstrStockAdjuestmentReason { get; set; }
        DbSet<MstrModel> MstrModel { get;  set ; }
        DbSet<TransFtyProductionProcessOrderDt> TransFtyProductionProcessOrderDt { get;  set ; }

        DbSet<MstrSeason> MstrSeason { get; set; }
        DbSet<MstrGender> MstrGender { get; set; }
        DbSet<MstrFabricCom> MstrFabricCom { get; set; }
        DbSet<MstrFabricCategory> MstrFabricCategory { get; set; }
        DbSet<MstrWashType> MstrWashType { get; set; }

    }
}