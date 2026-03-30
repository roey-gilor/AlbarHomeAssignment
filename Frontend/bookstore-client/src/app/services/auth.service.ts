import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { tap } from 'rxjs/operators'
import { LoginDto, AuthResponse } from '../model/auth.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7112/api/auth'

  login(credentials: LoginDto) {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, credentials).pipe(
      tap((response: AuthResponse) => {
        if (response && response.token)
          localStorage.setItem('jwt_token', response.token)
      }))
  }

  logout() {
    localStorage.removeItem('jwt_token');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('jwt_token');
  }

  getToken(): string | null {
    return localStorage.getItem('jwt_token');
  }
}
