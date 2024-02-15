export interface InvoiceDetails {
    autoId: number;
    invoiceHdId: number;
    dispatchDtId: number;
    sOItemDtId: number;
    description: string;
    qty: number;
    uOM: number;
    unitPrice: number;
    value: number;
    taxId: number;
    taxRate: number;
    taxAmount: number;
    grossAmount: number;
    discountP: number;
    discountA: number;
    netAmount: number;
    createUserId?: number;
}

