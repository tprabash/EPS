export interface Forwarder {
    forwarderId: number;
    name: string;
    contact: string;
    emailId: string;
    createUserId?: number;
    createDateTime?: string;
    updateUserId?: number;
    updateDateTime?: string;
}