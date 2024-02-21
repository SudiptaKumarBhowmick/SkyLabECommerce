import { Injectable } from '@angular/core';
import { AbstractControl, ValidatorFn } from "@angular/forms";

@Injectable({
  providedIn: 'root'
})
export class CustomValidationService {

  constructor() { }

  patternValidatorPassword() : ValidatorFn{
    return (control : AbstractControl) : { [key: string]: any} | null => {
      if(!control.value){
        return null;
      }

      const regx = RegExp(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).*$/);
      const valid = regx.test(control.value);
      return valid ? null : { invalidPassword : true};
    };
  }

  patternValidatorEmail() : ValidatorFn{
    return (control : AbstractControl) : { [key: string]: any} | null => {
      if(!control.value){
        return null;
      }

      const regx = RegExp(/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,10}$/);
      const valid = regx.test(control.value);
      return valid ? null : { invalidEmail: true };
    }
  }

  dropdownSelectedValueValidator(defaultSelectedValue: number): ValidatorFn{
    console.log('received...........');
    return (control: AbstractControl) : { [key: string]: any} | null => {
      if(!control.value){
        return null;
      }

      console.log('control.value: ' + control.value);
      console.log('defaultSelectedValue: ' + defaultSelectedValue);
      let valid: boolean = false;
      if(defaultSelectedValue != 0 && defaultSelectedValue != control.value){
        valid = true;
      }
      return valid ? null : { invalidSelection: true};
    }
  }
}
