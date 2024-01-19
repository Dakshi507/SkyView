import { Component, OnInit } from '@angular/core';
import { WeatherService } from '../_services/weather.service';
import { LoggerService } from '../_services/logger.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private weatherService: WeatherService, private logger: LoggerService) { }
  weatherData : any
  ngOnInit(): void {
    this.weatherService.getWeatherData('delhi').subscribe(
      (data: any) => {
        this.weatherData = data; 
       
      },
      (error) => {
        console.error('Error fetching weather data:', error);
        this.logger.error('Error fetching weather data:', error);
        this.weatherData = null;
      }
      );
    }

}
