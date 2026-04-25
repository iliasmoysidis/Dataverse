import { Component, inject } from '@angular/core';
import {
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
import { Auth } from '../../services/auth';
import { Router, RouterLink } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
    selector: 'app-login',
    imports: [
        FormsModule,
        ReactiveFormsModule,
        MatCardModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule,
        RouterLink,
    ],
    templateUrl: './login.html',
    styleUrl: './login.css',
})
export class Login {
    private fb = inject(NonNullableFormBuilder);
    private auth = inject(Auth);
    private snackBar = inject(MatSnackBar);
    private router = inject(Router);

    submitted = false;

    form = this.fb.group({
        email: ['', [Validators.required, Validators.email, Validators.maxLength(255)]],
        password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(100)]],
    });

    email = '';
    password = '';

    login() {
        this.submitted = true;

        if (this.form.invalid) {
            return;
        }

        const payload = this.form.getRawValue();
        this.auth.login(payload).subscribe({
            next: (res) => {
                localStorage.setItem('token', res.token);

                this.router.navigate(['/absences']);

                this.snackBar.open('Login successful', '', {
                    duration: 1500,
                    horizontalPosition: 'end',
                    verticalPosition: 'top',
                    panelClass: ['success-snackbar'],
                });
            },
            error: (err) => {
                const message = err.error?.message ?? 'Invalid credentials';

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
