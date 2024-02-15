export interface IndentDetails {
    indentId: number;
    refNo: string;
    articleId: number;
    colorId: number;
    sizeId: number;
    openQty: number;
    orderQty: number;
    mrHeaderId: number;
    mrDetailsId: number;
    uomId: number;
    assignTo: number;
    transDate: string;
    type: IndentType;
    status: IndentStatus;
    createUserId: number;
    createDateTime: string;
    updateUserId: number;
    updateDateTime: string;    
}

export enum IndentStatus {
    Active = 1,
    Inactive = 2,
    Complete = 3,
    Cancelled = 4,
    WattingForApproval = 5
}

export enum IndentType {
    MRIndent = 1,
    AdhocIndentSalesOrder = 2,
    AdhocIndentNone = 3,
    OCIndent = 4    
}
