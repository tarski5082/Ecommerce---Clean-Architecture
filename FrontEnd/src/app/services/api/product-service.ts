import {inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environments.developpements';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from './models/Product';
@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private apiUrl = `${environment.baseUrl}/product`;
  private readonly http = inject(HttpClient);

  getAllProduct():Observable<Product[]>{
    return this.http.get<Product[]>(this.apiUrl);
  }
}
