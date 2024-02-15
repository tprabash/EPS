export interface ProdDefinition {
    autoId: number;
    seqNo?: number;
    pDName: string;
    pDHeaderId: number;
    processId: number;
    receiveSiteId: number;
    dispatchSiteId: number;
    receiveSite?: string;
    dispatchSite?: string;
    process?: string;
    createUserId ?: number;
}



