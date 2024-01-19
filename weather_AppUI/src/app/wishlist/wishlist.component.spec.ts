import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { WishlistComponent } from './wishlist.component';
import { RouterTestingModule } from '@angular/router/testing';
import { WeatherService } from '../_services/weather.service';
import { of, throwError} from 'rxjs';

describe('WishlistComponent', () => {
  let component: WishlistComponent;
  let fixture: ComponentFixture<WishlistComponent>;
  let weatherService: WeatherService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WishlistComponent ],
      imports: [HttpClientTestingModule, RouterTestingModule],
      providers: [WeatherService] 
    })
    .compileComponents();

    fixture = TestBed.createComponent(WishlistComponent);
    component = fixture.componentInstance;
    weatherService = TestBed.inject(WeatherService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch wishlist data for a user', () => {
    const mockWishlistData: any[] = [];

    spyOn(weatherService, 'getWishlistByUsername').and.returnValue(of(mockWishlistData));

    component.ngOnInit();

    expect(component.wishlistData).toEqual(mockWishlistData);
  });

  it('should delete a city from the wishlist', () => {
    const cityNameToDelete = 'Paris';

    spyOn(window, 'confirm').and.returnValue(true);
    spyOn(weatherService, 'deleteCitybyUser').and.returnValue(of(null));

    spyOn(component, 'getWishlistData').and.stub(); 

    component.deleteCitybyUser(cityNameToDelete);

    expect(weatherService.deleteCitybyUser).toHaveBeenCalledWith(cityNameToDelete, component.username);
    
  });
  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch wishlist data for a user', () => {
    const mockWishlistData: any[] = [];

    spyOn(weatherService, 'getWishlistByUsername').and.returnValue(of(mockWishlistData));

    component.ngOnInit();

    expect(component.wishlistData).toEqual(mockWishlistData);
  });


  it('should navigate to weather route on checkWeather()', () => {
    const routerSpy = spyOn(component['route'], 'navigate');

    component.checkWeather('Paris');

    expect(routerSpy).toHaveBeenCalledWith(['/weather', 'Paris']);
  });
  it('should navigate to weather route on routeToHome()', () => {
    const routerSpy = spyOn(component['route'], 'navigate');
  
    component.routeToHome();
  
    expect(routerSpy).toHaveBeenCalledWith(['/weather']);
  });
  it('should not fetch wishlist data if username is empty', () => {
    const mockWishlistData: any[] = [];
    spyOn(weatherService, 'getWishlistByUsername').and.returnValue(of(mockWishlistData));
    spyOn(localStorage, 'getItem').and.returnValue(''); 
  
    component.ngOnInit();
  
    expect(component.wishlistData).toEqual([]); 
  });


  
  it('should delete a city from the wishlist and handle cancellation', () => {
    spyOn(window, 'confirm').and.returnValue(false);
    spyOn(weatherService, 'deleteCitybyUser'); 
    
    component.deleteCitybyUser('Paris');
    
    expect(weatherService.deleteCitybyUser).not.toHaveBeenCalled();
  });

  it('should handle error when deleting city', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    spyOn(weatherService, 'deleteCitybyUser').and.returnValue(throwError('Error deleting city'));
    spyOn(console, 'error');
    spyOn(window, 'alert');
  
    component.deleteCitybyUser('Paris');
  
    expect(console.error).toHaveBeenCalledWith('Error deleting city:', 'Error deleting city');
    expect(window.alert).toHaveBeenCalledWith('Error deleting city. Please try again.');
  });

});
