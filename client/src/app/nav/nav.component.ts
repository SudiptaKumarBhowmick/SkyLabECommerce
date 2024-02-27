import { Component } from '@angular/core';
import { CustomModalComponent } from "../custom-modal/custom-modal.component";
import { CommonModule } from '@angular/common';
import { CustomModalService } from '../_services/custom-modal.service';

@Component({
    selector: 'app-nav',
    standalone: true,
    templateUrl: './nav.component.html',
    styleUrl: './nav.component.css',
    imports: [
      CustomModalComponent,
      CommonModule
    ]
})
export class NavComponent {
  modalOpen$ = this.modalService.modalVisible$;

  constructor(private modalService: CustomModalService){}

  openModal(){
    this.modalService.openModal();
  }

  closeModal(){
    this.modalService.closeModal();
  }
}
