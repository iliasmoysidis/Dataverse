import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private http = inject(HttpClient);

    private api = `${environment.apiUrl}/users`;

    login(data: { email: string; password: string }): Observable<any> {
        return this.http.post(`${this.api}/login`, data);
    }

    register(data: {
        name: string;
        surname: string;
        email: string;
        password: string;
        role: number;
    }): Observable<any> {
        return this.http.post(`${this.api}/register`, data);
    }

    logout() {
        localStorage.removeItem('token');
    }

    getToken(): string {
        return localStorage.getItem('token') ?? '';
    }

    isLoggedIn(): boolean {
        return !!this.getToken();
    }

    getPayload(): any {
        const token = this.getToken();

        if (!token) return null;

        try {
            const base64 = token.split('.')[1];
            const json = atob(base64);
            return JSON.parse(json);
        } catch {
            return null;
        }
    }

    getUserId(): number {
        const payload = this.getPayload();

        return Number(
            payload?.nameid ??
                payload?.sub ??
                payload?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] ??
                0,
        );
    }

    getRole(): number {
        const payload = this.getPayload();

        const role =
            payload?.role ??
            payload?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ??
            '';

        if (role === 'Employee') return 1;
        if (role === 'Manager') return 2;

        return 0;
    }

    isEmployee(): boolean {
        return this.getRole() === 1;
    }

    isManager(): boolean {
        return this.getRole() === 2;
    }
}
