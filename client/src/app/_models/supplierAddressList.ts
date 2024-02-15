export interface SupplierAddressList {
    suppAddId: number;
    supplierId: number;
    addressTypeId: number;
    address: string;
    city: string;
    state: string;
    zipPostalCode: string;
    countryId: number;
    tel: string;
    vatNo: string;
    taxNo: string;
    tinNo: string;    
    userId: number | null;
}