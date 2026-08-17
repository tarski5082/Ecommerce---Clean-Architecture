import { Component,input,inject,computed} from '@angular/core';
import { Router } from '@angular/router';
import { ProductService } from '../../services/api/product-service';

import { toSignal,toObservable} from '@angular/core/rxjs-interop';
import { filter, switchMap } from 'rxjs';
import { ProductResponse } from '../../services/api/models/ProductResponse';
@Component({
  selector: 'app-productdetail-page',
  imports: [],
  templateUrl: './productdetail-page.html',
  styleUrl: './productdetail-page.css',
})
export class ProductdetailPage {
  id =input<string>();
  router =inject(Router);
  productService = inject(ProductService);

  productId = computed(()=>Number(this.id()))

  product = toSignal(toObservable(this.productId).pipe(
    filter((id)=>!isNaN(id) && id>0),
    switchMap((id)=>this.productService.getProduct(id))
  ));
  

}
