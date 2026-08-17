import { inject, Injectable,signal } from '@angular/core';
import { environment } from '../../environments/environments.developpements';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserRequest } from './models/ProfilRequest';
import { AuthStateService } from '../auth-state';
import { AddressRequest } from './models/AdresseRequest';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl = `${environment.baseUrl}/user`;
  private readonly http = inject(HttpClient);
  private readonly authStateService = inject(AuthStateService);
  getProfil():Observable<UserRequest>{
    return this.http.get<UserRequest>(`${this.apiUrl}`,{headers:this.authStateService.getAuthHeaders()});
  }

  addLivraison(request:AddressRequest):Observable<any>{
    return this.http.post<AddressRequest>(`${this.apiUrl}/livraison`,request,{headers:this.authStateService.getAuthHeaders()});
  }
  addFacturation(request:AddressRequest):Observable<any>{
    return this.http.post<AddressRequest>(`${this.apiUrl}/facturation`,request,{headers:this.authStateService.getAuthHeaders()});
  }
}
