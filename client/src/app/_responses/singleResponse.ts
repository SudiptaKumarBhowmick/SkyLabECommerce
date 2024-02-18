export interface SingleResponse<T>{
    statusCode: number;
    model: T;
    message: string;
}