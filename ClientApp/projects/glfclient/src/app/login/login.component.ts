import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm = this.fb.group({
    email:['', Validators.required],
    password:['', Validators.required],
  })
  // loginForm = new FormGroup({
  //   email: new FormControl('', Validators.required),
  //   password: new FormControl('', Validators.required)
  // });
  constructor(private http: HttpClient, private fb: FormBuilder) { }

  ngOnInit(): void {
  }

  onSubmit() {
    console.warn(this.loginForm.value);
    var formData = new FormData();

    const email=(this.loginForm.get("email"));
    if (email) formData.append("email", email.value);

    const password=(this.loginForm.get("password"));
    if (password) formData.append("password", password.value);

    var formJson = JSON.stringify(formData);

    var testJson = {
      email: "email.value",
      password: "123"
    }

    var stringified = JSON.stringify(testJson)
    
    console.log("stringified", stringified);
    //formData.append("password", this.loginForm.get("password").value);
    JSON.stringify(testJson)
    this.http.post('http://localhost:33000/api/useraccount/login', stringified).subscribe(
      (response) => console.log("response:", response),
      (error) => console.log("error: ", error)
    )
  }

}
