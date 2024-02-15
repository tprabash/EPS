export interface CustomerAddressList {
    autoId: number;
    customerId: number;
    cusLocationId: number;
    addressTypeId: number;
    addressTo: string;
    address: string;
    city: string;
    email: string;
    tel: string;
    zipPostalCode: string;
    countryId: number;
    vatNo: string;
    taxNo: string;
    tinNo: string;
    currencyId: number;    
    createUserId: number | null;
}
