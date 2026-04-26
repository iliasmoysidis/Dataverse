import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';

import { Absence, AbsenceRow } from '../../core/services/absence';

export enum AbsenceStatus {
    Pending = 1,
    Approved = 2,
    Rejected = 3,
}

@Component({
    selector: 'app-absences',
    standalone: true,
    templateUrl: './absences.html',
    styleUrl: './absences.css',
    imports: [
        CommonModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule,
        MatChipsModule,
    ],
})
export class Absences {
    private absenceService = inject(Absence);

    AbsenceStatus = AbsenceStatus;

    displayedColumns = ['name', 'surname', 'email', 'startDate', 'endDate', 'status'];

    dataSource = new MatTableDataSource<AbsenceRow>([]);

    pageIndex = 0;
    pageSize = 5;
    totalCount = 0;
    search = '';

    ngOnInit() {
        this.loadAbsences();
    }

    loadAbsences() {
        this.absenceService.getAll(this.pageIndex + 1, this.pageSize, this.search).subscribe({
            next: (res) => {
                this.totalCount = res.totalCount;

                this.dataSource.data = res.items.map((x) => ({
                    id: x.id,
                    name: x.user.name,
                    surname: x.user.surname,
                    email: x.user.email,
                    startDate: x.startDate,
                    endDate: x.endDate,
                    status: x.status,
                }));
            },
            error: (err) => {
                console.error(err);
            },
        });
    }

    applyFilter(event: Event) {
        this.search = (event.target as HTMLInputElement).value.trim();
        this.pageIndex = 0;
        this.loadAbsences();
    }

    onPageChange(event: PageEvent) {
        this.pageIndex = event.pageIndex;
        this.pageSize = event.pageSize;

        this.loadAbsences();
    }
}
