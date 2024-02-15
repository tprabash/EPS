export interface SalesOrderDelivery {
    deliveryId: number;
    soItemDtId: number;
    deliveryRef: string;
    deliveryDate: string;
    qty: number;
    sizeId: number;
    size: string;
    colorId: number;
    color: string;
    articleId: number;
    article: string;
}
