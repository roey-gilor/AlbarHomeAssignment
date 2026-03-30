import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { BookListComponent } from './components/book-list/book-list.component';
import { BookFormComponent } from './components/book-form/book-form.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [

    // Default routing
    { path: '', redirectTo: '/login', pathMatch: 'full' },

    // Login Page
    { path: 'login', component: LoginComponent },

    // Get all books method
    {
        path: 'books',
        component: BookListComponent,
        canActivate: [authGuard]
    },

    // Create new book method
    {
        path: 'books/new',
        component: BookFormComponent,
        canActivate: [authGuard]
    },

    // For not exist URL - redirect to login page
    { path: '**', redirectTo: '/login' }
];