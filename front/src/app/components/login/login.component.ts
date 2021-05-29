import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {Router} from "@angular/router";
import { map } from 'rxjs/operators'
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  email: string;
  password: string;
  errorMsg="";
  constructor(private authService:AuthService,
              private router: Router
              ) 
  { 
    this.email = "";
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
    const provera=this.checkInput(this.email, this.password);
    if(provera){
      this.authService.checkIfUserValid(this.email, this.password)
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