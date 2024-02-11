using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.DTOs.ptrack;

using API.Entities.Ptrack;

using API.Entities.Admin;

namespace API.Interfaces
{
    public interface IPTrackMasterRepository
    {
        Task<IEnumerable<ReportListDto>> GetReportMenuListAsync();
        //  Task<PurchaseOrdersDto> GetPurchaseOrderXMLAsync();
        Task<PurchaseOrdersDto> GetPurchaseOrdersAsync();
        Task<IEnumerable<PermitMenuDto>> GetAuthMenuListAsyncPtrack(UserDto userDto);
        Task<ReturnDto> WorkStudySavedata(List<SaveWorkStudyDto> wsDt);   
        Task<IEnumerable<WorkStudyDto>> GetWorkStudyData(WorkStudyDto wsDt);
        Task<IEnumerable<CPDDto>> GetCPDData(CPDDto wsDt);
        Task<ReturnDto> SaveCPDData(List<SaveWorkStudyDto> wsDt);  
        Task<IEnumerable<TransportDto>> GetTransportData(TransportDto wsdt);
        Task<ReturnDto> SaveTransportData(List<SaveTransportDto> wsDt);
        Task<IEnumerable<PaymentInvoiceDto>> GetPaymentInvoiceData(PaymentInvoiceDto pidt);
        Task<ReturnDto> SavePaymentInvoiceData(List<SavePaymentInvoiceDto> piDt);
        Task<IEnumerable<FROrderDto>> getFROrdersAsync(DateTime myDate);
        Task<IEnumerable<FRwfxRtnDto>> GetFRWFXDetailsAsync(FRwfxDto frDt);
        Task<ReturnDto> saveFRAssocationOrderAsync(SaveFROrderDto frPODto);
        Task<IEnumerable<FRwfxRtnDto>> getFRNewOrdersAsync(string myOrder);
        Task<IEnumerable<dashBoardTwoDto>> GetDashBoardTwoDataListAsync(dashBoardTwoSearchDto dashboardDt);
        Task<IEnumerable<EmployeeRemainingOpsDto>> GetEmployeeRemainingAsync(int employeeId);
        Task<IEnumerable<EmpOperationDtDto>> GetEmployeeOpDtAsync(int employeeId);
        Task<int> SaveEmployeeOperationAsync(TransOperationsAndSectionsAssignToEmp operation);
        Task<int> DeactiveOperationEmpAsync(TransOperationsAndSectionsAssignToEmp operation);
        Task<IEnumerable<MstrEmployee>> GetEmployeDtAsync(string location);
        Task<IEnumerable<FtyDepartmentDto>> GetFactoryWiseDepartmentsAsync(int locationId);
        Task<IEnumerable<SectionDetailsDto>> GetDepartmentWiseSectionAsync(SectionDetailsDto section);
        Task<IEnumerable<SubSectionDto>> GetSectionWiseSubSectionAsync(SubSectionDto subsection);
        Task<IEnumerable<SectionLineDto>> GetSubSectionWiseLineAsync(SectionLineDto line);
        Task<IEnumerable<LostTimeReasonDto>> GetLostTimeReasonAsync(int moduleId);
        Task<IEnumerable<LostTimeDto>> GetLostTimeAsync(LostTimeSearchDto lost);
        Task<int> SaveLostTimeAsync(LostTimeSaveDto lostS);
    }
}