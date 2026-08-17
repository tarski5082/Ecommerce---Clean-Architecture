import { Component,inject,signal,effect } from '@angular/core';
import { UserService } from '../../services/api/user-service';
import { toSignal } from '@angular/core/rxjs-interop';
import { catchError, map } from 'rxjs/operators';
import { EMPTY } from 'rxjs';
@Component({
  selector: 'app-profil-page',
  imports: [],
  templateUrl: './profil-page.html',
  styleUrl: './profil-page.css',
})
export class ProfilPage {
  user = inject(UserService);
  isLoading = signal(true);
  hasError = signal(false);
  
  userProfil = toSignal(
    this.user.getProfil().pipe(
      catchError(() => {
        this.hasError.set(true);
        this.isLoading.set(false);
        return EMPTY; 
      })
    ),
    {
      initialValue: null,
    }
  );
  _ = effect(() => {
    if (this.userProfil()&& !this.hasError()) {
        this.isLoading.set(false);
    }
  });
  
}
