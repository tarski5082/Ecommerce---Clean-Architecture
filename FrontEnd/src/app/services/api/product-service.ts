import {inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environments.developpements';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from './models/Product';
import { ProductResponse } from './models/ProductResponse';
import { Cart } from './models/Cart';
@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private apiUrl = `${environment.baseUrl}/product`;
  private readonly http = inject(HttpClient);

  getAllProduct():Observable<Product[]>{
    return this.http.get<Product[]>(this.apiUrl);
  }
  getProduct(id:number):Observable<ProductResponse>{

    return this.http.get<ProductResponse>(`${this.apiUrl}/${id}`);
  }

}
