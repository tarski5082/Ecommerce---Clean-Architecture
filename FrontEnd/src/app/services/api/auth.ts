import { inject, Injectable,signal } from '@angular/core';
import { environment } from '../../environments/environments.developpements';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthentificationRequest } from './models/AuthRequest';
import { LoginResponse } from './models/LoginResponse';
import { RegisterRequest } from './models/RegisterRequest';
import { RegisterResponse } from './models/RegisterResponse';
import { UserRequest } from './models/ProfilRequest';
import { AuthStateService } from '../auth-state';
import { AddressRequest } from './models/AdresseRequest';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = `${environment.baseUrl}/user`;
  private readonly http = inject(HttpClient);
  

  login(credentials: AuthentificationRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/auth`, credentials);
  }
  register(userInfo: RegisterRequest): Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>(`${this.apiUrl}/register`, userInfo);
  }
  

}
