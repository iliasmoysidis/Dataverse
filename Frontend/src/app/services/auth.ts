import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class Auth {
    private http = inject(HttpClient);

    private api = 'http://localhost:5021/api/users';

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
}
