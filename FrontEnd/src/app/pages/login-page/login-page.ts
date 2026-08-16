import { Component,inject } from '@angular/core';
import { ReactiveFormsModule,FormBuilder,Validators} from '@angular/forms';
import { AuthentificationRequest } from '../../services/api/models/AuthRequest';
import { AuthStateService } from '../../services/auth-state';
@Component({
  selector: 'app-login-page',
  imports: [ReactiveFormsModule],
  templateUrl: './login-page.html',
  styleUrl: './login-page.css',
})
export class LoginPage {
  private readonly fb = inject(FormBuilder);
  private readonly authStateService = inject(AuthStateService);

  loginForm = this.fb.group({
    username: ['', [Validators.required, Validators.minLength(3)]],
    password: ['', [Validators.required, Validators.minLength(3)]]
  });

  onSubmit(){
    
    if(this.loginForm.valid){
      const rawValue =this.loginForm.getRawValue();
      const credential:AuthentificationRequest = {
        username: rawValue.username ?? '',
        password: rawValue.password ?? ''
      }
      this.authStateService.login(credential);
      
    }else{
      this.loginForm.markAllAsTouched();
    }
  }
}
