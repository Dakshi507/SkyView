import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WeatherService } from '../_services/weather.service';
import { LoggerService } from '../_services/logger.service';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.css']
})
export class WishlistComponent implements OnInit {
  routeToHome() {
    console.log('Dakshi')
    this.route.navigate(['/weather']);
  }
  username: string = '';
  wishlistData: any[] = [];
  constructor(private route: Router, private service: WeatherService,  private logger: LoggerService) {
    this.username = localStorage.getItem('Username') ?? '';
  }
  ngOnInit(): void {
    if (this.username) {
      this.getWishlistData();
    }
  }
  checkWeather(cityName: string): void {
    this.route.navigate(['/weather', cityName]);
  }

  getWishlistData() {
    this.service.getWishlistByUsername(this.username).subscribe(
      (data) => {
        this.wishlistData = data as any[];
        console.log(this.wishlistData);
      },
      (error) => {
        console.error('Error fetching wishlist data:', error);
        this.logger.error('Error fetching wishlist data:', error);

      }
    );
  }

  deleteCitybyUser(cityName: string): void {
    const confirmDelete = confirm('Are you sure you want to delete this city?');

    if (confirmDelete) {
      this.service.deleteCitybyUser(cityName, this.username).subscribe(
        () => {
          console.log('City deleted successfully');
          alert('City deleted successfully');
          this.getWishlistData();
          this.route.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.route.navigate(['/wishlist']);
          });
        },
        (error) => {
          console.error('Error deleting city:', error);
          this.logger.error('Error deleting city:', error);
          alert('Error deleting city. Please try again.');
        }
      );
    }
  }
}
