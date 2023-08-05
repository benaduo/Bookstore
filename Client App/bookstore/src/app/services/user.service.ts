import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import { User } from '../models/user';
@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  registerUser(user: User) {
    return this.http.post(this.baseUrl + '/auth/register', user);
  }

  login(user: User) {
    return this.http.post(this.baseUrl + '/auth/login', user);
  }
}
