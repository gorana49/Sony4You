import { Component, OnInit } from '@angular/core';
import {UserService} from "../../services/user.service";
import {Router} from "@angular/router";
import {User} from "../../../data-model/user";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: User;
  errorMessage: string = "";
  constructor(private userService: UserService,
    private router: Router) {
this.user = new User();
}

  ngOnInit() {
    this.user.username = "";
    this.user.password = "";
    this.errorMessage = "";
  }

  onLogIn()
  {
    if(this.user.username == "" || this.user.username == null)
      return;
    if(this.user.password == "" || this.user.password == null)
      return;
    this.userService.postUserLogIn(this.user).subscribe(
      (result: User) => {
        let user = result;
        if(user!=null) {
          this.errorMessage = "";
          localStorage.setItem('user', JSON.stringify(user));
          this.router.navigate(['/estimator']);
        }
        else
        {
          this.errorMessage = "Invalid LogIn";
        }
      }
    )
  }
}