export class StockAdjuestment {
    autoId: number;
    articleId: number | null;
    colorId: number | null;
    sizeId: number;
    siteId: number;
    transDate: string;
    stockQty: number;
    price: number;
    expireDate: string;
    remarks: string;
    reason: string;
    createdUserId?: number;
    createdDateTime?: string;
    updateUserId?: number;
    updateDateTime?: string;
}