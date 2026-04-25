import { AbstractControl, ValidationErrors } from "@angular/forms";

export function roleValidator(control: AbstractControl): ValidationErrors | null {
    const allowed = [1, 2];

    return allowed.includes(control.value)
        ? null
        : {invalidRole: true}
}
