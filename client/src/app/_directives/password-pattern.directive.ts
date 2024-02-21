import { Directive } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, Validator } from '@angular/forms';
import { CustomValidationService } from '../_services/custom-validation.service';

@Directive({
  selector: '[appPasswordPattern]',
  standalone: true,
  providers: [{ provide: NG_VALIDATORS, useExisting: PasswordPatternDirective, multi: true}]
})
export class PasswordPatternDirective implements Validator {

  constructor(private customValidation: CustomValidationService) { }

  validate(control: AbstractControl): {[key: string]: any} | null {
    return this.customValidation.patternValidatorPassword()(control);
  }
}
