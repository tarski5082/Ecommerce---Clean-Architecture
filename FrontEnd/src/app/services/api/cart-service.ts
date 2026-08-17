import {inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environments.developpements';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CartItemRequest } from './models/CartItemRequest';
import { AuthStateService } from '../auth-state';
import { Cart } from './models/Cart';
@Injectable({
  providedIn: 'root',
})
export class CartService {
  private apiUrl = `${environment.baseUrl}/cart`;
  private readonly http = inject(HttpClient);
  private readonly authStateService = inject(AuthStateService);

  addCartItem(cartItem:CartItemRequest[]){
    return this.http.post<CartItemRequest[]>(`${this.apiUrl}/cart`,cartItem,{headers:this.authStateService.getAuthHeaders()})
  }

  getAllCart():Observable<Cart[]>{
    return this.http.get<Cart[]>(`${this.apiUrl}/carts`,{headers:this.authStateService.getAuthHeaders()})
  }
}
