import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { UserTypeComponent } from './admin/entry-forms/user-type/user-type.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        children: [
            {path: 'admin', component: AdminDashboardComponent},
            {path: 'admin/user-type', component: UserTypeComponent}
        ]
    } 
];
