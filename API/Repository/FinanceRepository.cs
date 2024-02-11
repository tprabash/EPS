using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Dapper;

namespace API.Repository
{
    public class FinanceRepository : DbConnCartonRepositoryBase, IFinanceRepository
    {
        // private readonly IApplicationCartonDbContext _context;

        public FinanceRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        //public FinanceRepository(IApplicationCartonDbContext context, IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        //{
        //    _context = context;
        //}

        public async Task<int> SaveExchageRateAsync(TransExchangeRate exchangeRate)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId", exchangeRate.AutoId);
            para.Add("CurrencyFId", exchangeRate.CurrencyFId);
            para.Add("CurrencyTId", exchangeRate.CurrencyTId);
            para.Add("Rate", exchangeRate.Rate);
            para.Add("ValidFrom", exchangeRate.ValidFrom);
            para.Add("ValidTo", exchangeRate.ValidTo);
            para.Add("UserId", exchangeRate.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spTransExchangeRateSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> SaveTaxAsync(MstrTax tax)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId", tax.AutoId);
            para.Add("Description", tax.Description);
            para.Add("Rate", tax.Rate);
            para.Add("RateOnInvoice", tax.RateOnInvoice);
            para.Add("UserId", tax.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrTaxSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<int> SaveBankAsync(MstrBank bank)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("AutoId", bank.AutoId);
            para.Add("Name", bank.Name);
            para.Add("Branch", bank.Branch);
            para.Add("AccountNo", bank.AccountNo);
            para.Add("CurrencyId", bank.CurrencyId);
            para.Add("NextChequeNo", bank.NextChequeNo);
            para.Add("UserId", bank.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spMstrBankSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<PendInvoiceDto>> GetInvoicePendDtAsync(int CustomerId)
        {
            IEnumerable<PendInvoiceDto> pendDetails;
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerId" , CustomerId);  

            pendDetails = await DbConnection.QueryAsync<PendInvoiceDto>("spTransInvoiceGetPendDt" , para
                    , commandType: CommandType.StoredProcedure);
            return pendDetails;
        }

        public async Task<ReturnDto> SaveInvoiceAsync(SavedInvoiceDto invoiceDto)
        {
            DataTable InvoiceDT = new DataTable();
            DataTable AddChargeDT = new DataTable();

            DynamicParameters para = new DynamicParameters();          

            ///////////----------- INVOICE HEADER ------------------------
            para.Add("AutoId", invoiceDto.InvoiceHeader.AutoId);
            para.Add("InvoiveNo", invoiceDto.InvoiceHeader.InvoiceNo);
            para.Add("LocationId", invoiceDto.InvoiceHeader.LocationId);
            para.Add("CustomerId", invoiceDto.InvoiceHeader.CustomerId);
            para.Add("PaymentDue", invoiceDto.InvoiceHeader.PaymentDueDate);
            para.Add("CustomerAddId", invoiceDto.InvoiceHeader.CustomerAddId);
            para.Add("InvCurrencyId", invoiceDto.InvoiceHeader.InvCurrencyId);
            para.Add("BaseCurrencyId", invoiceDto.InvoiceHeader.BaseCurrencyId);
            para.Add("ExchangeRate", invoiceDto.InvoiceHeader.ExchangeRate);
            para.Add("Attention", invoiceDto.InvoiceHeader.Attention);
            para.Add("TaxNo", invoiceDto.InvoiceHeader.TaxNo);
            para.Add("VatNo", invoiceDto.InvoiceHeader.VatNo);
            para.Add("TotalAmount", invoiceDto.InvoiceHeader.TotalAmount);
            para.Add("TaxAmount", invoiceDto.InvoiceHeader.TaxAmount);
            para.Add("GrossAmount", invoiceDto.InvoiceHeader.GrossAmount);
            para.Add("DiscountAmount", invoiceDto.InvoiceHeader.DiscountAmount);
            para.Add("NBTRate", invoiceDto.InvoiceHeader.NBTRate);
            para.Add("NBTAmount", invoiceDto.InvoiceHeader.NBTAmount);
            para.Add("NetAmount", invoiceDto.InvoiceHeader.NetAmount);
            para.Add("NetValue", invoiceDto.InvoiceHeader.NetValue);
            para.Add("TaxValue", invoiceDto.InvoiceHeader.TaxValue);
            para.Add("UserId", invoiceDto.InvoiceHeader.CreateUserId);
            para.Add("BankCharge", invoiceDto.InvoiceHeader.BankCharge);

            /////// ---------- INVOICE DETAILS ------------------
            InvoiceDT.Columns.Add("DispatchDtId", typeof(long));
            InvoiceDT.Columns.Add("SOItemDtId", typeof(long));
            InvoiceDT.Columns.Add("Qty", typeof(int));
            InvoiceDT.Columns.Add("UOM", typeof(int));
            InvoiceDT.Columns.Add("UnitPrice", typeof(decimal));
            InvoiceDT.Columns.Add("Value", typeof(decimal));
            
            /////// ---------- INVOICE ADDITIONAL CHARGES ------------------
            AddChargeDT.Columns.Add("AddChargeId", typeof(int));
            AddChargeDT.Columns.Add("BasisId", typeof(int));
            AddChargeDT.Columns.Add("InvoiceHdId", typeof(long));
            AddChargeDT.Columns.Add("Value", typeof(decimal));
            AddChargeDT.Columns.Add("Description", typeof(string));
            AddChargeDT.Columns.Add("TaxId", typeof(int));

            foreach (var item in invoiceDto.InvoiceDetails)
            {
                InvoiceDT.Rows.Add(item.DispatchDtId, 
                                item.SOItemDtId ,
                                item.Qty,
                                item.UOM,
                                item.UnitPrice,
                                item.Value);
            }

            foreach (var item in invoiceDto.InvoiceCharges)
            {
                AddChargeDT.Rows.Add(item.AddChargeId,
                                item.BasisId,
                                item.InvoiceHdId,
                                item.Value,
                                item.Description,
                                item.TaxId);    
            }  
           
            para.Add("InvoiceDT", InvoiceDT.AsTableValuedParameter("InvoiceDTType"));
            para.Add("AddChargeDT", AddChargeDT.AsTableValuedParameter("InvoiceAddChargeType"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransInvoiceSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<InvoiceDto> GetInvoiceDetailsAsync(string invoiceNo)
        {
            InvoiceDto invoice = new InvoiceDto();
            DynamicParameters para = new DynamicParameters();

            para.Add("InvoiceNo", invoiceNo);

            using (var multi = await DbConnection.QueryMultipleAsync("spTransInvoiceGetDt", para, commandType: CommandType.StoredProcedure))
            {
                invoice.InvoiceHeader = multi.Read<TransInvoiceHeader>();
                invoice.InvoiceDetails = multi.Read<InvoiceDetailsDto>();
                invoice.InvoiceCharges = multi.Read<InvoiceChargesDto>();
            }
            return invoice;
        }

        public async Task<int> ApproveInvoiceAsync(TransInvoiceHeader invoiceHd)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("InvoiceHdId", invoiceHd.AutoId);
            para.Add("UserId", invoiceHd.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spTransInvoiceApprove", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<InvoiceNoListDto>> GetInvoiceListAsync(string RefNo)
        {
            IEnumerable<InvoiceNoListDto> invoiceList;
            DynamicParameters para = new DynamicParameters();

            para.Add("RefNo" , RefNo);
            para.Add("Action" , "CR");

            invoiceList = await DbConnection.QueryAsync<InvoiceNoListDto>("spTransInvoiceFilter" , para
                    , commandType: CommandType.StoredProcedure);
            return invoiceList;
        }

        public async Task<IEnumerable<InvoiceNoListDto>> GetInvoiceNoFilterAsync(string RefNo)
        {
            IEnumerable<InvoiceNoListDto> invoiceList;
            DynamicParameters para = new DynamicParameters();

            para.Add("RefNo" , RefNo);
            para.Add("Action" , "IN");

            invoiceList = await DbConnection.QueryAsync<InvoiceNoListDto>("spTransInvoiceFilter" , para
                    , commandType: CommandType.StoredProcedure);
            return invoiceList;
        }

        public async Task<IEnumerable<ReceiptPendInvoiceDto>> GetReceiptPendInvAsync(int customerId)
        {
            IEnumerable<ReceiptPendInvoiceDto> pendInvList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerId" , customerId);

            pendInvList = await DbConnection.QueryAsync<ReceiptPendInvoiceDto>("spTransReceiptGetPendInvoice" , para
                    , commandType: CommandType.StoredProcedure);
            return pendInvList;
        }

        public async Task<ReturnDto> SaveReceiptAsync(SavedReceiptDto receiptDto)
        {
            DataTable ReceiptDT = new DataTable();
            DynamicParameters para = new DynamicParameters();          

            ///////////----------- RECEIPT HEADER ------------------------
            para.Add("ReceiptHdId", receiptDto.ReceiptHeader.AutoId);
            para.Add("ReceiptNo", receiptDto.ReceiptHeader.ReceiptNo);
            para.Add("Reference", receiptDto.ReceiptHeader.Reference);
            para.Add("CustomerId", receiptDto.ReceiptHeader.CustomerId);
            para.Add("ExchangeRate", receiptDto.ReceiptHeader.ExchangeRate);
            para.Add("BankId", receiptDto.ReceiptHeader.BankId);
            para.Add("DefaultCurrencyId", receiptDto.ReceiptHeader.DefaultCurrencyId);
            para.Add("PaymodeId", receiptDto.ReceiptHeader.PaymodeId);
            para.Add("ReceiptTypeId", receiptDto.ReceiptHeader.ReceiptTypeId);
            para.Add("ReceiptTotal", receiptDto.ReceiptHeader.ReceiptTotal);
            para.Add("AllocatedTotal", receiptDto.ReceiptHeader.AllocatedTotal);
            para.Add("UserId", receiptDto.ReceiptHeader.CreateUserId);
            para.Add("CustomerBankId", receiptDto.ReceiptHeader.CustomerBankId);

            /////// ---------- RECEIPT DETAILS ------------------
            ReceiptDT.Columns.Add("InvoiceHdId", typeof(long));
            ReceiptDT.Columns.Add("DepositAmount", typeof(decimal));
            ReceiptDT.Columns.Add("ExchangeRate", typeof(decimal));
            ReceiptDT.Columns.Add("ForeignDeposit", typeof(decimal));
            ReceiptDT.Columns.Add("OutStandingAmt", typeof(decimal));
            ReceiptDT.Columns.Add("ForeignOustand", typeof(decimal));
           
            foreach (var item in receiptDto.ReceiptDetails)
            {
                ReceiptDT.Rows.Add(item.InvoiceHdId, 
                                item.DepositAmount ,
                                item.ExchangeRate,
                                item.ForeignDeposit,
                                item.OutstandAmount,
                                item.ForeignOutstand);
            }   
           
            para.Add("ReceiptDT", ReceiptDT.AsTableValuedParameter("ReceiptDTType"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransReceiptSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<ReceiptNoListDto>> GetReceiptListAsync(ReceiptSearchDto receipt)
        {
            IEnumerable<ReceiptNoListDto> receiptList;
            DynamicParameters para = new DynamicParameters();

            para.Add("Reference" , receipt.Reference);
            para.Add("Action" , receipt.Action);

            receiptList = await DbConnection.QueryAsync<ReceiptNoListDto>("spTransReceiptGetListDuplicate" , para
                    , commandType: CommandType.StoredProcedure);
            return receiptList;
        }

        public async Task<ReceiptDto> GetReceiptDetailsAsync(string receiptNo)
        {
            ReceiptDto receipt = new ReceiptDto();
            DynamicParameters para = new DynamicParameters();

            para.Add("ReceiptNo", receiptNo);

            using (var multi = await DbConnection.QueryMultipleAsync("spTransReceiptGetDt", para, commandType: CommandType.StoredProcedure))
            {
                receipt.ReceiptHeader = multi.Read<TransReceiptHeader>();
                receipt.ReceiptDetails = multi.Read<ReceiptDetailDto>();
            }
            return receipt;
        }

        public async Task<int> CancelReceiptAsync(TransReceiptHeader receiptHd)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("ReceiptHdId", receiptHd.AutoId);
            para.Add("UserId", receiptHd.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spTransReceiptCancel", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }

         public async Task<int> CancelCreditNoteAsync(TransCreditNoteHeader creditnote)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("CreditNoteHdId" , creditnote.AutoId);  
            para.Add("UserId" , creditnote.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spTransCreditNoteCancel" , para
                    , commandType: CommandType.StoredProcedure);
                    
            return para.Get<int>("Result");
        }

        public async Task<int> CancelInvoiceAsync(TransInvoiceHeader InvoiceHd)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("InvoiceHeaderid" , InvoiceHd.AutoId);  
            para.Add("CancelUserId" , InvoiceHd.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spTransInvoiceCancel" , para
                    , commandType: CommandType.StoredProcedure);
                    
            return para.Get<int>("Result");
        }

        public async Task<IEnumerable<ReceiptNoListDto>> GetReceiptAllocationPendingListAsync(int customerId)
        {
            IEnumerable<ReceiptNoListDto> receiptList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerId" , customerId);

            receiptList = await DbConnection.QueryAsync<ReceiptNoListDto>("spTransReceiptPendingAllocationList" , para
                    , commandType: CommandType.StoredProcedure);
            return receiptList;
        }
        
        
        public async Task<IEnumerable<CreditNoteDispatchDto>> GetDispatchDetailsAsync(long invoiceId)
        {
            IEnumerable<CreditNoteDispatchDto> dispatchList;
            DynamicParameters para = new DynamicParameters();

            para.Add("InvoiceID" , invoiceId);

            dispatchList = await DbConnection.QueryAsync<CreditNoteDispatchDto>("spTransCRDispatchGetList" , para
                    , commandType: CommandType.StoredProcedure);
            return dispatchList;
        }

        public async Task<IEnumerable<CreditNoteListDto>> GetCreditnoteListAsync(int customerId)
        {
            IEnumerable<CreditNoteListDto> creditnoteList;
            DynamicParameters para = new DynamicParameters();

            para.Add("Customerid" , customerId);

            creditnoteList = await DbConnection.QueryAsync<CreditNoteListDto>("spTransCreditNoteListByCustomer" , para
                    , commandType: CommandType.StoredProcedure);
            return creditnoteList;
        }

        public async Task<CreditNoteDto> GetCreditNoteDetailsAsync(string creditNoteNo)
        {
            CreditNoteDto creditnote = new CreditNoteDto();
            DynamicParameters para = new DynamicParameters();

            para.Add("CreditNoteNo", creditNoteNo);

            using (var multi = await DbConnection.QueryMultipleAsync("spTransCreditnoteGetDt", para, commandType:           CommandType.StoredProcedure))
            {
                creditnote.CreditNoteHeader = multi.Read<TransCreditNoteHeader>();
                creditnote.CreditNoteDetails = multi.Read<CreditNoteDetailDto>();
            }
            return creditnote;
        }

        public async Task<ReturnDto> SaveCreditNoteAsync(SavedCreditNoteDto creditNoteDto)
        {
            DataTable creditNoteDT = new DataTable();
            DynamicParameters para = new DynamicParameters();          

            ///////////----------- CREDIT NOTE HEADER ------------------------
            para.Add("CreditNoteHdId", creditNoteDto.CreditNoteHeader.AutoId);
            para.Add("CreditNoteNo", creditNoteDto.CreditNoteHeader.ReferenceNo);
            para.Add("CustomerId", creditNoteDto.CreditNoteHeader.CustomerId);
            para.Add("ExchangeRate", creditNoteDto.CreditNoteHeader.ExchangeRate);
            para.Add("NoteTypeId", creditNoteDto.CreditNoteHeader.noteTypeId);
            para.Add("CurrencyId", creditNoteDto.CreditNoteHeader.idCurrency);
            para.Add("idReason", creditNoteDto.CreditNoteHeader.idReason);
            para.Add("Remark", creditNoteDto.CreditNoteHeader.Remark);
            //para.Add("Reference", creditNoteDto.CreditNoteHeader.Reference);
            para.Add("ModuleId",creditNoteDto.CreditNoteHeader.ModuleId);
            para.Add("CompanyId",creditNoteDto.CreditNoteHeader.CompanyId);
            para.Add("UserId", creditNoteDto.CreditNoteHeader.CreateUserId);

            /////// ---------- CREDIT NOTE DETAILS ------------------
            creditNoteDT.Columns.Add("AutoId", typeof(long));
            creditNoteDT.Columns.Add("InvoiceHdId", typeof(long));
            creditNoteDT.Columns.Add("DispatchNo", typeof(string));
            creditNoteDT.Columns.Add("ExchangeRate", typeof(decimal));
            creditNoteDT.Columns.Add("qty", typeof(int));
            creditNoteDT.Columns.Add("amount", typeof(decimal));
            creditNoteDT.Columns.Add("allocatedamount", typeof(decimal));
            
           
            foreach (var item in creditNoteDto.CreditNoteDetails)
            {
                creditNoteDT.Rows.Add(item.AutoId, 
                                item.InvoiceHdId ,
                                item.DispatchNo,
                                item.ExchangeRate,
                                item.qty,
                                item.amount,
                                item.allocatedamount);
            }   
           
            para.Add("CreditNoteDT", creditNoteDT.AsTableValuedParameter("CreditNoteDTType"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransCreditNoteSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<CreditNoteAllListDto>> GetCreditNoteAllocationPendingListAsync(int customerId)
        {
            IEnumerable<CreditNoteAllListDto> cNList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerId" , customerId);

            cNList = await DbConnection.QueryAsync<CreditNoteAllListDto>("spTransCreditNotePendingAllocationList" , para
                    , commandType: CommandType.StoredProcedure);
            return cNList;
        }
        
        public async Task<ReturnDto> SaveReceiptAllAsync(SavedReceiptDto receiptDto)
        {
            DataTable ReceiptDT = new DataTable();
            DynamicParameters para = new DynamicParameters();          

            ///////////----------- RECEIPT HEADER ------------------------
            para.Add("ReceiptHdId", receiptDto.ReceiptHeader.AutoId);
            para.Add("AllocatedTotal", receiptDto.ReceiptHeader.AllocatedTotal);
            para.Add("UserId", receiptDto.ReceiptHeader.CreateUserId);

            /////// ---------- RECEIPT DETAILS ------------------
            ReceiptDT.Columns.Add("InvoiceHdId", typeof(long));
            ReceiptDT.Columns.Add("DepositAmount", typeof(decimal));
            ReceiptDT.Columns.Add("ExchangeRate", typeof(decimal));
            ReceiptDT.Columns.Add("ForeignDeposit", typeof(decimal));
            ReceiptDT.Columns.Add("OutStandingAmt", typeof(decimal));
            ReceiptDT.Columns.Add("ForeignOustand", typeof(decimal));
           
            foreach (var item in receiptDto.ReceiptDetails)
            {
                ReceiptDT.Rows.Add(item.InvoiceHdId, 
                                item.DepositAmount ,
                                item.ExchangeRate,
                                item.ForeignDeposit,
                                item.OutstandAmount,
                                item.ForeignOutstand);
            }   
           
            para.Add("ReceiptDT", ReceiptDT.AsTableValuedParameter("ReceiptDTType"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransReceiptAllSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }
        
        public async Task<ReturnDto> SaveCreditNoteAllAsync(SavedCreditNoteDto creditNoteDto)
        {
            DataTable CreditNoteDT = new DataTable();
            DynamicParameters para = new DynamicParameters();          

            ///////////----------- CREDITNOTE HEADER ------------------------
            para.Add("CreditNoteHdId", creditNoteDto.CreditNoteHeader.AutoId);
            para.Add("AllocatedTotal", creditNoteDto.CreditNoteHeader.AllocatedTotal);
            para.Add("UserId", creditNoteDto.CreditNoteHeader.CreateUserId);

            /////// ---------- CREDITNOTE DETAILS ------------------
            CreditNoteDT.Columns.Add("InvoiceHdId", typeof(long));
            CreditNoteDT.Columns.Add("Outstand", typeof(decimal));
            CreditNoteDT.Columns.Add("ForeignOutstand", typeof(decimal));
            CreditNoteDT.Columns.Add("amount", typeof(decimal));
           
            foreach (var item in creditNoteDto.CreditNoteDetails)
            {
                CreditNoteDT.Rows.Add(item.InvoiceHdId,
                                item.Outstand,
                                item.ForeignOutstand, 
                                item.amount);
            }   
            para.Add("CreditNoteDT", CreditNoteDT.AsTableValuedParameter("CreditNoteAllDTType"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransCreditNoteAllocationSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<ReturnDto> SaveDebitNoteAsync(SavedDebitNoteDto debitnoteDto)
        {
            DataTable DebitNoteDT = new DataTable();
            DynamicParameters para = new DynamicParameters();          

            ///////////----------- DEBITNOTE HEADER ------------------------
            para.Add("idDebitNoteHd", debitnoteDto.DebitNoteHeader.idDebitNoteHd);
            para.Add("DebitNoteNumber", debitnoteDto.DebitNoteHeader.ReferenceNo);
            para.Add("CustomerId", debitnoteDto.DebitNoteHeader.CustomerId);
            para.Add("DebitNoteTypeId", debitnoteDto.DebitNoteHeader.DebitNoteType);
            para.Add("Description", debitnoteDto.DebitNoteHeader.Description);
            para.Add("TotalAmount", debitnoteDto.DebitNoteHeader.Totalamount);
            para.Add("Allocatedamount", debitnoteDto.DebitNoteHeader.Allocatedamount);
            para.Add("CurrencyId", debitnoteDto.DebitNoteHeader.CurrencyId);
            para.Add("ReasonId", debitnoteDto.DebitNoteHeader.ReasonId);
            para.Add("ExRate", debitnoteDto.DebitNoteHeader.Exrate);
            para.Add("ModuleId", debitnoteDto.DebitNoteHeader.ModuleId);
            para.Add("LocationId", debitnoteDto.DebitNoteHeader.CompanyId);
            para.Add("UserId", debitnoteDto.DebitNoteHeader.CreateUserId);

            /////// ---------- DEBITNOTE DETAILS ------------------
            DebitNoteDT.Columns.Add("InvoiceHdId", typeof(long));
            DebitNoteDT.Columns.Add("DepositAmount", typeof(decimal));
            DebitNoteDT.Columns.Add("ForeignDeposit", typeof(decimal));
            DebitNoteDT.Columns.Add("OutStandingAmt", typeof(decimal));
            DebitNoteDT.Columns.Add("ForeignOustand", typeof(decimal));
           
            foreach (var item in debitnoteDto.DebitNoteDetails)
            {
                DebitNoteDT.Rows.Add(item.InvoiceHdId, 
                                item.DepositAmount ,
                                item.ForeignDeposit,
                                item.OutstandAmount,
                                item.ForeignOutstand);
            }   
            para.Add("DebitNoteDT", DebitNoteDT.AsTableValuedParameter("DebitNoteDTType"));

            var result = await DbConnection.QueryFirstOrDefaultAsync<ReturnDto>("spTransDebitNoteSave", para
                , commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<DebitNoteListDto>> GetDebitNoteAsync(int customerId)
        {
            IEnumerable<DebitNoteListDto> debitNoteList;
            DynamicParameters para = new DynamicParameters();

            para.Add("CustomerId" , customerId);

            debitNoteList = await DbConnection.QueryAsync<DebitNoteListDto>("spTransGetDebitNoteList" , para
                    , commandType: CommandType.StoredProcedure);
            return debitNoteList;
        }

        public async Task<DebitNoteDto> GetDebitNoteDetailsAsync(int debitNoteHdId)
        {
            DebitNoteDto debitnote = new DebitNoteDto();
            DynamicParameters para = new DynamicParameters();

            para.Add("DebitNoteHdId" , debitNoteHdId);

            using (var multi = await DbConnection.QueryMultipleAsync("spTransGetDeabitNoteDetails", para, commandType: CommandType.StoredProcedure))
            {
                debitnote.DebitNoteHeader = multi.Read<TransDebitNoteHeader>();
                debitnote.DebitNoteDetails = multi.Read<DebitNoteDetailDto>();
            }
            return debitnote;
        }
       
        public async Task<int> CancelDebitNoteAsync(TransDebitNoteHeader debitnote)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("DebitNoteHdId" , debitnote.idDebitNoteHd);  
            para.Add("UserId" , debitnote.CreateUserId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await DbConnection.ExecuteAsync("spTransDebitNoteCancel" , para
                    , commandType: CommandType.StoredProcedure);
                    
            return para.Get<int>("Result");
        }

        public async Task<int> ReceiptAutoAllocateAsync(TransReceiptHeader receiptHd)
        {
            DynamicParameters para = new DynamicParameters();

            para.Add("ReceiptHdId", receiptHd.AutoId);
            para.Add("ReceiptNo", receiptHd.ReceiptNo);
            para.Add("Reference", receiptHd.Reference);
            para.Add("CustomerId", receiptHd.CustomerId);
            para.Add("ExchangeRate", receiptHd.ExchangeRate);
            para.Add("BankId", receiptHd.BankId);
            para.Add("DefaultCurrencyId", receiptHd.DefaultCurrencyId);
            para.Add("PaymodeId", receiptHd.PaymodeId);
            para.Add("ReceiptTypeId", receiptHd.ReceiptTypeId);
            para.Add("ReceiptTotal", receiptHd.ReceiptTotal);
            para.Add("AllocatedTotal", receiptHd.AllocatedTotal);
            para.Add("UserId", receiptHd.CreateUserId);
            para.Add("CustomerBankId", receiptHd.CustomerBankId);
            para.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            var result = await DbConnection.ExecuteAsync("spTransReceiptAutoAllocationSave", para
                , commandType: CommandType.StoredProcedure);            

            return para.Get<int>("Result");
        }



    }
}