import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { LoginComponent } from './login.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AuthServiceService } from '../_services/auth-service.service';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { Router } from '@angular/router';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authService: AuthServiceService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoginComponent ],
      imports: [ HttpClientTestingModule, RouterTestingModule, ReactiveFormsModule ],
      providers: [ AuthServiceService ]
    }).compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    authService = TestBed.inject(AuthServiceService);
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call login method and navigate to weather on successful login', fakeAsync(() => {
    const loginResponse = {
      tokenValue: 'mockToken',
      username: 'testUser',
      expirationDate: 'mockExpirationDate'
    };
    spyOn(authService, 'login').and.returnValue(of(loginResponse));
    spyOn(localStorage, 'setItem');
    spyOn(router, 'navigateByUrl');

    component.loginForm.setValue({
      username: 'test',
      password: 'password'
    });

    component.onSubmit();
    tick();

    expect(authService.login).toHaveBeenCalledWith('test', 'password');
    expect(localStorage.setItem).toHaveBeenCalledWith('token', loginResponse.tokenValue);
    expect(localStorage.setItem).toHaveBeenCalledWith('Username', loginResponse.username);
    expect(localStorage.setItem).toHaveBeenCalledWith('expirationDate', loginResponse.expirationDate);
    expect(router.navigateByUrl).toHaveBeenCalledWith('/weather');

  }));

  it('should show alert for invalid form submission', fakeAsync(() => {
    const loginFormValues = {
      username: '',
      password: ''
    };
  
    spyOn(window, 'alert');
    spyOn(authService, 'login');
  
    component.loginForm.setValue(loginFormValues);
    component.onSubmit();
    tick();
  
    expect(window.alert).toHaveBeenCalledWith('Form is invalid. Please check all the fields.');
    expect(authService.login).not.toHaveBeenCalled();
  }));
  
  it('should handle invalid username or password error', fakeAsync(() => {
    const loginFormValues = {
      username: 'testUser',
      password: 'invalidPassword'
    };
    const errorResponse = { error: 'Invalid username or password' };
  
    spyOn(window, 'alert');
    spyOn(authService, 'login').and.returnValue(throwError(errorResponse));
  
    component.loginForm.setValue(loginFormValues); 
  
    component.onSubmit();
    tick();
  
    expect(window.alert).toHaveBeenCalledWith('Invalid username or password. Please provide correct input.');
    expect(component.loginForm.value).toEqual({ username: null, password: null });
  }));
  
  it('should handle generic login error', fakeAsync(() => {
    const loginFormValues = {
      username: 'testUser',
      password: 'invalidPassword'
    };
    const errorResponse = {}; 
  
    spyOn(window, 'alert');
    spyOn(authService, 'login').and.returnValue(throwError(errorResponse));
  
    component.loginForm.setValue(loginFormValues);
  
    component.onSubmit();
    tick();
  
    expect(window.alert).toHaveBeenCalledWith('Some error occured'); 
    expect(component.loginForm.value).toEqual({ username: null, password: null });
  }));
  
  
 

});
