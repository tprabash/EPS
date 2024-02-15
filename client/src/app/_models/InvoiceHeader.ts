export interface InvoiceHeader {
    autoId: number;
    invoiceNo: string;
    transDate: string;
    locationId: number;
    customerId: number;
    validTo: string;
    totalValue: number;
    taxValue: number;
    grossValue: number;
    nBTRate: number;
    nBTAmount: number;
    nBTGross: number;
    netValue: number;
    customerAddId: number;
    invCurrencyId: number;
    baseCurrencyId: number;
    exchangeRate: number;
    attention: string;
    paymentDueDate: string;
    receivedValue: number;
    transUpdated: boolean;
    bActive: boolean;
    approvedAgent: number;
    createUserId?: number;
}
