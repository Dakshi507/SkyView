import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeComponent } from './home.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { WeatherService } from '../_services/weather.service';
import { of, throwError } from 'rxjs';


describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let weatherService: WeatherService

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeComponent ],
      imports: [ HttpClientTestingModule, RouterTestingModule ],
      providers: [ WeatherService ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch weather data on initialization', () => {
    let mockWeatherData: undefined;

    component.ngOnInit();

    expect(component.weatherData).toEqual(mockWeatherData);
  });

  it('should handle error when fetching weather data', () => {
    const errorResponse = 'Error fetching weather data';
    const weatherService = TestBed.inject(WeatherService);
    
    spyOn(weatherService, 'getWeatherData').and.returnValue(throwError(errorResponse));
    spyOn(console, 'error');
    
    component.ngOnInit();
  
    expect(console.error).toHaveBeenCalledWith('Error fetching weather data:', errorResponse);
    expect(component.weatherData).toBeNull();
  });

 
  it('should handle different weather data structures', () => {
    const mockWeatherData1 = { /* mock data structure 1 */ };
    const mockWeatherData2 = { /* mock data structure 2 */ };
    
    spyOn(TestBed.inject(WeatherService), 'getWeatherData').and.returnValues(of(mockWeatherData1), of(mockWeatherData2));
  
    component.ngOnInit();
    expect(component.weatherData).toEqual(mockWeatherData1);
  
    component.ngOnInit();
    expect(component.weatherData).toEqual(mockWeatherData2);
  });
  
 
  
  
  

});
