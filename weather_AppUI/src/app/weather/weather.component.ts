import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { WeatherService } from '../_services/weather.service';
import { LoggerService } from '../_services/logger.service';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.css']
})
export class WeatherComponent implements OnInit {
  errorMessage!: string;
  username: any;
  wishlistData: any;

  constructor(private weatherService: WeatherService, private route: ActivatedRoute, private router:Router, private logger : LoggerService  ) { 
    this.username = localStorage.getItem('Username') ?? '';
  }
  weatherData: any; 
  cityName: any;
  loading: boolean = false;
  

  ngOnInit(): void {   
    this.route.params.subscribe(params => {
      const cityNameParam = params['cityName'];
      if (cityNameParam) {
        this.cityName = cityNameParam;
        this.getWeather(); 
      } else {
        //this.router.navigate(['/weather'])
        this.cityName = null
      }

    });
    this.weatherService.getWishlistByUsername(this.username).subscribe(
      (data) => {
        this.wishlistData = data;
        console.log(this.wishlistData);
      },
      (error) => {
        console.error('Error fetching wishlist data:', error);
        this.logger.error('Error fetching wishlist data:', error); 
      }
    );
  }

  checkCityExists(cityName: string): boolean {
    const foundCity = this.wishlistData.some((cityObj: { city: string; }) => cityObj.city.toLowerCase() === cityName.toLowerCase());
    return foundCity;
  }


  getWeather(): void {
    if (this.cityName.trim() !== '') {
      this.loading = true;
      this.weatherService.getWeatherData(this.cityName).subscribe(
        (data: any) => {
          this.weatherData = data; 
          this.loading = false;
        },
        (error) => {
          console.error('Error fetching weather data:', error);
          this.logger.error('Error fetching weather data:', error);
          this.weatherData = null;
          this.loading = false;
          this.errorMessage = `Error: ${error.error?.message || 'Unknown error'}`; 
          console.log(this.errorMessage)
       
        }
      );
    }
  }

  addToWishlist(): void {
    console.log(this.weatherData);
    const data = {
      city: this. weatherData.name,
      country: this.weatherData.sys.country,
      username: localStorage.getItem('Username') ?? ''
    };
    console.log(data)
    this.weatherService.addToWishlist(data).subscribe(
      (response) => {
        console.log('City added to wishlist:', response);
        alert('City added to wishlist')
        this.router.routeReuseStrategy.shouldReuseRoute = () => false;
        this.router.onSameUrlNavigation = 'reload';
        this.router.navigate([this.router.url]);
      },
      (error) => {
        console.error('Error adding city to wishlist:', error);
        console.log(error.error);
        if (error.error) {

          console.log('Received error message:', error.error); 
          if (error.error === 'city already exists in wishlist') {
            window.alert('city already exists in wishlist'); 
          } 
          else{
            window.alert('Some error occured'); 
          }
        }
      }
    );
  }

  goToWishlist(): void {
    this.router.navigate(['/wishlist']); 
  }


}
