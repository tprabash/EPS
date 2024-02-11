using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.CCSystem.Transaction
{
    //
    public class FinanceController : BaseApiController
    {
        private readonly IApplicationCartonDbContext _context;
        private readonly IFinanceRepository _financeRepository;
        public FinanceController(IApplicationCartonDbContext context, IFinanceRepository financeRepository)
        {
            _financeRepository = financeRepository;
            _context = context;
        }

        [HttpPost("SaveExR")]
        public async Task<IActionResult> SaveExchangeRate(TransExchangeRate exchangeRate) 
        {
            var result = await _financeRepository.SaveExchageRateAsync(exchangeRate);
            return Ok(result);
        }

        [HttpGet("ExRate")]
        public async Task<IActionResult> GetExchangeRate() 
        {
            var result = await _context.TransExchangeRate
                .Join(_context.MstrCurrency , e => e.CurrencyFId , f => f.AutoId , (e,f) => new  {e,f} )
                .Join(_context.MstrCurrency , a => a.e.CurrencyTId , t => t.AutoId , (a,t) =>
                    new {
                    autoId = a.e.AutoId,
                    currencyFId = a.e.CurrencyFId,
                    currencyTId = a.e.CurrencyTId,
                    validFrom = a.e.ValidFrom,
                    validTo = a.e.ValidTo,
                    rate = a.e.Rate,
                    currencyFrom = a.f.Code,
                    currencyTo = t.Code
                    }).OrderByDescending(s => s.validFrom)    
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("Rate")]
        public async Task<IActionResult> GetRate() 
        {
            var result = await _context.MstrTax
                    .Select(x => new { x.AutoId , x.Description , x.Rate , x.RateOnInvoice })
                    .ToListAsync();
            return Ok(result);
        }

        [HttpPost("SaveTax")]
        public async Task<IActionResult> SaveTax(MstrTax tax) 
        {
            var result = await _financeRepository.SaveTaxAsync(tax);
            return Ok(result);
        }
        [HttpGet("Bank")]
        public async Task<IActionResult> GetBank() 
        {
            var result = await _context.MstrBank
                    .Join(_context.MstrCurrency , b => b.CurrencyId , c => c.AutoId ,
                    (b, c) => new {
                        autoId = b.AutoId,
                        name = b.Name,
                        accountNo = b.AccountNo,
                        branch = b.Branch,
                        nextChequeNo = b.NextChequeNo,
                        currencyId = b.CurrencyId,
                        // Symbol = c.Symbol,
                        currency = c.Code
                    })               
                    .ToListAsync();

            return Ok(result);
        }
        
        [HttpGet("Banks/{customerId}")]
        public async Task<IActionResult> GetBank(int customerId) 
        {
            var result = await _context.MstrCustomerHeader
                    .Where(x => x.AutoId == customerId)
                    .Join(_context.MstrCurrency , b => b.CurrencyId , c => c.AutoId,
                     (b,c) => new {b,c})
                     .Join (_context.MstrBank , m => m.c.AutoId , d => d.CurrencyId,
                     (m,d) => new {
                        autoId = d.AutoId,
                        name = d.Name,
                        accountNo = d.AccountNo,
                        branch = d.Branch,
                        nextChequeNo = d.NextChequeNo,
                        currencyId = m.c.AutoId,
                        currency = m.c.Code
                     })               
                    .ToListAsync();
            return Ok(result);
        }

        /*[HttpGet("Banks/{customerId}")]
        public async Task<IActionResult> GetBankOnCustomer(int customerId) 
        {
            var result = await _financeRepository.GetBankOnCustomerAsync(customerId);
            return Ok(result);
        }*/

        [HttpPost("SaveBank")]
        public async Task<IActionResult> SaveBank(MstrBank bank) 
        {
            var result = await _financeRepository.SaveBankAsync(bank);
            return Ok(result);
        }

        [HttpGet("PInvoice/{customerId}")]
        public async Task<IActionResult> GetInvoicePendDt(int customerId) 
        {
            var result = await _financeRepository.GetInvoicePendDtAsync(customerId);
            return Ok(result);
        }

        [HttpPost("SaveInvoice")]
        public async Task<IActionResult> SaveInvoice(SavedInvoiceDto invoiceDto) 
        {
            var result = await _financeRepository.SaveInvoiceAsync(invoiceDto);
            return Ok(result);
        }

        [HttpGet("GetInvDt/{invoiceNo}")]
        public async Task<IActionResult> GetInvoiceDetails(string invoiceNo) 
        {
            var result = await _financeRepository.GetInvoiceDetailsAsync(invoiceNo);
            return Ok(result);
        }

        [HttpGet("InvList/{cusRef}")]
        public async Task<IActionResult> GetInvoiceList(string cusRef) 
        {
            var result = await _financeRepository.GetInvoiceListAsync(cusRef);
            return Ok(result);
        }

        [HttpGet("Invfilter/{invoiceNo}")]
        public async Task<IActionResult> GetInvoiceNoFilter(string invoiceNo) 
        {
            var result = await _financeRepository.GetInvoiceNoFilterAsync(invoiceNo);
            return Ok(result);
        }

        [HttpGet("RecePInv/{CustomerId}")]
        public async Task<IActionResult> GetInvoiceList(int CustomerId) 
        {
            var result = await _financeRepository.GetReceiptPendInvAsync(CustomerId);
            return Ok(result);
        }

        [HttpPost("SaveReceipt")]
        public async Task<IActionResult> SaveReceipt(SavedReceiptDto receiptDto) 
        {
            var result = await _financeRepository.SaveReceiptAsync(receiptDto);
            return Ok(result);
        }

        [HttpPost("AutoAllocate")]
        public async Task<IActionResult> ReceiptAutoAllocate(TransReceiptHeader receiptHd) 
        {
            var result = await _financeRepository.ReceiptAutoAllocateAsync(receiptHd);
            return Ok(result);
        }

        [HttpPost("RecList")]
        public async Task<IActionResult> GetReceiptList(ReceiptSearchDto receipt) 
        {
            var result = await _financeRepository.GetReceiptListAsync(receipt);
            return Ok(result);
        }

        [HttpGet("RecDt/{receiptNo}")]
        public async Task<IActionResult> GetReceiptDetails(string receiptNo) 
        {
            var result = await _financeRepository.GetReceiptDetailsAsync(receiptNo);
            return Ok(result);
        }

        [HttpPost("CanReceipt")]
        public async Task<IActionResult> CancelReceipt(TransReceiptHeader receiptHd) 
        {
            var result = await _financeRepository.CancelReceiptAsync(receiptHd);
            return Ok(result);
        }

        [HttpPost("cancelCredit")]
        public async Task<IActionResult> CancelCreditNote(TransCreditNoteHeader creditnote) 
        {
            var result = await _financeRepository.CancelCreditNoteAsync(creditnote);
            return Ok(result);
        }
        
        [HttpPost("CancelInv")]
        public async Task<IActionResult> CancelInvoice(TransInvoiceHeader InvoiceHd)
        {
            var result = await _financeRepository.CancelInvoiceAsync(InvoiceHd);
            return Ok(result);
        }

        [HttpGet("RecAllPenList/{CustomerId}")]
        public async Task<IActionResult> GetReceiptAllocationPendingList(int CustomerId) 
        {
            var result = await _financeRepository.GetReceiptAllocationPendingListAsync(CustomerId);
            return Ok(result);
        }
        [HttpGet("CRDispatch/{invoiceId}")]
        public async Task<IActionResult> GetDispatchDetails(long invoiceId) 
        {
            var result = await _financeRepository.GetDispatchDetailsAsync(invoiceId);
            return Ok(result);
        }
        [HttpPost("SaveCrNt")]
        public async Task<IActionResult> SaveCreditNote(SavedCreditNoteDto creditNote)
        {
            var result = await _financeRepository.SaveCreditNoteAsync(creditNote);
            return Ok(result);
        }
        [HttpGet("CrNoteList/{CustomerId}")]
        public async Task<IActionResult> GetCreditnoteList(int CustomerId) 
        {
            var result = await _financeRepository.GetCreditnoteListAsync(CustomerId);
            return Ok(result);
        }
        [HttpGet("CRNoteDt/{creditNoteNo}")]
        public async Task<IActionResult> GetCreditNoteDetails(string creditNoteNo) 
        {
            var result = await _financeRepository.GetCreditNoteDetailsAsync(creditNoteNo);
            return Ok(result);
        }
        [HttpGet("CreditAllPenList/{CustomerId}")]
        public async Task<IActionResult> GetCreditNoteAllocationPendingList(int CustomerId) 
        {
            var result = await _financeRepository.GetCreditNoteAllocationPendingListAsync(CustomerId);
            return Ok(result);
        }
        [HttpPost("SaveReceiptAll")]
        public async Task<IActionResult> SaveReceiptAll(SavedReceiptDto receiptDto) 
        {
            var result = await _financeRepository.SaveReceiptAllAsync(receiptDto);
            return Ok(result);
        }
        [HttpPost("SaveCreditNAll")]
        public async Task<IActionResult> SaveCreditNoteAll(SavedCreditNoteDto creditNoteDto) 
        {
            var result = await _financeRepository.SaveCreditNoteAllAsync(creditNoteDto);
            return Ok(result);
        }
        [HttpPost("SaveDebitNote")]
        public async Task<IActionResult> SaveDebitNote(SavedDebitNoteDto debitnoteDto) 
        {
            var result = await _financeRepository.SaveDebitNoteAsync(debitnoteDto);
            return Ok(result);
        }
        [HttpGet("DebitList/{CustomerId}")]
        public async Task<IActionResult> GetDebitNote(int CustomerId) 
        {
            var result = await _financeRepository.GetDebitNoteAsync(CustomerId);
            return Ok(result);
        }
        [HttpGet("DebitDetails/{DebitNoteHdId}")]
        public async Task<IActionResult> GetDebitNoteDetails(int DebitNoteHdId) 
        {
            var result = await _financeRepository.GetDebitNoteDetailsAsync(DebitNoteHdId);
            return Ok(result);
        }
        
        [HttpPost("cancelDebit")]
        public async Task<IActionResult> CancelDebitNote(TransDebitNoteHeader debitnote) 
        {
            var result = await _financeRepository.CancelDebitNoteAsync(debitnote);
            return Ok(result);
        } 

  

      

    }
}