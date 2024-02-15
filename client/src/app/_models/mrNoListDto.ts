import { MRStatus } from "./mrHeader";

export interface MRNoListDto {
    mrHeaderId: number;
    transDate: string;
    mrNo: string;
    assignedTo: number;
    siteId: number;
    categoryId: number;
    remark: string;
    bActive: boolean;
    statusId: MRStatus;
    locationId: number;
    location: string;
    siteName: string;
    agentName: string;
    category: string;
}

