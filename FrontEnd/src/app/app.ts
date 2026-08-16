import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { RegisterPage } from './pages/register-page/register-page';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet,RegisterPage],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('FrontEnd');
}
