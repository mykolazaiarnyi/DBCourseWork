import { AbstractControl, ValidatorFn } from "@angular/forms";

export const isNotCurrentUser = (name: string): ValidatorFn => {
  return (control: AbstractControl): {[key: string]: any} | null => {
    if (control.value === name){
      return {'isCurrentUser': {value: true}};
    }
    return null;
  }
}