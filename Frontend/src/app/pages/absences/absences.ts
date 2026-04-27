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

import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

import { MatSnackBar } from '@angular/material/snack-bar';

import { AbsenceRow, AbsenceService } from '../../core/services/absence.service';
import { AuthService } from '../../core/services/auth.service';

export enum AbsenceStatus {
    Pending = 1,
    Approved = 2,
    Rejected = 3,
    Canceled = 4,
}

export enum Role {
    Employee = 1,
    Manager = 2,
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
    private absenceService = inject(AbsenceService);
    private snackBar = inject(MatSnackBar);
    private auth = inject(AuthService);

    AbsenceStatus = AbsenceStatus;
    Role = Role;

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

    role = this.auth.getRole();
    currentUserId = this.auth.getUserId();

    isEmployee = this.auth.isEmployee();
    isManager = this.auth.isManager();

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
                    userId: x.user.id,
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

                this.showError(message);
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

    approve(id: number) {
        this.absenceService.approve(id).subscribe({
            next: () => {
                this.showSuccess('Absence approved');
                this.loadAbsences();
            },
            error: () => this.showError('Failed to approve'),
        });
    }

    reject(id: number) {
        this.absenceService.reject(id).subscribe({
            next: () => {
                this.showSuccess('Absence rejected');
                this.loadAbsences();
            },
            error: () => this.showError('Failed to reject'),
        });
    }

    cancel(id: number) {
        this.absenceService.cancel(id).subscribe({
            next: () => {
                this.showSuccess('Absence canceled');
                this.loadAbsences();
            },
            error: (err) => {
                const message = err.error?.message ?? 'Failed to cancel';

                this.showError(message);
            },
        });
    }

    formatDate(date: Date): string {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');

        const day = String(date.getDate()).padStart(2, '0');

        return `${year}-${month}-${day}`;
    }

    sortIcon(column: string): string {
        if (this.sortBy !== column) return 'unfold_more';

        return this.desc ? 'arrow_downward' : 'arrow_upward';
    }

    private showSuccess(message: string) {
        this.snackBar.open(message, '', {
            duration: 2500,
            horizontalPosition: 'end',
            verticalPosition: 'top',
            panelClass: ['success-snackbar'],
        });
    }

    private showError(message: string) {
        this.snackBar.open(message, 'Close', {
            duration: 4000,
            horizontalPosition: 'end',
            verticalPosition: 'top',
            panelClass: ['error-snackbar'],
        });
    }
}
