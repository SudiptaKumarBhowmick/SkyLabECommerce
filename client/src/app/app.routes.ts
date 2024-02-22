import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { UserTypeComponent } from './admin/entry-forms/user-type/user-type.component';
import { AdminUserComponent } from './admin/entry-forms/admin-user/admin-user.component';
import { ProductCategoryComponent } from './admin/entry-forms/product-category/product-category.component';
import { ProductSubcategoryComponent } from './admin/entry-forms/product-subcategory/product-subcategory.component';
import { ProductComponent } from './admin/entry-forms/product/product.component';
import { OrderStatusComponent } from './admin/entry-forms/order-status/order-status.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        children: [
            {path: 'admin', component: AdminDashboardComponent},
            {path: 'admin/user-type', component: UserTypeComponent},
            {path: 'admin/admin-user', component: AdminUserComponent},
            {path: 'admin/category', component: ProductCategoryComponent},
            {path: 'admin/sub-category', component: ProductSubcategoryComponent},
            {path: 'admin/product', component: ProductComponent},
            {path: 'admin/order-status', component: OrderStatusComponent}
        ]
    } 
];
