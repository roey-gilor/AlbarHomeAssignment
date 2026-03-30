import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { LoginDto } from '../../model/auth.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  credentials: LoginDto = { Username: '', Password: '' };

  errorMessage = signal<string>('');
  isLoading = signal<boolean>(false);

  private authService = inject(AuthService);
  private router = inject(Router);

  onSubmit() {
    this.errorMessage.set('');
    this.isLoading.set(true);

    this.authService.login(this.credentials).subscribe({
      next: () => {
        this.router.navigate(['/books']);
      },
      error: (err: HttpErrorResponse) => {
        this.isLoading.set(false);
        this.errorMessage.set(err.error.message);
      }
    });
  }
}