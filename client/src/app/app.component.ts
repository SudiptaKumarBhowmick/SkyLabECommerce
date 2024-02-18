import { Component } from '@angular/core';
import { Router, NavigationEnd, RouterOutlet, RouterEvent, ActivatedRoute } from '@angular/router';
import { CommonModule } from "@angular/common";
import { AdminHeaderComponent } from "./admin/admin-header/admin-header.component";
import { AdminSidebarComponent } from "./admin/admin-sidebar/admin-sidebar.component";
import { AdminFooterComponent } from "./admin/admin-footer/admin-footer.component";
import { NavComponent } from "./nav/nav.component";
import { SidebarComponent } from "./sidebar/sidebar.component";
//import { Router } from 'express';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [RouterOutlet, CommonModule, AdminHeaderComponent, AdminSidebarComponent, AdminFooterComponent, NavComponent, SidebarComponent]
})
export class AppComponent {
  title = 'client';
  currentUrl : string = "";
  isAdmin : boolean = false;


  constructor(private router: Router){
    
  }

  ngOnInit(){
    this.router.events.subscribe((e) => {
      if(e instanceof NavigationEnd){
        this.currentUrl = e.url;
        if(this.currentUrl.indexOf('admin') > 0){
          this.isAdmin = true;
        }
      }
    })
  }
}
