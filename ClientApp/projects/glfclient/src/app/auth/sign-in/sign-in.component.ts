import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  submitting = false;

  signInForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  });

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  onSubmit() {
    if (this.signInForm.invalid)
      return;
      
    this.submitting = true;

    return this.authService.signIn(this.signInForm.value).subscribe({
      next: (response) => {
        console.log('response:', response);
        this.submitting = false;
        this.router.navigateByUrl('main');
      },
      error: ({ error }) => {
        this.signInForm.setErrors({ credentials: true })
        this.submitting = false;
      }
    });
  }

}
