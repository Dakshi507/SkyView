import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { WeatherService } from '../_services/weather.service';
import { Router } from '@angular/router';
import { LoggerService } from '../_services/logger.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

registerForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private service: WeatherService, private router: Router, private logger: LoggerService ) { }

  ngOnInit(): void {
      this.registerForm = this.formBuilder.group({
        fullName : ['', [Validators.required, Validators.minLength(3), Validators.maxLength(40), Validators.pattern(/^[a-zA-Z]+(?: [a-zA-Z]+)*$/)]],
        email: ['', [Validators.required, Validators.pattern('^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$')]],
        username :['',[Validators.required, Validators.minLength(3), Validators.maxLength(20), Validators.pattern(/^[a-zA-Z][a-zA-Z0-9._]*$/)]],
        password : ['',[Validators.required, Validators.minLength(4), Validators.maxLength(20), Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d@$!%*?&]+$/)]],
        confirmPassword: ['', [Validators.required ]],
        phoneNumber: ['', [ Validators.required, Validators.pattern('[0-9]{10}') ]]
      }, {
        validator: this.passwordMatchValidator
      });
  }

  passwordMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('password')?.value;
    const confirmPassword = formGroup.get('confirmPassword')?.value;
  
    if (password !== confirmPassword) {
      formGroup.get('confirmPassword')?.setErrors({ mismatch: true });
    } else {
      formGroup.get('confirmPassword')?.setErrors(null);
    }
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const formData = this.registerForm.value;
    
      this.service.registerUser(formData).subscribe(
        (response) => {
         
          this.logger.info('User registered successfully!');
          console.log('User registered successfully!', response);
          window.alert('User registered successfully!');
          this.router.navigateByUrl('/login');
        },

        (error) => {
          console.log(error.error);
          console.error('Error registering user:', error);
          if (error.error) {

            console.log('Received error message:', error.error); 
            if (error.error === 'Username already exists') {
              window.alert('Username already exists. Please choose a different username.');
              this.registerForm.reset(); 
            } else if (error.error === 'UserId already exists') {
              window.alert('UserID already exists. Please try a different UserID.');
              this.registerForm.reset(); 
            } else if (error.error === 'Failed to create user') {
              window.alert('Failed to create user. Please try again later.');
              this.registerForm.reset(); 
            } else {
              window.alert('Some error occurred. Please try again.');
              this.registerForm.reset(); 
            }
          } 
        });
    } else {
          console.log('Form is invalid. Please check the fields.');
        }
  }

}
