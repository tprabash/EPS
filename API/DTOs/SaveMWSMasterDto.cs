using System.Collections.Generic;
using API.Entities;


namespace API.DTOs
{
    public class SaveMWSMasterDto
    {
        public int Action { get; set; }
        public int FactoryId { get; set; }
        public int AgentId { get; set; }
        public int ModuleId { get; set; }
        public virtual   MstrMerchandizer  sMerchandizer  { get; set; }  
        public virtual   MstrBuyingOfficeMaster  sBuyingOfficeMaster  { get; set; }  
        public virtual   MstrDyeType sDyeType { get; set; }
        public virtual MstrFabComp sFabComp { get; set; }
        public virtual MstrGender sGender  { get; set; }
        public virtual MstrGMTType sGMTType   { get; set; }
        public virtual MstrMachineMaster  sMachineMaster    { get; set; }
        public virtual MstrMachineTypeToProcess  sMachineAllocation     { get; set; }
        public virtual MstrMachTypes sMachtypes { get; set; }
        public virtual MstrProcessType sProcesstype { get; set; }        
        public virtual MstrReceipeType sReceipetype  { get; set; }
        public virtual MstrRecipeUtl sRecipeUtl   { get; set; }
        public virtual MstrSampleIssue sSampleIssue   { get; set; }
        public virtual MstrSampleType sSampletype    { get; set; }
        public virtual MstrSpeOprType sSpeoprtype     { get; set; }
        public virtual MstrSubSalesCategory sSubSalesCategory  { get; set; }
        public virtual MstrUseBy sUseby  { get; set; }
        public virtual MstrWashStandard sWashstd   { get; set; }       
        public virtual MstrGatepassWashCat sGatepassWashCat    { get; set; }
        public virtual MstrWashType sWashtypes    { get; set; }
        public virtual MstrProcesstoChemical sProcesstoChemical  { get; set; }        
    }
}