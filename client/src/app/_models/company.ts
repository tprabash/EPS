export interface Company {
    autoId: number;
    companyName: string;
    address: string;
    defCurrencyId: number;
    svatNo: string;
    boiRegNo: string;
    createUserId?: number;
    createDateTime?: string;
    updateUserId?: number;
    updateDateTime?: string;
}