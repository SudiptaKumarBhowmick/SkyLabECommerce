import { Component } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { testMenuModel } from '../_models/testMenuModel';
import { TestValidateSelectOptionDirectiveDirective } from '../_directives/test-validate-select-option-directive.directive';

@Component({
  selector: 'app-test-dropdown-custom-directive',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    TestValidateSelectOptionDirectiveDirective
  ],
  templateUrl: './test-dropdown-custom-directive.component.html',
  styleUrl: './test-dropdown-custom-directive.component.css'
})
export class TestDropdownCustomDirectiveComponent {
  testMenuList: testMenuModel[] = [
    {
      id: -1,
      name: "Select......"
    },
    {
      id: 1,
      name: "Test 1"
    },
    {
      id: 2,
      name: "Test 2"
    },
    {
      id: 3,
      name: "Test 3"
    }
  ];

  testFormData: testMenuModel = {
    id: -1,
    name: ""
  };
}
