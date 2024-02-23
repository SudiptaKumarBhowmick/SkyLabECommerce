import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { orderStatus } from '../../../_models/orderStatus';
import { GenericService } from '../../../_services/generic.service';
import { ListResponse } from '../../../_responses/listResponse';
import { SingleResponse } from '../../../_responses/singleResponse';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-order-status',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule
  ],
  templateUrl: './order-status.component.html',
  styleUrl: './order-status.component.css'
})
export class OrderStatusComponent {
  @ViewChild("orderStatusAddForm") orderStatusAddForm!: NgForm;
  @ViewChild("orderStatusEditForm") orderStatusEditForm!: NgForm;

  orderStatusList: orderStatus[] = [];

  addOrderStatus: orderStatus = {
    id: 0,
    statusCode: 0,
    status: ""
  };

  editOrderStatus: orderStatus = {
    id: 0,
    statusCode: 0,
    status: ""
  };

  @ViewChild("orderStatusEditFormCloseButton") orderStatusEditFormCloseButton!: ElementRef;

  constructor(private genericService: GenericService, private _toastr: ToastrService){
    
  }

  ngOnInit(){
    this.genericService.controllerName = "OrderStatus";
    this.getAllOrderStatus();
  }

  getAllOrderStatus(){
    this.genericService.getAll().subscribe(orderStatus => {
      var response = orderStatus as ListResponse<orderStatus>;
      this.orderStatusList = response.model;
    })
  }

  saveOrderStatus(){
    this.genericService.post<orderStatus>(this.addOrderStatus).subscribe(() => {
      this._toastr.success('Order status added successfully');
      this.resetForms("add");
      this.getAllOrderStatus();
    })
  }

  getOrderStatus(id: number){
    this.genericService.get(id).subscribe(result => {
      var response = result as SingleResponse<orderStatus>;
      this.editOrderStatus = response.model;
    })
  }

  updateOrderStatus(id: number, orderStatus: orderStatus){
    this.genericService.put<orderStatus>(id, orderStatus).subscribe(() => {
      this._toastr.success('Order status updated successfully');
      this.orderStatusEditFormCloseButton.nativeElement.click();
      this.resetForms("edit");
      this.getAllOrderStatus();
    })
  }

  deleteOrderStatus(id: number){
    this.genericService.delete(id).subscribe(result => {
      this._toastr.warning('Order status deleted successfully');
      this.getAllOrderStatus();
    })
  }

  resetForms(formType: string){
    if(formType == "add")    {
      this.orderStatusAddForm.reset({
        statusCode: 0,
        statusName: ""
      });
    }
    else if(formType == "edit"){
      this.orderStatusEditForm.reset({
        statusCode: 0,
        statusName: ""
      });
    }
  }
}
