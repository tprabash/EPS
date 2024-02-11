using System.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationCartonDbContext : DbContext, IApplicationCartonDbContext
    {
        public ApplicationCartonDbContext(DbContextOptions<ApplicationCartonDbContext> options) : base(options)
        {
        }

        public IDbConnection Connection => throw new System.NotImplementedException();

        public DbSet<MstrLocation> MstrLocation { get; set; }
        public DbSet<MstrMenuList> MstrMenuList { get; set; }
        public DbSet<MstrMenuLevel> MstrMenuLevel { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }
        public DbSet<MenuJoinList> MenuJoinList { get; set; }
        public DbSet<MstrMenuUser> MstrMenuUser { get; set; }
        public DbSet<UserMenuList> UserMenuList { get; set; }
        public DbSet<MstrColorCard> MstrColorCard { get; set; }
        public DbSet<MstrSizeCard> MstrSizeCard { get; set; }
        public DbSet<MstrColor> MstrColor { get; set; }
        public DbSet<MstrSize> MstrSize { get; set; }
        public DbSet<MstrUserLocation> MstrUserLocation { get; set; }
        public DbSet<MstrCompany> MstrCompany { get; set; }
        public DbSet<MstrArticle> MstrArticle { get; set; }
        public DbSet<MstrArticleColor> MstrArticleColor { get; set; }
        public DbSet<MstrArticleColorSize> MstrArticleColorSize { get; set; }
        public DbSet<MstrArticleSize> MstrArticleSize { get; set; }
        public DbSet<MstrCustomerHeader> MstrCustomerHeader { get; set; }
        public DbSet<MstrCustomerLocation> MstrCustomerLocation { get; set; }
        public DbSet<MstrUnits> MstrUnits { get; set; }
        public DbSet<MstrStoreSite> MstrStoreSite { get; set; }
        public DbSet<MstrProcess> MstrProcess { get; set; }
        public DbSet<MstrBrand> MstrBrand { get; set; }
        public DbSet<MstrBrandCode> MstrBrandCode { get; set; }
        public DbSet<MstrMaterialType> MstrMaterialType { get; set; }
        public DbSet<MstrCategory> MstrCategory { get; set; }
        public DbSet<TransCostingHeader> TransCostingHeader { get; set; }
        public DbSet<MstrCombination> MstrCombination { get; set; }
        public DbSet<MstrCustomerUsers> MstrCustomerUsers { get; set; }
        public DbSet<MstrSalesCategory> MstrSalesCategory { get; set; }
        public DbSet<MstrCustomerCurrency> MstrCustomerCurrency { get; set; }
        public DbSet<MstrCurrency> MstrCurrency { get; set; }
        public DbSet<MstrCountries> MstrCountries { get; set; }
        public DbSet<MstrPaymentTerm> MstrPaymentTerm { get; set; }
        public DbSet<MstrProductType> MstrProductType { get; set; }
        public DbSet<MstrProductGroup> MstrProductGroup { get; set; }
        public DbSet<MstrProductSubCat> MstrProductSubCat { get; set; }
        public DbSet<MstrProductionDefinitionDt> MstrProdDefinitionDt { get; set; }
        public DbSet<MstrProductionDefinitionHd> MstrProdDefinitionHd { get; set; }
        public DbSet<MstrSalesAgent> MstrSalesAgent { get; set; }
        public DbSet<MstrCustomerDivision> MstrCustomerDivision { get; set; }
        public DbSet<MstrCustomerBrand> MstrCustomerBrand { get; set; }
        public DbSet<MstrAddressType> MstrAddressType { get; set; }
        public DbSet<MstrCustomerAddressList> MstrCustomerAddressList { get; set; }
        public DbSet<MstrStatus> MstrStatus { get; set; }
        public DbSet<TransFtyProductionOrder> TransFtyProductionOrder { get; set; }
        public DbSet<TransFtyProductionOrderDt> TransFtyProductionOrderDt { get; set; }
        public DbSet<TransFtyProductionProcessOrder> TransFtyProdProcessOrder { get; set; }
        public DbSet<TransFtyProductionProcessOrderDt> TransFtyProdProcessOrderDt { get; set; }
        public DbSet<MstrCostingGroup> MstrCostingGroup { get; set; }
        public DbSet<TransSequenceSettings> TransSequenceSettings { get; set; }
        public DbSet<MstrFlexFieldDetails> MstrFlexFieldDetails { get; set; }
        public DbSet<MstrFlexFieldValueList> MstrFlexFieldValueList { get; set; }
        public DbSet<MstrCatProductType> MstrCatProductType { get; set; }
        public DbSet<MstrProdTypeGroup> MstrProdTypeGroup { get; set; }
        public DbSet<MstrCodeDefinition> MstrCodeDefinition { get; set; }
        public DbSet<MstrRejectionReasons> MstrRejeReasons { get; set; }
        public DbSet<TransDispatchHeader> TransDispatchHeader { get; set; }
        public DbSet<TransDispatchDetails> TransDispatchDetails { get; set; }
        public DbSet<MstrDispatchSite> MstrDispatchSite { get; set; }
        public DbSet<MstrUnitConversion> UnitConversion { get; set; }
        public DbSet<MstrFluteTypes> MstrFluteTypes { get; set; }
        public DbSet<MstrSpecialInstruction> MstrSpecialInstruction { get; set; }
        public DbSet<MstrArticleUOMConversion> MstrArticleUOMConversion { get; set; }
        public DbSet<TransSalesOrderHd> TransSalesOrderHeader { get; set; }
        public DbSet<TransSalesOrderItemDt> TransSalesOrderItemDt { get; set; }
        public DbSet<TransExchangeRate> TransExchangeRate { get; set; }
        public DbSet<MstrTax> MstrTax { get; set; }
        public DbSet<MstrBank> MstrBank { get; set; }
        public DbSet<TransApprovalCenter> TransApprovalCenter { get; set; }
        public DbSet<TransJobHeader> TransJobHeader { get; set; }
        public DbSet<TransJobDetail> TransJobDetail { get; set; }
        public DbSet<MstrCustomerType> MstrCustomerType { get; set; }
        public DbSet<MstrInvoiceType> MstrInvoiceType { get; set; }
        public DbSet<FileDetails> FileDetails { get; set; }
        public DbSet<TransSalesOrderFileUpload> FileUpload { get; set; }
        public DbSet<MstrPaymentMode> MstrPaymentMode { get; set; }
        public DbSet<MstrReceiptType> MstrReceiptType { get; set; }
        public DbSet<MstrReport> MstrReport { get; set; }
        public DbSet<MstrCartonType> MstrCartonType {get; set;}
        public DbSet<MstrArticleBrandcode> MstrArticleBrandcode {get; set;}
        public DbSet<MstrCustomerOtherCode> MstrCustomerOtherCode{get; set;}
        public DbSet<MstrCustomerOther> MstrCustomerOther{get; set;}
        public DbSet<MstrReason> MstrReason { get;  set ; }
        //public DbSet<MstrUserMasterSettings> MstrUserMasterSettings{ get;  set ; }
        public DbSet<UserReportList> UserReportList {get; set;}
        public DbSet<MstrUserSite> MstrUserSite { get; set; }
        public DbSet<MstrUserMasterSetting> MstrUserMasterSetting { get; set; }
        public DbSet<TransMRHeader> TransMRHeader { get; set; }
        public DbSet<TransMRDetails> TransMRDetails { get; set; }
        public DbSet<MstrAccountType> MstrAccountType { get; set; }
        public DbSet<MstrAdditionalCharges> MstrAddionalCharges { get; set; }
        public DbSet<MstrDispatchReason> MstrDispatchReason { get; set; }
        public DbSet<MstrShipmentModes> MstrShipmentModes { get; set; }
        public DbSet<MstrPorts> MstrPorts { get; set; }
        public DbSet<MstrSupplierType> MstrSupplierType { get; set; }
        public DbSet<MstrPurchaseOrderType> MstrPurchaseOrderType { get; set; }
        public DbSet<MstrForwarder> MstrForwarder { get; set; }
        public DbSet<MstrDeliveryTerms> MstrDeliveryTerms { get; set; }
        public DbSet<TransSupplierHeader> TransSupplierHeader { get; set; }
        public DbSet<TransSupplierAddresses> TransSupplierAddresses { get; set; }
        public DbSet<TransSupplierCurrency> TransSupplierCurrency { get; set; }
        public DbSet<MstrGRNType> MstrGRNType { get; set; }
        public DbSet<TransIndentDetails> TransIndentDetails { get; set; }
        public DbSet<MstrProducts> MstrProducts { get; set; }
        public DbSet<MstrEnumValues> MstrEnumValues { get; set; }
        public DbSet<MstrBasis> MstrBasis { get; set; }
        public DbSet<MstrAddChargeModule> MstrAddChargeModule { get; set; }
        public DbSet<MstrMapping> MstrMapping { get; set; }
        public DbSet<MstrSpecialCategory> MstrSpecialCategory { get; set; }
        public DbSet<MstrSubCategory> MstrSubCategory { get; set; }
        public DbSet<MstrMainCategory> MstrMainCategory { get; set; }
        public DbSet<MstrFASubCategory> MstrFASubCategory { get; set; }
        public DbSet<MstrStockAdjuestmentReason> MstrStockAdjuestmentReason { get; set; }
        public DbSet<MstrModel> MstrModel { get; set; }
        public DbSet<TransFtyProductionProcessOrderDt> TransFtyProductionProcessOrderDt { get; set; }

        public DbSet<MstrSeason> MstrSeason { get; set; }
        public DbSet<MstrGender> MstrGender { get; set; }
        public DbSet<MstrFabricCom> MstrFabricCom { get; set; }
        public DbSet<MstrFabricCategory> MstrFabricCategory { get; set; }
        public DbSet<MstrWashType> MstrWashType { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuJoinList>().HasNoKey();
            modelBuilder.Entity<UserMenuList>().HasNoKey();
            modelBuilder.Entity<UserReportList>().HasNoKey();
            modelBuilder.Entity<MstrColorCard>(a => {
                a.HasKey(a => a.AutoId);
                a.Property(e => e.AutoId).HasColumnType("tinyint");
            });
            modelBuilder.Entity<MstrSizeCard>(a => {
                a.HasKey(a => a.AutoId);
                a.Property(e => e.AutoId).HasColumnType("tinyint");
            });
            modelBuilder.Entity<MstrCompany>(a => {
                a.HasKey(a => a.AutoId);
                a.Property(e => e.AutoId).HasColumnType("tinyint");
            });
            modelBuilder.Entity<MstrCustomerLocation>()
                .Ignore(c => c.MstrCustomerHeader );
            modelBuilder.Entity<MstrCustomerHeader>()
                .Ignore(c => c.Location)
                .Ignore(c => c.MstrCurrency)
                .Ignore(c => c.MstrCountries);
            modelBuilder.Entity<MstrBrand>()
                .Ignore(c => c.UserLocation);
            // modelBuilder.Entity<MstrColor>(a => {
            //     a.HasKey(a => a.LinkColorCard);                
            //     a.Property(e => e.LinkColorCard).HasColumnType("tinyint");
            // });
            
        }

    }
}