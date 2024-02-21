import { Directive, Input } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, Validator } from '@angular/forms';

@Directive({
  selector: '[appValidateSelectOption]',
  standalone: true,
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: ValidateSelectOptionDirective,
      multi: true
    }
  ]
})
export class ValidateSelectOptionDirective implements Validator {
  @Input() appValidateSelectOption: string = "";

  constructor() { }

  validate(control: AbstractControl): { [key: string]: any } | null {
    return control.value == this.appValidateSelectOption ? { 'defaultSelected': true } : null;
  }
}