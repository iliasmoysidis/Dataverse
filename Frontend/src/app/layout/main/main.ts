import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { Auth } from '../../core/services/auth';

@Component({
    selector: 'app-main',
    imports: [RouterOutlet, RouterLink, MatButtonModule, MatIconModule],
    templateUrl: './main.html',
    styleUrl: './main.css',
})
export class Main {
    private auth = inject(Auth);
    private router = inject(Router);

    logout() {
        this.auth.logout();
        this.router.navigate(['/login'], { replaceUrl: true });
    }

    isActive(url: string): boolean {
        return this.router.url === url;
    }
}
