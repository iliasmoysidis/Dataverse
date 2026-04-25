import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";

export const guestGuard: CanActivateFn = () => {
    const router = inject(Router);

    if (localStorage.getItem('token')) {
        router.navigate(['/absences']);
        return false;
    }

    return true;
}
