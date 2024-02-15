export interface SupplierHeader {
    supplierId: number;
    name: string;
    companyId: number;
    shortCode: string;
    address: string;
    city: string;
    state: string;
    zipPostalCode: string;
    countryId: number;
    tel: string;
    email: string;
    currencyId: number;
    supTypeId: number;
    vatNo: string;
    taxNo: string;
    tinNo: string;
    creditDays: number;
    shipmentTolorence: number;
    accountGroupId: number;
    //bActive: boolean;
    //locationId: number;
    createUserId?: number;
    createDateTime?: string;
    updateUserId?: number;
    updateDateTime?: string;
}
