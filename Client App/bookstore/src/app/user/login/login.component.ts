import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm!: FormGroup;
  message: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService

  ) {

    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }


  onSubmit() {
    if (this.loginForm.invalid) {
      this.message = "One or more values are invalid.";
      return;
    }
    this.userService.login(this.loginForm.value).subscribe({
      next: (res: any) => {
        sessionStorage.setItem('token', res.token);
        this.router.navigate(['/books']);
      },
      error: (err) => {
       this.message = err.error.error; },
      complete: () => { }
    })
  }
}
