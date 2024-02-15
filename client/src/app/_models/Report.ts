export interface Report {
    autoId: number;
    module: string;
    reportNo?: string;
    reportName: string;
    ssrsReportName: string;    
    isActive?: boolean;
    fromDate: boolean;
    toDate: boolean;
    purpose: string;
    createUserID?: number;
    createDateTime?: string;
    updateUserID?: number;
    updateDateTime?: string;
    bCustomer?: boolean;
    bDeliveryLocation?: boolean;
    bCurrency?: boolean;
    bBrand?: boolean;
    bBrandCode?: boolean;
    bCustomerPO?: boolean;
}



