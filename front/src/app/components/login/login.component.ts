import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
// import { Store } from '@ngrx/store';
// import { AuthService } from 'src/app/services/auth.service';
// import { AppState } from 'src/app/store';
// import { LogIn } from 'src/app/store/actions/auth.actions';
// import { map } from 'rxjs/operators'
// import { NavService } from 'src/app/services/nav.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  email: string;
  password: string;
  errorMsg="";
  constructor(// private authService:AuthService,
              private router: Router,
              //private store: Store<AppState>,
              //private navService: NavService
              ) { 
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
    // const provera=this.checkInput(this.email, this.password);
    // if(provera){
    //   this.authService.checkIfUserValid(this.email, this.password)
    //   .pipe( 
    //     map(array=> array[0])
    //   ).subscribe(value=>{
    //     if(value!=undefined){
    //       this.errorMsg="";
    //       this.store.dispatch(LogIn({user : value }));
    //       this.router.navigate([`./${value.role}`]);
    //       this.navService.changeFlag(true);
    //     }
    //     else{
    //       this.errorMsg="Pogrešan email ili password!"
    //     }
    //   })
    // }
    // else{
    //   this.errorMsg="Morate uneti sva input polja!";
    // }
  }

  checkInput(email:string,password:string){
    if((password === '' || password == null || password === undefined ) || 
        (email === '' || email == null || email === undefined))
        return false;
    else return true;
  }
}
