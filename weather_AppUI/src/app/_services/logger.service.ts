import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoggerService {
  private enableLogging = true; 

 

  info(message: string): void {
    if (this.enableLogging) {
      console.log(`[INFO] ${message}`);
    }
  }

  error(message: string, error?: any): void {
    if (this.enableLogging) {
      if (error) {
        console.error(`[ERROR] ${message}`, error);
      } else {
        console.error(`[ERROR] ${message}`);
      }
    }
  }

  debug(message: string): void {
    if (this.enableLogging) {
      console.debug(`[DEBUG] ${message}`);
    }
  }

  warn(message: string): void {
    if (this.enableLogging) {
      console.warn(`[WARN] ${message}`);
    }
  }
  
}
