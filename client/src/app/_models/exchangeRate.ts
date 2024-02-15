export interface ExchangeRate {
    autoId: number;
    currencyFId: number;
    currencyTId: number;
    rate: number;
    validFrom: string;
    validTo: string;
    createUserId?: number;
}

