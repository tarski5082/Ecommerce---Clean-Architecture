import { Component,inject,input,computed} from '@angular/core';
import { toSignal,toObservable } from '@angular/core/rxjs-interop';
import { CartService } from '../../services/api/cart-service';
import { ProductService } from '../../services/api/product-service';
import { filter, switchMap } from 'rxjs';
@Component({
  selector: 'app-cart-item-page',
  imports: [],
  templateUrl: './cart-item-page.html',
  styleUrl: './cart-item-page.css',
})
export class CartItemPage {
  id=input<string>();
  cartService = inject(CartService);
  productService = inject(ProductService);

  productId = computed(()=>Number(this.id()))
  
  product = toSignal(toObservable(this.productId).pipe(
      filter((id)=>!isNaN(id) && id>0),
      switchMap((id)=>this.productService.getProduct(id))
    ));
}
