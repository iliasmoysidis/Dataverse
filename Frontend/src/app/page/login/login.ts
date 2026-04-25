import { Component, inject } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Auth } from '../../services/auth';

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
    ],
    templateUrl: './login.html',
    styleUrl: './login.css',
})
export class Login {
    private auth = inject(Auth);

    email = '';
    password = '';

    login() {
        this.auth
            .login({
                email: this.email,
                password: this.password,
            })
            .subscribe({
                next: (res) => {
                    localStorage.setItem('token', res.token);
                },
                error: (err) => {
                    console.error(err);
                },
            });
    }
}
