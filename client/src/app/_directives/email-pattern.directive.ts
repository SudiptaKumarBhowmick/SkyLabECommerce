import { Directive, Input } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';
import { CustomValidationService } from '../_services/custom-validation.service';

@Directive({
  selector: '[appEmailPattern]',
  standalone: true,
  providers: [{ provide: NG_VALIDATORS, useExisting: EmailPatternDirective, multi: true}]
})
export class EmailPatternDirective implements Validator {

  constructor(private customValidation: CustomValidationService) { }

  validate(control: AbstractControl): { [key: string]: any} | null {
    return this.customValidation.patternValidatorEmail()(control);
  }
}
