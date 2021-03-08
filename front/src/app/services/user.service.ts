import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from "@angular/common/http";
import { Observable } from 'rxjs';
import {User} from "../../data-model/user";


@Injectable({
  providedIn: 'root'
})
export class UserService {
   private url: string = 'https://localhost:5001/api/account/';
  constructor(private http: HttpClient) { }

  postUserLogIn(user: User) : Observable<any>
  {
    return this.http.post(`${this.url}login`, user,
      { headers: new HttpHeaders(
          { 'Content-Type': 'application/json',}
        )});
  }

  gerUserLogOut() : Observable<any>
  {
    return this.http.get(`${this.url}logout`,
      { headers: new HttpHeaders(
          { 'Content-Type': 'application/json',}
        )});
  }

  getChangeUserDetails(userId: string, firstName: string, lastName: string, email: string) : Observable<any>
  {
    return this.http.get(`${this.url}changeUserDetails?userId=${userId}&firstName=${firstName}&lastName=${lastName}&email=${email}`,
      { headers: new HttpHeaders(
          { 'Content-Type': 'application/json',}
        )});
  }

  getChangePassword(userId: string, password: string, newPassword: string) : Observable<any>
  {
    return this.http.get(`${this.url}changePassword?userId=${userId}&password=${password}&newPassword=${newPassword}`,
      { headers: new HttpHeaders(
          { 'Content-Type': 'application/json',}
        )});
  }
}
