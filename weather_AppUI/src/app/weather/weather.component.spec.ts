import { ComponentFixture, TestBed } from '@angular/core/testing';
import { WeatherComponent } from './weather.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { WeatherService } from '../_services/weather.service';
import { ActivatedRoute, Router } from '@angular/router';
import { of, throwError } from 'rxjs';

describe('WeatherComponent', () => {
  let component: WeatherComponent;
  let fixture: ComponentFixture<WeatherComponent>;
  let weatherService: WeatherService;
  let router: Router;
  let activatedRoute: ActivatedRoute;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WeatherComponent ],
      imports: [ HttpClientTestingModule, RouterTestingModule ],
      providers: [ WeatherService ]
    }).compileComponents();

    fixture = TestBed.createComponent(WeatherComponent);
    component = fixture.componentInstance;
    activatedRoute = TestBed.inject(ActivatedRoute);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize component properties on ngOnInit', () => {
    spyOn(localStorage, 'getItem').and.returnValue('');
    activatedRoute.params = of({ cityName: 'London' });

    component.ngOnInit();

    expect(component.username).toBe('');
    expect(component.cityName).toBe('London');
   
  });

  it('should fetch weather data when cityName is provided', () => {
    const weatherService = TestBed.inject(WeatherService);
  spyOn(weatherService, 'getWeatherData').and.returnValue(
    of({ name: 'Paris', sys: { country: 'FR' } })
  );

  component.cityName = 'Paris';
  component.getWeather();

  expect(component.loading).toBe(false);
  expect(component.errorMessage).toBeUndefined();
   
  });

  it('should handle error when fetching weather data', () => {
    const weatherService = TestBed.inject(WeatherService);
    spyOn(weatherService, 'getWeatherData').and.returnValue(
      throwError({ error: { message: 'City not found' } }) 
    );
    spyOn(console, 'error');
  
    component.cityName = 'InvalidCity';
    component.getWeather();
  
    expect(component.loading).toBe(false);
    expect(component.errorMessage).toBe('Error: City not found');
    expect(console.error).toHaveBeenCalled();
   
  });

  it('should add city to wishlist', () => {
    spyOn(window, 'alert');
    const weatherService = TestBed.inject(WeatherService);
    const addToWishlistSpy = spyOn(weatherService, 'addToWishlist').and.returnValue(of('City added'));
  
    component.weatherData = { name: 'London', sys: { country: 'GB' } };
    component.addToWishlist();
  
    expect(addToWishlistSpy).toHaveBeenCalledWith({
      city: 'London',
      country: 'GB',
      username: '' 
    });
    expect(window.alert).toHaveBeenCalledWith('City added to wishlist');
   
  });
});
