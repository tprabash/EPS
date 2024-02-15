export interface CustomerUser {
    autoId: number;
    title: string;
    firstName: string;
    lastName: string;
    designation: string;
    email: string;
    // isActive: boolean | null;
    customerId: number;
    createUserId: number | null;
}
