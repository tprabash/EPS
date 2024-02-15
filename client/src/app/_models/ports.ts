export interface Ports {
    portId: number;
    portCode: string;
    portName: string;
    countryId: number;
    portOfLoading: boolean;
    portOfDischarge: boolean;
    createUserId?: number;
    createDateTime?: string;
    updateUserId?: number;
    updateDateTime?: string;
}
