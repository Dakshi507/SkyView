import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthServiceService } from '../_services/auth-service.service';
import { LoggerService } from '../_services/logger.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private authService: AuthServiceService, private router: Router,private logger: LoggerService) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
    if (this.authService.isLoggedIn() && !this.authService.isTokenValid()) {
      this.router.navigate(['/weather']);
    } 
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;
      this.authService.login(username, password).subscribe(
        (response) => {
          this.logger.info('Login successful'); 
          console.log('Login successful:', response);
          localStorage.setItem('token', response.tokenValue);
          localStorage.setItem('Username', response.username);
          localStorage.setItem('expirationDate', response.expirationDate);
          
          this.router.navigateByUrl('/weather');

        },
        (error) => {
          this.logger.error('Login failed:', error);
          console.error('Login failed:', error);
          console.log(error.error);
          console.error('Error logging user:', error); 
            if (error.error === 'Invalid username or password') {
              alert('Invalid username or password. Please provide correct input.');
              this.loginForm.reset(); 
            }else
            alert('Some error occured');
            this.loginForm.reset(); 
        }
      );
    } else {
      console.log('Form is invalid. Please check the fields.');
      alert('Form is invalid. Please check all the fields.');
    }
  }

}
