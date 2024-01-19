import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RegisterComponent } from './register.component';
import { WeatherService } from '../_services/weather.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';

describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;
  let weatherService: WeatherService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RegisterComponent],
      imports: [HttpClientTestingModule, RouterTestingModule, ReactiveFormsModule],
      providers: [WeatherService]
    }).compileComponents();

    fixture = TestBed.createComponent(RegisterComponent);
    component = fixture.componentInstance;
    weatherService = TestBed.inject(WeatherService);
    router = TestBed.inject(Router); // Inject Router

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call service method and navigate on valid form submission', () => {
    const registerSpy = spyOn(weatherService, 'registerUser').and.returnValue(
      of(Response)
    );
  
    spyOn(router, 'navigateByUrl'); 
  
    component.registerForm.setValue({
      fullName: 'John Doe',
      email: 'john@example.com',
      username: 'johndoe123',
      password: 'Pass@1234',
      confirmPassword: 'Pass@1234',
      phoneNumber: '1234567890'
    });
  
    component.onSubmit();
  
    expect(component.registerForm.valid).toBeTrue();
    expect(registerSpy).toHaveBeenCalled();
    expect(router.navigateByUrl).toHaveBeenCalledWith('/login'); 
  }); 

  it('should handle UserId already exists error', () => {
    const registerSpy = spyOn(weatherService, 'registerUser').and.returnValue(
      throwError({ error: 'UserId already exists' }) 
    );
  
    const navigateSpy = spyOn(router, 'navigateByUrl'); 
  
    component.registerForm.setValue({
      fullName: 'John Doe',
      email: 'john@example.com',
      username: 'johndoe123',
      password: 'Pass@1234',
      confirmPassword: 'Pass@1234',
      phoneNumber: '1234567890'
    });
  
    expect(component.registerForm.valid).toBeTrue(); 
  
    component.onSubmit();
  
    expect(registerSpy).toHaveBeenCalled();
    expect(navigateSpy).not.toHaveBeenCalled(); 
  }); 



  it('should reset form if user already exists', () => {
    const registerSpy = spyOn(weatherService, 'registerUser').and.returnValue(
      throwError({ error: 'Username already exists' }) 
    );

    spyOn(window, 'alert');
    const formResetSpy = spyOn(component.registerForm, 'reset');

    component.registerForm.setValue({
      fullName: 'John Doe',
      email: 'john@example.com',
      username: 'johndoe123',
      password: 'Pass@1234',
      confirmPassword: 'Pass@1234',
      phoneNumber: '1234567890'
    });

    component.onSubmit();

    expect(registerSpy).toHaveBeenCalled();
    expect(window.alert).toHaveBeenCalledWith('Username already exists. Please choose a different username.');
    expect(formResetSpy).toHaveBeenCalled();
  });
   
  it('should reset form on service failure with unknown error', () => {
    const registerSpy = spyOn(weatherService, 'registerUser').and.returnValue(
      throwError({ error: 'Some unknown error' }) 
    );

    spyOn(window, 'alert');
    const formResetSpy = spyOn(component.registerForm, 'reset');

    component.registerForm.setValue({
      fullName: 'John Doe',
      email: 'john@example.com',
      username: 'johndoe123',
      password: 'Pass@1234',
      confirmPassword: 'Pass@1234',
      phoneNumber: '1234567890'
    });

    component.onSubmit();

    expect(registerSpy).toHaveBeenCalled();
    expect(window.alert).toHaveBeenCalledWith('Some error occurred. Please try again.');
    expect(formResetSpy).toHaveBeenCalled();
  });

  it('should check if form is invalid before submission', () => {
    spyOn(window, 'alert');
    const registerSpy = spyOn(weatherService, 'registerUser');
  
    
    component.registerForm.controls['fullName'].markAsTouched(); 
  
    component.onSubmit();
  
    expect(registerSpy).not.toHaveBeenCalled(); 
  });

  
});
