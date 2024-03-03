import { product } from "./product";

export interface productImage{
    id: number;
    url: string;
    isMain: boolean;
    publicId: string;
    productId: number;
    product: product;
}