export interface Bank {
    autoId: number;
    name: string;
    branch: string;
    accountNo: string;
    currencyId: number;
    nextChequeNo: number;
    createUserId?: number;
}
