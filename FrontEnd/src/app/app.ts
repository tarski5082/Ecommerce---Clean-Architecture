import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { ProductPage } from './pages/product-page/product-page';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet,ProductPage],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('FrontEnd');
}
