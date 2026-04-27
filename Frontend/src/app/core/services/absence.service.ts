import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface PagedResult<T> {
    items: T[];
    page: number;
    limit: number;
    totalCount: number;
    totalPages: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
}

export interface AbsenceDto {
    id: number;
    startDate: string;
    endDate: string;
    status: number;
    user: UserDto;
}

export interface UserDto {
    id: number;
    email: string;
    name: string;
    surname: string;
    role: number;
}

export interface AbsenceRow {
    id: number;
    userId: number;
    name: string;
    surname: string;
    email: string;
    startDate: string;
    endDate: string;
    status: number;
}

@Injectable({
    providedIn: 'root',
})
export class AbsenceService {
    private http = inject(HttpClient);

    private api = `${environment.apiUrl}/absences`;

    getAll(params: any): Observable<PagedResult<AbsenceDto>> {
        return this.http.get<PagedResult<AbsenceDto>>(this.api, {
            params,
        });
    }

    approve(id: number) {
        return this.http.patch(`${this.api}/${id}/approve`, {});
    }

    reject(id: number) {
        return this.http.patch(`${this.api}/${id}/reject`, {});
    }

    cancel(id: number): Observable<any> {
        return this.http.patch(`${this.api}/${id}/cancel`, {});
    }

    create(payload: { startDate: string; endDate: string }) {
        return this.http.post(this.api, payload);
    }
}
