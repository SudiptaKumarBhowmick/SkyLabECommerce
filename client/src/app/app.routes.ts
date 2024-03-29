import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { UserTypeComponent } from './admin/entry-forms/user-type/user-type.component';
import { AdminUserComponent } from './admin/entry-forms/admin-user/admin-user.component';
import { ProductCategoryComponent } from './admin/entry-forms/product-category/product-category.component';
import { ProductSubcategoryComponent } from './admin/entry-forms/product-subcategory/product-subcategory.component';
import { ProductComponent } from './admin/entry-forms/product/product.component';
import { OrderStatusComponent } from './admin/entry-forms/order-status/order-status.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { UserAuthComponent } from './user-auth/user-auth.component';
import { AdminLoginComponent } from './admin/admin-login/admin-login.component';
import { adminAuthGuard, adminAuthRedirectGuard } from './_guards/admin-auth.guard';
import { UploadProductImageComponent } from './admin/entry-forms/upload-product-image/upload-product-image.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'user-auth', component: UserAuthComponent},
    {path: 'admin/login', component: AdminLoginComponent, canActivate: [adminAuthRedirectGuard]},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [adminAuthGuard],
        children: [
            {path: 'admin', component: AdminDashboardComponent},
            {path: 'admin/user-type', component: UserTypeComponent},
            {path: 'admin/admin-user', component: AdminUserComponent},
            {path: 'admin/category', component: ProductCategoryComponent},
            {path: 'admin/sub-category', component: ProductSubcategoryComponent},
            {path: 'admin/product', component: ProductComponent},
            {path: 'admin/order-status', component: OrderStatusComponent},
            {path: 'admin/upload-product-image', component: UploadProductImageComponent}
        ]
    },
    {path: '**', component: NotFoundComponent, pathMatch: 'full'}
];
