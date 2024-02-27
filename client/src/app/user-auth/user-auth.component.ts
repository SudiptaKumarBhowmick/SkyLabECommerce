import { Component } from '@angular/core';
import { CustomModalService } from '../_services/custom-modal.service';
import { CustomModalComponent } from "../custom-modal/custom-modal.component";
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-user-auth',
    standalone: true,
    templateUrl: './user-auth.component.html',
    styleUrl: './user-auth.component.css',
    imports: [
      CustomModalComponent,
      CommonModule
    ]
})
export class UserAuthComponent {
  modalOpen$ = this.modalService.modalVisible$;

  constructor(private modalService: CustomModalService){}

  openModal(){
    this.modalService.openModal();
  }

  closeModal(){
    this.modalService.closeModal();
  }
}
