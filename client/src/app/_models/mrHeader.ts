export interface MrHeader {
    mRHeaderId: number;
    transDate: string;
    mRNo: string;
    assignedTo: number;
    siteId: number;
    remark: string;
    bActive: boolean;
    statusId: MRStatus;
    locationId: number;
    createUserId: number;
    createDateTime: string;
    updateUserId: number;
    updateDateTime: string;
}

export enum MRStatus {
    Created = 1,
    Waiting = 2,
    Approve = 3,
    Reject = 4,
    Closed = 5
}