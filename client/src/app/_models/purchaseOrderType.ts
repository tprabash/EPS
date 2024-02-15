export interface PurchaseOrderType {
    poTypeId: number;
    details: string;
    createUserId?: number;
    createDateTime?: string;
    updateUserId?: number;
    updateDateTime?: string;
}