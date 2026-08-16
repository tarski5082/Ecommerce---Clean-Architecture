import { inject, Injectable,signal,computed } from '@angular/core';
import { AuthService } from './api/auth';
import { Router } from '@angular/router';
import { AuthentificationRequest } from './api/models/AuthRequest';
import { RegisterRequest } from './api/models/RegisterRequest';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthStateService {
  private readonly TOKEN_KEY = 'authToken';
  private readonly tokenSignal = signal<string | null>(localStorage.getItem(this.TOKEN_KEY));

  private authService = inject(AuthService);
  private router = inject(Router);
  
  public readonly currentUserTokenValue = this.tokenSignal.asReadonly();
  public readonly isLoggedIn = computed(() => !!this.tokenSignal());
  

  login(credentials: AuthentificationRequest) {
    this.authService.login(credentials).subscribe({
      next: response => {
        if (response && response.token) {
          localStorage.setItem(this.TOKEN_KEY, response.token);
          this.tokenSignal.set(response.token);
          this.router.navigate(['/']);
        }
      },
      error: err => {
        console.error(err);
      }
    });
  }

  register(credentials: RegisterRequest) {
    this.authService.register(credentials).subscribe({
      next: _ => {
          this.router.navigate(['/login']);
      },
      error: err => {
        console.error(err);
      }
    });
  }

  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    this.tokenSignal.set(null);
    this.router.navigate(['/login']);
  }

  public getAuthHeaders(): HttpHeaders {
    const token = this.tokenSignal();
    return token 
      ? new HttpHeaders({ 'Authorization': `Bearer ${token}` })
      : new HttpHeaders();
  }
}
