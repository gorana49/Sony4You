import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoggedUser } from 'src/app/models/LoggedUser';
import { AuthService } from 'src/app/services/auth.service';
import { NavigationService } from 'src/app/services/navigation.service';
// import { AppState } from 'src/app/store';
// import { LogIn } from 'src/app/store/actions/auth.actions';
// import { map } from 'rxjs/operators'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username: string;
  password: string;
  errorMsg="";
  constructor(
              private authService:AuthService,
              private router: Router,
              //private store: Store<AppState>,
              private navigationService: NavigationService
              ) { 
                this.username = "";
                this.password = "";
              }

  ngOnInit(): void {
  }

  @Output() cancelClicked: EventEmitter<any> =
  new EventEmitter();

  cancelLogIn(): void {
    this.cancelClicked.emit();
  }

  btnLoginClicked(){
    const provera=this.checkInput(this.username, this.password);
    if(provera){
      this.authService.checkIfUserValid(new LoggedUser(this.username, this.password,  false, ''))
      .subscribe(value=>{
        if(value!=undefined){
          this.errorMsg="";
          this.authService.logInUser(value);
          localStorage.setItem("user", JSON.stringify(value));
          this.router.navigate([`./${value.role}`]);
          this.navigationService.changeFlag(value.role);

          const loggedUserData: LoggedUser= JSON.parse(localStorage.getItem("user"));
        }
        else{
          this.errorMsg="Pogrešan email ili password!"
        }
      })
    }
    else{
      this.errorMsg="Morate uneti sva input polja!";
    }
  }

  checkInput(username:string,password:string){
    if((password === '' || password == null || password === undefined ) || 
        (username === '' || username == null || username === undefined))
        return false;
    else return true;
  }
}
