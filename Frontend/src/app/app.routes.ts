import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Absences } from './pages/absences/absences';
import { Public } from './layout/public/public';
import { guestGuard } from './core/guards/guest.guard';
import { Main } from './layout/main/main';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    {
        path: '',
        component: Public,
        children: [
            { path: 'login', component: Login, canActivate: [guestGuard] },
            { path: 'register', component: Register, canActivate: [guestGuard] },
        ],
    },

    {
        path: '',
        component: Main,
        canActivate: [authGuard],
        children: [{ path: 'absences', component: Absences }],
    },

    {path: '', pathMatch: 'full', redirectTo: 'login'},

    {path: '*', redirectTo: 'login'}
];
