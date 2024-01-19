import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthServiceService } from '../_services/auth-service.service';
import { LoggerService } from '../_services/logger.service';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent  {

  constructor(
    private router: Router,
    private authService: AuthServiceService,
    private logger: LoggerService
  ) {}

 

  routeToWeather(cityName: string): void {
    if (!this.authService.isLoggedIn()) {
      alert('Please login first.'); 
      this.logger.warn('User tried to navigate to weather without logging in.');
      return;
    }

    if (cityName && cityName.trim() !== '') {
      this.router.navigate(['/weather', cityName.trim()]);
    } else {
      this.logger.error('Invalid city name provided.');
    }
  }
  
  logout(): void {
    this.authService.logout();
    this.logger.info('User logged out.');
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }
}
