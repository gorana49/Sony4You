import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {Router} from "@angular/router";
import { map } from 'rxjs/operators'
import { LoggedUser } from 'src/app/models/LoggedUser';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username: string;
  password: string;
  errorMsg="";
  constructor(private authService:AuthService,
              private router: Router
              ) 
  { 
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
      var u = new LoggedUser(this.username, this.password, "", false);
      this.authService.checkIfUserValid(u)
      .pipe( 
        map(array=> array[0])
      ).subscribe(value=>{
        if(value!=undefined){
          this.errorMsg="";
          //this.store.dispatch(LogIn({user : value }));
          this.router.navigate([`./${value.role}`]);
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

  checkInput(email,password):boolean{
    if((password === '' || password == null || password === undefined ) || 
        (email === '' || email == null || email === undefined))
        return false;
    else return true;
  }
}