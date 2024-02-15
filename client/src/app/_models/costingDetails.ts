export interface CostingDetails {
    autoId: number;
    costHeaderId: number;
    groupId: number;
    groupOrder: number;
    articleId: number;
    colorId: number;
    sizeId: number;
    unitId: number;
    gSM: string;
    fluteId: number;
    consumption: number;
    wastage: number;
    weight: number;
    requirment: number;
    cost: number;
    value: number;    
    createUserId?: number;
}

