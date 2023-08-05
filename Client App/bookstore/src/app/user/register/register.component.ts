import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  registerForm!: FormGroup;
  message: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private userService: UserService

  ) {

    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }


  onSubmit() {
    if (this.registerForm.invalid) {
      this.message = "One or more values are invalid.";
      return;
    }
    this.userService.registerUser(this.registerForm.value).subscribe({
      next: (res) => {
        this.router.navigate(['/user/login']);
      },
      error: (err) => { this.message = err.error.error; },
      complete: () => { }
    })
  }
}
