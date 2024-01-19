import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HeaderComponent } from './header.component';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthServiceService } from '../_services/auth-service.service';
import { LoggerService } from '../_services/logger.service';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

describe('HeaderComponent', () => {
  let component: HeaderComponent;
  let fixture: ComponentFixture<HeaderComponent>;
  let authService: AuthServiceService;
  let logger: LoggerService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HeaderComponent ],
      imports: [ RouterTestingModule, HttpClientModule ],
      providers: [ AuthServiceService, LoggerService ]
    }).compileComponents();

    fixture = TestBed.createComponent(HeaderComponent);
    component = fixture.componentInstance;
    authService = TestBed.inject(AuthServiceService);
    logger = TestBed.inject(LoggerService);
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should navigate to weather when user is logged in and cityName is provided', () => {
    const navigateSpy = spyOn(router, 'navigate');
    spyOn(authService, 'isLoggedIn').and.returnValue(true);
    const cityName = 'New York';

    component.routeToWeather(cityName);

    expect(navigateSpy).toHaveBeenCalledWith(['/weather', cityName.trim()]);
  });

  

  it('should check if the user is logged in', () => {
    spyOn(authService, 'isLoggedIn').and.returnValue(true);

    const isLoggedIn = component.isLoggedIn();

    expect(isLoggedIn).toBeTrue();
  });

  it('should not navigate when cityName is empty', () => {
    const alertSpy = spyOn(window, 'alert');
    const navigateSpy = spyOn(router, 'navigate');
    spyOn(authService, 'isLoggedIn').and.returnValue(true);
  
    component.routeToWeather('');
  
    expect(alertSpy).not.toHaveBeenCalled();
    expect(navigateSpy).not.toHaveBeenCalled();
  });
  
 
  

  
  it('should navigate to weather when user is logged in and valid cityName is provided', () => {
    const navigateSpy = spyOn(router, 'navigate');
    spyOn(authService, 'isLoggedIn').and.returnValue(true);
    const cityName = 'Sydney';
  
    component.routeToWeather(cityName);
  
    expect(navigateSpy).toHaveBeenCalledWith(['/weather', cityName.trim()]);
  });
  
  
  it('should alert and not navigate when user is not logged in', () => {
    const alertSpy = spyOn(window, 'alert');
    spyOn(authService, 'isLoggedIn').and.returnValue(false);
  
    component.routeToWeather('Berlin');
  
    expect(alertSpy).toHaveBeenCalledWith('Please login first.');
  });
  it('should log out user and trigger logger', () => {
    const authServiceSpy = spyOn(authService, 'logout');
    const loggerSpy = spyOn(logger, 'info');
  
    component.logout();
  
    expect(authServiceSpy).toHaveBeenCalled();
    expect(loggerSpy).toHaveBeenCalledWith('User logged out.');
  });
  it('should return true if the user is logged in', () => {
    spyOn(authService, 'isLoggedIn').and.returnValue(true);
  
    const isLoggedIn = component.isLoggedIn();
  
    expect(isLoggedIn).toBeTrue();
  });
      
});
