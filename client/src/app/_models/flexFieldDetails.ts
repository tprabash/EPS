export interface FlexFieldDetails {
    autoId: number;
    categoryId: number;
    prodTypeId: number;
    moduleId: number;
    flexFieldName: string;
    flexFieldCode: string;
    dataType: string;
    valueList: boolean;
    isActive?: boolean | null;
    mandatory: boolean;
    createUserId: number | null;
}

