import { IndentStatus } from "./IndentDetails";

export interface IndentHdDto {
    indentNo: string;
    mrNo: string;
    createdDate: string;
    indentHeaderId: number;   
    status: IndentStatus;
    assignTo: string;
    createdUser: string;
    statusName: string;
    memberCompany: string;
    division: string;
}
