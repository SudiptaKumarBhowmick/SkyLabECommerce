import { Component } from '@angular/core';
import { AdminHeaderComponent } from "../admin-header/admin-header.component";
import { AdminSidebarComponent } from "../admin-sidebar/admin-sidebar.component";
import { AdminContentWrapperComponent } from "../admin-content-wrapper/admin-content-wrapper.component";
import { AdminControlSidebarComponent } from "../admin-control-sidebar/admin-control-sidebar.component";
import { AdminFooterComponent } from "../admin-footer/admin-footer.component";

@Component({
    selector: 'app-admin-dashboard',
    standalone: true,
    templateUrl: './admin-dashboard.component.html',
    styleUrl: './admin-dashboard.component.css',
    imports: [AdminHeaderComponent, AdminSidebarComponent, AdminContentWrapperComponent, AdminControlSidebarComponent, AdminFooterComponent]
})
export class AdminDashboardComponent {

}
