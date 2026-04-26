import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

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
export class Absence {
    private http = inject(HttpClient);

    private api = 'http://localhost:5021/api/absences';

    getAll(params: any): Observable<PagedResult<AbsenceDto>> {
        return this.http.get<PagedResult<AbsenceDto>>(this.api, {
            params,
        });
    }
}
