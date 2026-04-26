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
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBar } from '@angular/material/snack-bar';

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
        MatSelectModule,
        MatDatepickerModule,
        MatNativeDateModule,
    ],
})
export class Absences {
    private absenceService = inject(Absence);
    private snackBar = inject(MatSnackBar);

    AbsenceStatus = AbsenceStatus;

    displayedColumns = ['name', 'surname', 'email', 'startDate', 'endDate', 'status', 'actions'];

    dataSource = new MatTableDataSource<AbsenceRow>([]);

    search = '';
    status: number | null = null;
    from = '';
    to = '';

    sortBy = 'startDate';
    desc = false;

    pageIndex = 0;
    pageSize = 5;
    totalCount = 0;

    ngOnInit() {
        this.loadAbsences();
    }

    loadAbsences() {
        const params: any = {
            page: this.pageIndex + 1,
            limit: this.pageSize,
            sortBy: this.sortBy,
            desc: this.desc,
        };

        if (this.search) params.search = this.search;
        if (this.status) params.status = this.status;
        if (this.from) params.from = this.from;
        if (this.to) params.to = this.to;

        this.absenceService.getAll(params).subscribe({
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
                const message = err.error?.message ?? 'Failed to load absences';

                this.snackBar.open(message, 'Close', {
                    duration: 4000,
                    horizontalPosition: 'end',
                    verticalPosition: 'top',
                    panelClass: ['error-snackbar'],
                });
            },
        });
    }

    applyFilter(event: Event) {
        this.search = (event.target as HTMLInputElement).value.trim();
        this.pageIndex = 0;
        this.loadAbsences();
    }

    statusChanged(value: number | null) {
        this.status = value;
        this.pageIndex = 0;
        this.loadAbsences();
    }

    fromChanged(event: any) {
        this.from = event.value ? this.formatDate(event.value) : '';
        this.pageIndex = 0;
        this.loadAbsences();
    }

    toChanged(event: any) {
        this.to = event.value ? this.formatDate(event.value) : '';
        this.pageIndex = 0;
        this.loadAbsences();
    }

    sort(column: string) {
        if (this.sortBy === column) {
            this.desc = !this.desc;
        } else {
            this.sortBy = column;
            this.desc = false;
        }

        this.loadAbsences();
    }

    onPageChange(event: PageEvent) {
        this.pageIndex = event.pageIndex;
        this.pageSize = event.pageSize;

        this.loadAbsences();
    }

    formatDate(date: Date): string {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');

        return `${year}-${month}-${day}`;
    }

    isSorted(column: string): boolean {
        return this.sortBy === column;
    }

    sortIcon(column: string): string {
        if (this.sortBy !== column) return 'unfold_more';

        return this.desc ? 'arrow_downward' : 'arrow_upward';
    }

    approve(id: number) {
        this.absenceService.approve(id).subscribe({
            next: () => {
                this.snackBar.open('Absence approved', '', {
                    duration: 2500,
                    horizontalPosition: 'end',
                    verticalPosition: 'top',
                    panelClass: ['success-snackbar'],
                });

                this.loadAbsences();
            },
            error: () => {
                this.snackBar.open('Failed to approve', 'Close', {
                    duration: 4000,
                    horizontalPosition: 'end',
                    verticalPosition: 'top',
                    panelClass: ['error-snackbar'],
                });
            },
        });
    }

    reject(id: number) {
        this.absenceService.reject(id).subscribe({
            next: () => {
                this.snackBar.open('Absence rejected', '', {
                    duration: 2500,
                    horizontalPosition: 'end',
                    verticalPosition: 'top',
                    panelClass: ['success-snackbar'],
                });
                this.loadAbsences();
            },
            error: () => {
                this.snackBar.open('Failed to reject', 'Close', {
                    duration: 4000,
                    horizontalPosition: 'end',
                    verticalPosition: 'top',
                    panelClass: ['error-snackbar'],
                });
            },
        });
    }
}
