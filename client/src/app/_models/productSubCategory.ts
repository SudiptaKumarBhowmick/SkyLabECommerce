import { productCategory } from "./productCategory";

export interface productSubCategory{
    id: number;
    subCategoryName: string;
    productCategoryId: number;
    productCategory: productCategory;
}