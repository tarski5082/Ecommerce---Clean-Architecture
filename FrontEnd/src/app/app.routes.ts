import { Routes } from '@angular/router';
import { LoginPage } from './pages/login-page/login-page';
import { RegisterPage } from './pages/register-page/register-page';
import { ProductdetailPage } from './pages/productdetail-page/productdetail-page';
import { ProductPage } from './pages/product-page/product-page';
import { ProfilPage } from './pages/profil-page/profil-page';

export const routes: Routes = [
    {path:'login',component:LoginPage},
    {path:'register',component:RegisterPage},
    {path:'product/:id',component:ProductdetailPage},
    {path:'profil',component:ProfilPage},
    {path:'',component:ProductPage}
];
