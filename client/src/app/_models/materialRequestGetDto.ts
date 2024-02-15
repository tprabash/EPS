import { MRStatus } from "./mrHeader";

export interface MaterialRequestGetDto {
    mrHeaderId: number;
    transDate: string;
    mrNo: string;
    assignedTo: number;
    siteId: number;
    remark: string;
    categoryId: number;
    bActive: boolean;
    statusId: MRStatus;
    locationId: number;
    mrDetailsId: number;
    articleName: string;
    stockCode: string;
    articleId: number;
    colorId: number;
    color: string;
    sizeId: number;
    size: string;
    reqQty: number;
    approvedQty: number;
    uomId: number;
    unit: string;
    unitPrice: number;
    requireDate: string;
    location: string;
    siteName: string;
    category: string;
    agentName: string;
}

