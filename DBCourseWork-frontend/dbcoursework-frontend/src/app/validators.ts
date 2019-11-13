import { AbstractControl, ValidatorFn, Validators } from "@angular/forms";

export const isNotCurrentUser = (name: string): ValidatorFn => {
  return (control: AbstractControl): {[key: string]: any} | null => {
    if (control.value === name){
      return {'isCurrentUser': {value: true}};
    }
    return null;
  }
}

export const isPositiveNumber: ValidatorFn = Validators.pattern(/^[1-9]\d*$/);