import { Component ,OnInit,inject} from '@angular/core';
import { ProductService } from '../../services/api/product-service';
import { toSignal } from '@angular/core/rxjs-interop';
import { Router } from '@angular/router';
@Component({
  selector: 'app-product-page',
  imports: [],
  templateUrl: './product-page.html',
  styleUrl: './product-page.css',
})
export class ProductPage{
  productService = inject(ProductService);
  productList = toSignal(this.productService.getAllProduct(),{initialValue:[]});
  router:Router =inject(Router);
  onClick(id:number){
    this.router.navigate(['product',id]);
  }
}
