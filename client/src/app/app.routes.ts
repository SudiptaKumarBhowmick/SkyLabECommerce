import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { UserTypeComponent } from './admin/entry-forms/user-type/user-type.component';
import { AdminUserComponent } from './admin/entry-forms/admin-user/admin-user.component';
import { ProductCategoryComponent } from './admin/entry-forms/product-category/product-category.component';
import { ProductSubcategoryComponent } from './admin/product-subcategory/product-subcategory.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        children: [
            {path: 'admin', component: AdminDashboardComponent},
            {path: 'admin/user-type', component: UserTypeComponent},
            {path: 'admin/admin-user', component: AdminUserComponent},
            {path: 'admin/category', component: ProductCategoryComponent},
            {path: 'admin/sub-category', component: ProductSubcategoryComponent}
        ]
    } 
];
