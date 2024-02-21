import { userType } from "./userType";

export interface adminUser{
    id: number;
    userName: string;
    password: string;
    userEmail: string;
    userTypeId: number;
    userType: userType;
}