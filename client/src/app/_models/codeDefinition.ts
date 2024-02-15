export interface CodeDefinition {
    autoId: number;
    categoryId: number;
    prodTypeId: number;
    prodGroupId: number;
    sortOrder: number;
    IsProductField: boolean;
    flexFieldId: number;
    fieldName: string;
    isCode: boolean;
    isName: boolean;
    isCounter: boolean;
    isValue: boolean;
    counterPad: number;
    counterStart: number;
    seqNo: number;
    isSeperator: boolean;
    seperator: string;
    createUserId?: number;
}


