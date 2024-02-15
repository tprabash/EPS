export interface POHeader {
    poHeaderId: number;
    status: POStatus;
    poRef: string;
    createdDate: string;
    categoryId: number;
    attention: string;
    orderRef: string;
    supplierId: number;
    deliveryStartDate: string;
    deliveryCancelDate: string;
    dateInHouse: string;
    poTypeId: number;
    taxId: number;
    portOfLoading: number;
    portOfDischarge: number;
    countryOfOrign: number;
    countryOfConsolidation: number;
    countryOfFinalDestination: number;
    forwardingAgent: number;
    currencyId: number;
    locationId: number;
    paymentTerm: number;
    shipmentMode: number;
    deliveryTerm: number;
    leadTimeinDays: number;
    transitTimeinDays: number;
    supplierReference: string;
    packingType: string;
    remarks: string;
    createUserId: number;
    createDateTime: string;
    updateUserId: number;
    selectType: SelectType;
    updateDateTime: string;    
}

export enum POStatus {
    Created = 1,
    Waiting = 2,
    Approve = 3,
    Reject = 4,
    PartialAllocation = 5,
    Inactive = 6
}
export enum SelectType
{
    Article = 1,
    Indent = 2
}

