import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Absences } from './pages/absences/absences';
import { Public } from './layout/public/public';
import { guestGuard } from './core/guards/guest.guard';
import { Main } from './layout/main/main';
import { authGuard } from './core/guards/auth.guard';
import { CreateAbsence } from './pages/create-absence/create-absence';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'login',
    },

    {
        path: '',
        component: Public,
        children: [
            {
                path: 'login',
                component: Login,
                canActivate: [guestGuard],
            },
            {
                path: 'register',
                component: Register,
                canActivate: [guestGuard],
            },
        ],
    },

    {
        path: '',
        component: Main,
        canActivate: [authGuard],
        children: [
            {
                path: 'absences',
                component: Absences,
            },
            {
                path: 'request-absence',
                component: CreateAbsence
            }
        ],
    },

    {
        path: '**',
        redirectTo: 'login',
    },
];
