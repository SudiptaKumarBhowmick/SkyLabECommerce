import { productCategory } from "./productCategory";
import { productSubCategory } from "./productSubCategory";

export interface product{
    id: number;
    name: string;
    description: string;
    productCategoryId: number;
    productSubCategoryId: number;
    productCategory: productCategory;
    productSubCategory: productSubCategory;
}