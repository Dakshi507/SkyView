import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  private tokenKey = 'token';
  private expirationDate = 'expirationDate';
  private Username = 'Username';
 
 // private apiUrl = 'https://localhost:7170/api';
  private apiUrl = 'https://localhost:7168/gateway';

  constructor(private http: HttpClient, private route: Router) { }

  
  login(username: string, password: string): Observable<any> {
    const body = { username, password };
    return this.http.post<any>(`${this.apiUrl}/Auth/login`, body);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
    
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.expirationDate);
    localStorage.removeItem(this.Username);
    
    this.route.navigate(['./login'])
  }
 

  isTokenValid(): boolean {
    const token = this.getToken();
    return token ? !this.isTokenExpired(token) : false;
  }

 
  private isTokenExpired(token: string): boolean {
    try {
      const tokenExpiryString = localStorage.getItem('expirationDate'); 
      if (!tokenExpiryString) {
        console.error('Token expiry date not found in local storage.');
        return true;
      }
  
      const tokenExpiryDate: Date = new Date(tokenExpiryString);
      const currentDateTime: Date = new Date();
  
      
      if (tokenExpiryDate < currentDateTime) {
        return true;
      } else {
        return false;
      }
    } catch (error) {
      console.error('Error parsing token expiry date:', error);
      return true; 
    }
  }

  handleTokenExpiration(): void {
    if (this.isTokenExpired(this.tokenKey)) {
      alert('Your session is expired. Please log in again.'); 
      this.logout(); 
    }
  }
  
}
