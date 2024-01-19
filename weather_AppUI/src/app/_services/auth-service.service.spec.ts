import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthServiceService } from './auth-service.service';

describe('AuthServiceService', () => {
  let service: AuthServiceService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule], // Import required modules
      providers: [AuthServiceService]
    });

    service = TestBed.inject(AuthServiceService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify(); // Verifies that there are no outstanding requests after each test
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should perform login', () => {
    const mockLoginResponse = { /* mock response for login testing */ };
    const username = 'testUser';
    const password = 'testPassword';

    service.login(username, password).subscribe(response => {
      expect(response).toEqual(mockLoginResponse); // Ensure the service returns the expected response
    });

    const req = httpTestingController.expectOne('https://localhost:7168/gateway/Auth/login');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ username, password });

    req.flush(mockLoginResponse); // Respond to the request with mock data
  });

  it('should get token from localStorage', () => {
    const token = 'mockToken';
    spyOn(localStorage, 'getItem').withArgs('token').and.returnValue(token);

    const retrievedToken = service.getToken();
    expect(retrievedToken).toEqual(token);
  });

  it('should return false if token does not exist for isLoggedIn', () => {
    spyOn(localStorage, 'getItem').withArgs('token').and.returnValue(null);

    const loggedIn = service.isLoggedIn();

    expect(loggedIn).toBeFalse();
  });

  it('should remove items and navigate to login for logout', () => {
    const removeItemSpy = spyOn(localStorage, 'removeItem');
    const navigateSpy = spyOn(service['route'], 'navigate');

    service.logout();

    expect(removeItemSpy).toHaveBeenCalledTimes(3); // Ensure three items are removed
    expect(removeItemSpy).toHaveBeenCalledWith('token');
    expect(removeItemSpy).toHaveBeenCalledWith('expirationDate');
    expect(removeItemSpy).toHaveBeenCalledWith('Username');
    expect(navigateSpy).toHaveBeenCalledWith(['./login']);
  });

  it('should return false if token is expired for isTokenValid', () => {
    spyOn(service, 'getToken').and.returnValue('mockToken');
    spyOn(localStorage, 'getItem').withArgs('expirationDate').and.returnValue('2022-12-31T23:59:59Z');
    const isTokenExpiredSpy = spyOn(service as any, 'isTokenExpired').and.returnValue(true);

    const isTokenValid = service.isTokenValid();

    expect(isTokenValid).toBeFalse();
    expect(isTokenExpiredSpy).toHaveBeenCalledWith('mockToken');
  });
  it('should return true if token exists for isLoggedIn', () => {
    spyOn(localStorage, 'getItem').withArgs('token').and.returnValue('mockToken');

    const loggedIn = service.isLoggedIn();

    expect(loggedIn).toBeTrue();
  });
});
