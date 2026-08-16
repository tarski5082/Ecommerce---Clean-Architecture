import { Component,inject } from '@angular/core';
import { ReactiveFormsModule,FormBuilder,Validators} from '@angular/forms';
import { RegisterRequest } from '../../services/api/models/RegisterRequest';
import { AuthStateService } from '../../services/auth-state';

@Component({
  selector: 'app-register-page',
  imports: [ReactiveFormsModule],
  templateUrl: './register-page.html',
  styleUrl: './register-page.css',
})
export class RegisterPage {
  authStateService = inject(AuthStateService);
  fb=inject(FormBuilder);

  registerForm = this.fb.group({
    username: ['', [Validators.required, Validators.minLength(3)]],
    password: ['', [Validators.required, Validators.minLength(3)]],
    confirmPassword:['', [Validators.required, Validators.minLength(3)]]
  });

  onSubmit(){
      
      if(this.registerForm.valid){
        const rawValue =this.registerForm.getRawValue();
        if (rawValue.password !== rawValue.confirmPassword) {
          return;
        }
        const userInfo:RegisterRequest= {
          username: rawValue.username ?? '',
          password: rawValue.password ?? '',
          confirmPassword:rawValue.confirmPassword??''
        }
        this.authStateService.register(userInfo);
        
      }else{
        this.registerForm.markAllAsTouched();
      }
  }

  get username() {
    return this.registerForm.get('username')!;
  }

  get password() {
    return this.registerForm.get('password')!;
  }

  get confirmPassword() {
    return this.registerForm.get('confirmPassword')!;
  }
}
