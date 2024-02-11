using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IFinanceRepository
    {
        Task<int> SaveExchageRateAsync(TransExchangeRate exchangeRate);
        Task<int> SaveTaxAsync(MstrTax tax);
        Task<int> SaveBankAsync(MstrBank bank);
        Task<IEnumerable<PendInvoiceDto>> GetInvoicePendDtAsync(int CustomerId);
        Task<ReturnDto> SaveInvoiceAsync(SavedInvoiceDto invoiceDto);
        Task<InvoiceDto> GetInvoiceDetailsAsync(string invoiceNo);
        Task<int> ApproveInvoiceAsync(TransInvoiceHeader invoiceHd);
        Task<IEnumerable<InvoiceNoListDto>> GetInvoiceListAsync(string customerRef);
        Task<IEnumerable<ReceiptPendInvoiceDto>> GetReceiptPendInvAsync(int customerId);
        Task<ReturnDto> SaveReceiptAsync(SavedReceiptDto receiptDto);
        Task<IEnumerable<ReceiptNoListDto>> GetReceiptListAsync(ReceiptSearchDto receipt);
        Task<int> CancelReceiptAsync(TransReceiptHeader receiptHd);
        Task<IEnumerable<InvoiceNoListDto>> GetInvoiceNoFilterAsync(string RefNo);
        //Task<IEnumerable<GetBankDto>> GetBankOnCustomerAsync(int customerId);
        Task<int> CancelInvoiceAsync(TransInvoiceHeader InvoiceHd);
        Task<IEnumerable<ReceiptNoListDto>> GetReceiptAllocationPendingListAsync(int customerId);
        Task<IEnumerable<CreditNoteDispatchDto>> GetDispatchDetailsAsync(long invoiceId);
        Task<IEnumerable<CreditNoteListDto>> GetCreditnoteListAsync(int customerId);
        Task<CreditNoteDto> GetCreditNoteDetailsAsync(string creditNoteNo);
        Task<ReturnDto> SaveCreditNoteAsync(SavedCreditNoteDto creditNoteDto);
        Task<IEnumerable<CreditNoteAllListDto>> GetCreditNoteAllocationPendingListAsync(int customerId);
        Task<ReturnDto> SaveReceiptAllAsync(SavedReceiptDto receiptDto);
        Task<ReturnDto> SaveCreditNoteAllAsync(SavedCreditNoteDto creditNoteDto);
        Task<ReturnDto> SaveDebitNoteAsync(SavedDebitNoteDto debitnoteDto);
        Task<IEnumerable<DebitNoteListDto>> GetDebitNoteAsync(int customerId);
        Task<ReceiptDto> GetReceiptDetailsAsync(string receiptNo);
        Task<DebitNoteDto> GetDebitNoteDetailsAsync(int debitNoteHdId);
        Task<int> CancelCreditNoteAsync(TransCreditNoteHeader creditnote);
        Task<int> CancelDebitNoteAsync(TransDebitNoteHeader debitnote);
        Task<int> ReceiptAutoAllocateAsync(TransReceiptHeader receiptHd);

    }
}