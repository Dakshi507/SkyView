import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { AuthServiceService } from '../_services/auth-service.service';
import { HttpClientTestingModule } from '@angular/common/http/testing'; // Import HttpClientTestingModule


describe('AuthGuard', () => {
  let guard: AuthGuard;
  let authService: AuthServiceService;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule // Include HttpClientTestingModule
      ],
      providers: [AuthGuard, AuthServiceService],
    });
    guard = TestBed.inject(AuthGuard);
    authService = TestBed.inject(AuthServiceService);
    router = TestBed.inject(Router);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });

  it('should allow navigation if user is logged in and token is valid', () => {
    spyOn(authService, 'isLoggedIn').and.returnValue(true);
    spyOn(authService, 'isTokenValid').and.returnValue(true);

    const canActivate = guard.canActivate();

    expect(canActivate).toBeTrue();
  });

  it('should navigate to login page if user is not logged in or token is invalid', () => {
    spyOn(authService, 'isLoggedIn').and.returnValue(false);
    spyOn(authService, 'isTokenValid').and.returnValue(false);
    const navigateSpy = spyOn(router, 'navigate');

    const canActivate = guard.canActivate();

    expect(canActivate).toBeFalse();
    expect(navigateSpy).toHaveBeenCalledWith(['/login']);
  });

 

});
