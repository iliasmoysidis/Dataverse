import { Component, inject } from '@angular/core';
import {
    FormBuilder,
    FormsModule,
    NonNullableFormBuilder,
    ReactiveFormsModule,
    Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { Router, RouterLink } from '@angular/router';
import { Auth } from '../../services/auth';
import { roleValidator } from '../../validators/role';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
    selector: 'app-register',
    imports: [
        ReactiveFormsModule,
        RouterLink,
        MatCardModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule,
        MatSelectModule,
    ],
    templateUrl: './register.html',
    styleUrl: './register.css',
})
export class Register {
    private fb = inject(NonNullableFormBuilder);
    private auth = inject(Auth);
    private router = inject(Router);
    private snackBar = inject(MatSnackBar);

    submitted = false;
    serverError = '';

    form = this.fb.group({
        name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
        surname: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
        email: ['', [Validators.required, Validators.email, Validators.maxLength(255)]],
        password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(100)]],
        role: [1, [Validators.required, roleValidator]],
    });

    register() {
        this.submitted = true;

        if (this.form.invalid) {
            return;
        }
        const payload = this.form.getRawValue();

        this.auth.register(payload).subscribe({
            next: () => {
                this.snackBar.open('Account created successfully', '', {
                    duration: 1500,
                    horizontalPosition: 'end',
                    verticalPosition: 'top',
                    panelClass: ['success-snackbar'],
                });

                setTimeout(() => {
                    this.router.navigate(['/login'], { replaceUrl: true });
                }, 1500);
            },
            error: (err) => {
                const message = err.error?.message ?? 'Registration failed';

                this.snackBar.open(message, 'Close', {
                    duration: 4000,
                    horizontalPosition: 'end',
                    verticalPosition: 'top',
                    panelClass: ['error-snackbar'],
                });
            },
        });
    }
}
