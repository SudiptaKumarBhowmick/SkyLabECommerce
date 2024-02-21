import { Directive, Input } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';
import { CustomValidationService } from '../_services/custom-validation.service';

@Directive({
  selector: '[appTestValidate]',
  standalone: true,
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: TestValidateSelectOptionDirectiveDirective,
      multi: true
    }
  ]
})
export class TestValidateSelectOptionDirectiveDirective implements Validator {
  @Input() appTestValidate: string = "";

  constructor(private customValidation: CustomValidationService){

  }

  validate(control: AbstractControl): { [key: string]: any } | null {
    console.log('Invoke from validate function');
    console.log(control.value == this.appTestValidate);
    return control.value == this.appTestValidate ? { 'defaultSelected': true } : null;
  }
}
