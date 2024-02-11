import { PermitMenu } from "./permitMenu";

export interface User {
    locationId?: number;
    userId: number;
    moduleId: number;
    userName: string;
    token: string;
    locations: any;
    permitMenus?: PermitMenu[];
}


