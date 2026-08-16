import { Component ,OnInit,inject} from '@angular/core';
import { ProductService } from '../../services/api/product-service';
import { Product } from '../../services/api/models/Product';
import { toSignal } from '@angular/core/rxjs-interop';
@Component({
  selector: 'app-product-page',
  imports: [],
  templateUrl: './product-page.html',
  styleUrl: './product-page.css',
})
export class ProductPage{
  productService = inject(ProductService);
  productList = toSignal(this.productService.getAllProduct(),{initialValue:[]});
  
  
}
