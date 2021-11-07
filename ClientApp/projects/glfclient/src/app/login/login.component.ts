import { Component, OnInit } from '@angular/core';
import { Login } from './login';
import { LoginService } from './login.service';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  // loginForm = this.fb.group({
  //   email:['', Validators.required],
  //   password:['', Validators.required],
  // })
  loginForm = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });
  constructor(private loginService: LoginService) {}

  ngOnInit(): void {
  }


  onSubmit() {
    console.warn(this.loginForm.value);
    return this.loginService.login(this.loginForm.value).subscribe((response) => {
      console.log('RESPONSE=', response);
    })
  }

}
