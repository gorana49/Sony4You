import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {select, Store} from '@ngrx/store';
import {isLoggedIn, selectedLoggedUser} from '../store/selectors/auth.selectors';
import {AppState} from '../store'
import { LoggedUser } from '../models/LoggedUser';



@Injectable({ providedIn: 'root' })
export class AuthRoleGuard implements CanActivate {
  loggedUser:LoggedUser;
  logged: boolean = false;
  constructor(private store: Store<AppState>, 
              private router: Router) { }


  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):boolean  {

    this.store.select(isLoggedIn).subscribe( 
      value => this.logged=value
    )
    this.store.select(selectedLoggedUser).subscribe( 
      value => this.loggedUser=value
    )
    if(this.logged && route.data.role === this.loggedUser.role){
        return true;
    }
    this.router.navigate(['./mainPage']);
    return false;
  }
}