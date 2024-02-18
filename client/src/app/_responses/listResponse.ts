export interface ListResponse<T>{
    statusCode: number;
    model: T[];
    message: string;
}