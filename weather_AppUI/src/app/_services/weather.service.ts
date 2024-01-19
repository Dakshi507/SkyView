import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  private apiUrl = 'https://localhost:7168/gateway';
 // private apiUrl = 'https://localhost:8500/gateway';
  // private Url = 'https://localhost:7251/api'; 
  // private registerUrl = 'https://localhost:44340/api';
  // private wishListUrl = 'https://localhost:44358/api';
  // private deletecityUrl = 'https://localhost:44358/api';
  // private addWishlistUrl = 'https://localhost:44358/api';

  constructor(private http: HttpClient) {}

  getWeatherData(cityName: string): Observable<any> {
    const url = `${this.apiUrl}/Weather/${cityName}`;
    return this.http.get<any>(url);
  }

   registerUser(userData: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/User/create`, userData);
  }


  getWishlistByUsername(username: string): Observable<any> {
    const url = `${this.apiUrl}/WishList/${username}`;
    return this.http.get(url);
  }

  deleteCitybyUser(cityName: string, username: string): Observable<any> {
    const url = `${this.apiUrl}/WishList/city/${cityName}/user/${username}`;
    return this.http.delete<any>(url);
  }

  addToWishlist(cityData: any): Observable<any> {
    
    return this.http.post<any>(`${this.apiUrl}/WishList/add`, cityData);
  }

}
