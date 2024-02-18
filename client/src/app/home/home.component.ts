import { Component } from '@angular/core';
import { NavComponent } from "../nav/nav.component";
import { SidebarComponent } from "../sidebar/sidebar.component";
import { Router } from '@angular/router';

@Component({
    selector: 'app-home',
    standalone: true,
    templateUrl: './home.component.html',
    styleUrl: './home.component.css',
    imports: [NavComponent, SidebarComponent]
})
export class HomeComponent {

}
