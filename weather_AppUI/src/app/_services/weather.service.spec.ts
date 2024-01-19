import { HttpClientModule } from '@angular/common/http';
import { WeatherService } from './weather.service';
import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('WeatherService', () => {
  let service: WeatherService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule, HttpClientTestingModule], 
      providers: [WeatherService]
    });
    service = TestBed.inject(WeatherService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve weather data for a specific city', () => {
    const cityName = 'London';
    const mockWeatherData = { /* mock data for testing */ };
    
    service.getWeatherData(cityName).subscribe(data => {
      expect(data).toEqual(mockWeatherData);
    });

    const req = httpTestingController.expectOne(`https://localhost:7168/gateway/Weather/${cityName}`);
    expect(req.request.method).toBe('GET');

    req.flush(mockWeatherData); 
  });

  it('should register a user', () => {
    const userData = {  };
    const mockResponse = {  };

    service.registerUser(userData).subscribe(response => {
      expect(response).toEqual(mockResponse); 
    });

    const req = httpTestingController.expectOne('https://localhost:7168/gateway/User/create');
    expect(req.request.method).toBe('POST');

    req.flush(mockResponse); 
  });

  it('should retrieve wishlist data for a specific username', () => {
    const username = 'testUser';
    const mockWishlistData = { /* mock data for testing */ };
    
    service.getWishlistByUsername(username).subscribe(data => {
      expect(data).toEqual(mockWishlistData);
    });

    const req = httpTestingController.expectOne(`https://localhost:7168/gateway/WishList/${username}`);
    expect(req.request.method).toBe('GET');

    req.flush(mockWishlistData); 
  });

  it('should delete a city for a specific username', () => {
    const cityName = 'London';
    const username = 'testUser';
    const mockResponse = { /* mock data for testing */ };

    service.deleteCitybyUser(cityName, username).subscribe(response => {
      expect(response).toEqual(mockResponse); 
    });

    const req = httpTestingController.expectOne(`https://localhost:7168/gateway/WishList/city/${cityName}/user/${username}`);
    expect(req.request.method).toBe('DELETE');

    req.flush(mockResponse);
  });

  it('should add a city to the wishlist', () => {
    const cityData = { /* mock data for testing */ };
    const mockResponse = { /* mock data for testing */ };

    service.addToWishlist(cityData).subscribe(response => {
      expect(response).toEqual(mockResponse); 
    });

    const req = httpTestingController.expectOne('https://localhost:7168/gateway/WishList/add');
    expect(req.request.method).toBe('POST');

    req.flush(mockResponse); 
  });

});
