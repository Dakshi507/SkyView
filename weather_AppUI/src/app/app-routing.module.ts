import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { WeatherComponent } from './weather/weather.component';
import { WishlistComponent } from './wishlist/wishlist.component';
import { AuthGuard } from './_guard/auth.guard';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  { path: '',  component : HomeComponent }, 
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'weather', component: WeatherComponent, canActivate: [AuthGuard] },
  { path: 'wishlist', component: WishlistComponent, canActivate: [AuthGuard] },
  { path: 'weather/:cityName', component: WeatherComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
