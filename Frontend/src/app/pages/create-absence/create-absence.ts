import { Component, DestroyRef, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AbsenceService } from '../../core/services/absence.service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Router, RouterLink } from '@angular/router';
import { dateRangeValidator } from '../../shared/validators/dateRange';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
    selector: 'app-create-absence',
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatCardModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatSnackBarModule,
        RouterLink,
    ],
    templateUrl: './create-absence.html',
    styleUrl: './create-absence.css',
})
export class CreateAbsence {
    fb = inject(FormBuilder);
    private absenceService = inject(AbsenceService);
    private snackBar = inject(MatSnackBar);
    private router = inject(Router);
    private destroyRef = inject(DestroyRef);

    today = new Date();

    submitted = false;

    form = this.fb.group(
        {
            startDate: [null as Date | null, Validators.required],
            endDate: [null as Date | null, Validators.required],
        },
        { validators: dateRangeValidator },
    );

    submit() {
        this.submitted = true;

        if (this.form.invalid) return;

        const raw = this.form.getRawValue();

        const payload = {
            startDate: this.formatDate(raw.startDate!),
            endDate: this.formatDate(raw.endDate!),
        };

        this.absenceService
            .create(payload)
            .pipe(takeUntilDestroyed(this.destroyRef))
            .subscribe({
                next: () => {
                    this.snackBar.open('Absence request created', '', {
                        duration: 2500,
                        horizontalPosition: 'end',
                        verticalPosition: 'top',
                        panelClass: ['success-snackbar'],
                    });
                    this.router.navigate(['/absences'], { replaceUrl: true });
                },
                error: (err) => {
                    const message = err.error?.message ?? 'Failed to create absence request';

                    this.snackBar.open(message, 'Close', {
                        duration: 4000,
                        horizontalPosition: 'end',
                        verticalPosition: 'top',
                        panelClass: ['error-snackbar'],
                    });
                },
            });
    }

    formatDate(date: Date): string {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');

        return `${year}-${month}-${day}`;
    }
}
